﻿using LinqToDB;
using LinqToDB.Data;
using LinqToDB.EntityFrameworkCore;
using LinqToDB.Mapping;
using LinqToDB.Tools;
using NadekoBot.Common.ModuleBehaviors;
using NadekoBot.Db.Models;
using System.Security.Cryptography;

namespace NadekoBot.Modules.Administration.DangerousCommands;

public sealed class CleanupService : ICleanupService, IReadyExecutor, INService
{
    private TypedKey<KeepReport> _cleanupReportKey = new("cleanup:report");
    private TypedKey<bool> _cleanupTriggerKey = new("cleanup:trigger");

    private TypedKey<bool> _keepTriggerKey = new("keep:trigger");

    private readonly IPubSub _pubSub;
    private readonly DiscordSocketClient _client;
    private ConcurrentDictionary<int, ulong[]> guildIds = new();
    private readonly IBotCredsProvider _creds;
    private readonly DbService _db;

    public CleanupService(
        IPubSub pubSub,
        DiscordSocketClient client,
        IBotCredsProvider creds,
        DbService db)
    {
        _pubSub = pubSub;
        _client = client;
        _creds = creds;
        _db = db;
    }

    public async Task OnReadyAsync()
    {
        await _pubSub.Sub(_cleanupTriggerKey, OnCleanupTrigger);
        await _pubSub.Sub(_keepTriggerKey, InternalTriggerKeep);

        _client.JoinedGuild += ClientOnJoinedGuild;

        if (_client.ShardId == 0)
            await _pubSub.Sub(_cleanupReportKey, OnKeepReport);
    }

    private bool keepTriggered = false;

    private async ValueTask InternalTriggerKeep(bool arg)
    {
        if (keepTriggered)
            return;

        keepTriggered = true;
        try
        {
            await Task.Delay(10 + (10 * _client.ShardId));

            var allGuildIds = _client.Guilds.Select(x => x.Id);

            var table = await GetKeptGuildsTable();

            var dontDeleteList = await table
                                       .Where(x => allGuildIds.Contains(x.GuildId))
                                       .Select(x => x.GuildId)
                                       .ToListAsyncLinqToDB();

            var dontDelete = dontDeleteList.ToHashSet();

            guildIds = new();
            foreach (var guildId in allGuildIds)
            {
                if (dontDelete.Contains(guildId))
                    continue;

                // 1 leave per 20 seconds per shard
                await Task.Delay(RandomNumberGenerator.GetInt32(18_000, 22_000));

                SocketGuild? guild = null;
                try
                {
                    guild = _client.GetGuild(guildId);

                    if (guild is null)
                    {
                        Log.Warning("Unable to find guild {GuildId}", guildId);
                        continue;
                    }

                    await guild.LeaveAsync();
                }
                catch (Exception ex)
                {
                    Log.Warning("Unable to leave guild {GuildName} [{GuildId}]: {ErrorMessage}",
                        guild?.Name,
                        guildId,
                        ex.Message);
                }
            }
        }
        finally
        {
            keepTriggered = false;
        }
    }

