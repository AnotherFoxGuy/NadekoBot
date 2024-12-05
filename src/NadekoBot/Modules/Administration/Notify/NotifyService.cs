﻿using LinqToDB;
using LinqToDB.EntityFrameworkCore;
using NadekoBot.Common.ModuleBehaviors;
using NadekoBot.Db.Models;

namespace NadekoBot.Modules.Administration;

public sealed class NotifyService : IReadyExecutor, INotifySubscriber, INService
{
    private readonly DbService _db;
    private readonly IMessageSenderService _mss;
    private readonly DiscordSocketClient _client;
    private readonly IBotCreds _creds;
    private readonly IReplacementService _repSvc;
    private readonly IPubSub _pubSub;
    private ConcurrentDictionary<NotifyType, ConcurrentDictionary<ulong, Notify>> _events = new();

    public NotifyService(
        DbService db,
        IMessageSenderService mss,
        DiscordSocketClient client,
        IBotCreds creds,
        IReplacementService repSvc,
        IPubSub pubSub)
    {
        _db = db;
        _mss = mss;
        _client = client;
        _creds = creds;
        _repSvc = repSvc;
        _pubSub = pubSub;
    }

    public async Task OnReadyAsync()
    {
        await using var uow = _db.GetDbContext();
        _events = (await uow.GetTable<Notify>()
                            .Where(x => Linq2DbExpressions.GuildOnShard(x.GuildId,
                                _creds.TotalShards,
                                _client.ShardId))
                            .ToListAsyncLinqToDB())
                  .GroupBy(x => x.Type)
                  .ToDictionary(x => x.Key, x => x.ToDictionary(x => x.GuildId).ToConcurrent())
                  .ToConcurrent();


        await SubscribeToEvent<LevelUpNotifyModel>();
    }

    private async Task SubscribeToEvent<T>()
        where T : struct, INotifyModel
    {
        await _pubSub.Sub(new TypedKey<T>(T.KeyName), async (model) => await OnEvent(model));
    }

    public async Task NotifyAsync<T>(T data, bool isShardLocal = false)
        where T : struct, INotifyModel
    {
        try
        {
            if (isShardLocal)
            {
                await OnEvent(data);
                return;
            }

            await _pubSub.Pub(data.GetTypedKey(), data);
        }
        catch (Exception ex)
        {
            Log.Warning(ex,
                "Unknown error occurred while trying to triger {NotifyEvent} for {NotifyModel}",
                T.KeyName,
                data);
        }
    }

    private async Task OnEvent<T>(T model)
        where T : struct, INotifyModel
    {
        if (_events.TryGetValue(T.NotifyType, out var subs))
        {
            if (model.TryGetGuildId(out var gid))
            {
                if (!subs.TryGetValue(gid, out var conf))
                    return;

                await HandleNotifyEvent(conf, model);
                return;
            }

            foreach (var key in subs.Keys.ToArray())
            {
                if (subs.TryGetValue(key, out var notif))
                {
                    try
                    {
                        await HandleNotifyEvent(notif, model);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex,
                            "Error occured while sending notification {NotifyEvent} to guild {GuildId}: {ErrorMessage}",
                            T.NotifyType,
                            key,
                            ex.Message);
                    }

                    await Task.Delay(500);
                }
            }
        }
    }

    private async Task HandleNotifyEvent(Notify conf, INotifyModel model)
    {
        var guild = _client.GetGuild(conf.GuildId);
        var channel = guild?.GetTextChannel(conf.ChannelId);

        if (guild is null || channel is null)
            return;

        IUser? user = null;
        if (model.TryGetUserId(out var userId))
        {
            user = guild.GetUser(userId) ?? _client.GetUser(userId);
        }

        var rctx = new ReplacementContext(guild: guild, channel: channel, user: user);

        var st = SmartText.CreateFrom(conf.Message);
        foreach (var modelRep in model.GetReplacements())
        {
            rctx.WithOverride(modelRep.Key, () => modelRep.Value(guild));
        }

        st = await _repSvc.ReplaceAsync(st, rctx);
        if (st is SmartPlainText spt)
        {
            await _mss.Response(channel)
                      .Confirm(spt.Text)
                      .SendAsync();
            return;
        }

        await _mss.Response(channel)
                  .Text(st)
                  .SendAsync();
    }

    public async Task EnableAsync(
        ulong guildId,
        ulong channelId,
        NotifyType nType,
        string message)
    {
        await using var uow = _db.GetDbContext();
        await uow.GetTable<Notify>()
                 .InsertOrUpdateAsync(() => new()
                     {
                         GuildId = guildId,
                         ChannelId = channelId,
                         Type = nType,
                         Message = message,
                     },
                     (_) => new()
                     {
                         Message = message,
                         ChannelId = channelId
                     },
                     () => new()
                     {
                         GuildId = guildId,
                         Type = nType
                     });

        var eventDict = _events.GetOrAdd(nType, _ => new());
        eventDict[guildId] = new()
        {
            GuildId = guildId,
            ChannelId = channelId,
            Type = nType,
            Message = message
        };
    }

    public async Task DisableAsync(ulong guildId, NotifyType nType)
    {
        await using var uow = _db.GetDbContext();
        var deleted = await uow.GetTable<Notify>()
                               .Where(x => x.GuildId == guildId && x.Type == nType)
                               .DeleteAsync();

        if (deleted == 0)
            return;

        if (!_events.TryGetValue(nType, out var guildsDict))
            return;

        guildsDict.TryRemove(guildId, out _);
    }
}