    public async Task<KeepResult?> DeleteMissingGuildDataAsync()
    {
        guildIds = new();
        var totalShards = _creds.GetCreds().TotalShards;
        await _pubSub.Pub(_cleanupTriggerKey, true);
        var counter = 0;
        while (guildIds.Keys.Count < totalShards)
        {
            await Task.Delay(1000);
            counter++;

            if (counter >= 5)
                break;
        }

        if (guildIds.Keys.Count < totalShards)
            return default;

        var allIds = guildIds.SelectMany(x => x.Value)
                             .ToArray();

        await using var ctx = _db.GetDbContext();
        await using var linqCtx = ctx.CreateLinqToDBContext();
        await using var tempTable = linqCtx.CreateTempTable<CleanupId>();

        foreach (var chunk in allIds.Chunk(20000))
        {
            await tempTable.BulkCopyAsync(chunk.Select(x => new CleanupId()
            {
                GuildId = x
            }));
        }

        // delete guild configs
        await ctx.GetTable<GuildConfig>()
                 .Where(x => !tempTable.Select(x => x.GuildId)
                                       .Contains(x.GuildId))
                 .DeleteAsync();

        // delete guild xp
        await ctx.GetTable<UserXpStats>()
                 .Where(x => !tempTable.Select(x => x.GuildId)
                                       .Contains(x.GuildId))
                 .DeleteAsync();

        // delete expressions
        await ctx.GetTable<NadekoExpression>()
                 .Where(x => x.GuildId != null
                             && !tempTable.Select(x => x.GuildId)
                                          .Contains(x.GuildId.Value))
                 .DeleteAsync();

        // delete quotes
        await ctx.GetTable<Quote>()
                 .Where(x => !tempTable.Select(x => x.GuildId)
                                       .Contains(x.GuildId))
                 .DeleteAsync();

        // delete planted currencies
        await ctx.GetTable<PlantedCurrency>()
                 .Where(x => !tempTable.Select(x => x.GuildId)
                                       .Contains(x.GuildId))
                 .DeleteAsync();

        // delete image only channels
        await ctx.GetTable<ImageOnlyChannel>()
                 .Where(x => !tempTable.Select(x => x.GuildId)
                                       .Contains(x.GuildId))
                 .DeleteAsync();

        // delete reaction roles
        await ctx.GetTable<ReactionRoleV2>()
                 .Where(x => !tempTable.Select(x => x.GuildId)
                                       .Contains(x.GuildId))
                 .DeleteAsync();

        // delete ignored users
        await ctx.GetTable<DiscordPermOverride>()
                 .Where(x => x.GuildId != null
                             && !tempTable.Select(x => x.GuildId)
                                          .Contains(x.GuildId.Value))
                 .DeleteAsync();

        // delete perm overrides
        await ctx.GetTable<DiscordPermOverride>()
                 .Where(x => x.GuildId != null
                             && !tempTable.Select(x => x.GuildId)
                                          .Contains(x.GuildId.Value))
                 .DeleteAsync();

        // delete repeaters
        await ctx.GetTable<Repeater>()
                 .Where(x => !tempTable.Select(x => x.GuildId)
                                       .Contains(x.GuildId))
                 .DeleteAsync();

        return new()
        {
            GuildCount = guildIds.Keys.Count,
        };
    }

    public async Task<bool> KeepGuild(ulong guildId)
    {
        var table = await GetKeptGuildsTable();

        if (await table.AnyAsyncLinqToDB(x => x.GuildId == guildId))
            return false;

        await table.InsertAsync(() => new()
        {
            GuildId = guildId
        });

        return true;
    }

    public async Task<int> GetKeptGuildCount()
    {
        var table = await GetKeptGuildsTable();
        return await table.CountAsync();
    }

    private async Task<ITable<KeptGuilds>> GetKeptGuildsTable()
    {
        await using var db = _db.GetDbContext();
        await using var ctx = db.CreateLinqToDBContext();
        var table = ctx.CreateTable<KeptGuilds>(tableOptions: TableOptions.CheckExistence);
        return table;
    }

    public async Task LeaveUnkeptServers()
        => await _pubSub.Pub(_keepTriggerKey, true);

    private ValueTask OnKeepReport(KeepReport report)
    {
        guildIds[report.ShardId] = report.GuildIds;
        return default;
    }

    private async Task ClientOnJoinedGuild(SocketGuild arg)
    {
        await KeepGuild(arg.Id);
    }

    private ValueTask OnCleanupTrigger(bool arg)
    {
        _pubSub.Pub(_cleanupReportKey,
            new KeepReport()
            {
                ShardId = _client.ShardId,
                GuildIds = _client.GetGuildIds(),
            });

        return default;
    }
}

public class KeptGuilds
{
    [PrimaryKey]
    public ulong GuildId { get; set; }
}