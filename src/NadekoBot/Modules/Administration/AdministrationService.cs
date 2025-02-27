﻿#nullable disable
using Microsoft.EntityFrameworkCore;
using NadekoBot.Db.Models;
using NadekoBot.Modules.Administration._common.results;

namespace NadekoBot.Modules.Administration;

public class AdministrationService : INService
{
    public ConcurrentHashSet<ulong> DeleteMessagesOnCommand { get; }
    public ConcurrentDictionary<ulong, bool> DeleteMessagesOnCommandChannels { get; }

    private readonly DbService _db;
    private readonly IReplacementService _repSvc;
    private readonly ILogCommandService _logService;
    private readonly IHttpClientFactory _httpFactory;

    public AdministrationService(
        IBot bot,
        CommandHandler cmdHandler,
        DbService db,
        IReplacementService repSvc,
        ILogCommandService logService,
        IHttpClientFactory factory)
    {
        _db = db;
        _repSvc = repSvc;
        _logService = logService;
        _httpFactory = factory;

        DeleteMessagesOnCommand = new(bot.AllGuildConfigs.Where(g => g.DeleteMessageOnCommand).Select(g => g.GuildId));

        DeleteMessagesOnCommandChannels = new(bot.AllGuildConfigs.SelectMany(x => x.DelMsgOnCmdChannels)
                                                 .ToDictionary(x => x.ChannelId, x => x.State)
                                                 .ToConcurrent());

        cmdHandler.CommandExecuted += DelMsgOnCmd_Handler;
    }

    public (bool DelMsgOnCmd, IEnumerable<DelMsgOnCmdChannel> channels) GetDelMsgOnCmdData(ulong guildId)
    {
        using var uow = _db.GetDbContext();
        var conf = uow.GuildConfigsForId(guildId, set => set.Include(x => x.DelMsgOnCmdChannels));

        return (conf.DeleteMessageOnCommand, conf.DelMsgOnCmdChannels);
    }

    private Task DelMsgOnCmd_Handler(IUserMessage msg, CommandInfo cmd)
    {
        if (msg.Channel is not ITextChannel channel)
            return Task.CompletedTask;
        
        _ = Task.Run(async () =>
        {
            //wat ?!
            if (DeleteMessagesOnCommandChannels.TryGetValue(channel.Id, out var state))
            {
                if (state && cmd.Name != "prune" && cmd.Name != "pick")
                {
                    _logService.AddDeleteIgnore(msg.Id);
                    try { await msg.DeleteAsync(); }
                    catch { }
                }
                //if state is false, that means do not do it
            }
            else if (DeleteMessagesOnCommand.Contains(channel.Guild.Id) && cmd.Name != "prune" && cmd.Name != "pick")
            {
                _logService.AddDeleteIgnore(msg.Id);
                try { await msg.DeleteAsync(); }
                catch { }
            }
        });
        return Task.CompletedTask;
    }

    public bool ToggleDeleteMessageOnCommand(ulong guildId)
    {
        bool enabled;
        using var uow = _db.GetDbContext();
        var conf = uow.GuildConfigsForId(guildId, set => set);
        enabled = conf.DeleteMessageOnCommand = !conf.DeleteMessageOnCommand;

        uow.SaveChanges();
        return enabled;
    }

    public async Task SetDelMsgOnCmdState(ulong guildId, ulong chId, Administration.State newState)
    {
        await using (var uow = _db.GetDbContext())
        {
            var conf = uow.GuildConfigsForId(guildId, set => set.Include(x => x.DelMsgOnCmdChannels));

            var old = conf.DelMsgOnCmdChannels.FirstOrDefault(x => x.ChannelId == chId);
            if (newState == Administration.State.Inherit)
            {
                if (old is not null)
                {
                    conf.DelMsgOnCmdChannels.Remove(old);
                    uow.Remove(old);
                }
            }
            else
            {
                if (old is null)
                {
                    old = new()
                    {
                        ChannelId = chId
                    };
                    conf.DelMsgOnCmdChannels.Add(old);
                }

                old.State = newState == Administration.State.Enable;
                DeleteMessagesOnCommandChannels[chId] = newState == Administration.State.Enable;
            }

            await uow.SaveChangesAsync();
        }

        if (newState == Administration.State.Disable)
        {
        }
        else if (newState == Administration.State.Enable)
            DeleteMessagesOnCommandChannels[chId] = true;
        else
            DeleteMessagesOnCommandChannels.TryRemove(chId, out _);
    }

    public async Task DeafenUsers(bool value, params IGuildUser[] users)
    {
        if (!users.Any())
            return;
        foreach (var u in users)
        {
            try
            {
                await u.ModifyAsync(usr => usr.Deaf = value);
            }
            catch
            {
                // ignored
            }
        }
    }

    public async Task EditMessage(
        ICommandContext context,
        ITextChannel chanl,
        ulong messageId,
        string input)
    {
        var msg = await chanl.GetMessageAsync(messageId);

        if (msg is not IUserMessage umsg || msg.Author.Id != context.Client.CurrentUser.Id)
            return;

        var repCtx = new ReplacementContext(context);

        var text = SmartText.CreateFrom(input);
        text = await _repSvc.ReplaceAsync(text, repCtx);

        await umsg.EditAsync(text);
    }

    public async Task<SetServerBannerResult> SetServerBannerAsync(IGuild guild, string img)
    {
        if (!IsValidUri(img)) return SetServerBannerResult.InvalidURL;
        
        var uri = new Uri(img);

        using var http = _httpFactory.CreateClient();
        using var sr = await http.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
        
        if (!sr.IsImage()) return SetServerBannerResult.InvalidFileType;

        if (sr.GetContentLength() > 8.Megabytes())
        {
            return SetServerBannerResult.Toolarge;
        }
        
        await using var imageStream = await sr.Content.ReadAsStreamAsync();

        await guild.ModifyAsync(x => x.Banner = new Image(imageStream));
        return SetServerBannerResult.Success;
    }

    public async Task<SetServerIconResult> SetServerIconAsync(IGuild guild, string img)
    {
        if (!IsValidUri(img)) return SetServerIconResult.InvalidURL;
        
        var uri = new Uri(img);

        using var http = _httpFactory.CreateClient();
        using var sr = await http.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
        
        if (!sr.IsImage()) return SetServerIconResult.InvalidFileType;
        
        await using var imageStream = await sr.Content.ReadAsStreamAsync();

        await guild.ModifyAsync(x => x.Icon = new Image(imageStream));
        return SetServerIconResult.Success;
    }
 
    private bool IsValidUri(string img) => !string.IsNullOrWhiteSpace(img) && Uri.IsWellFormedUriString(img, UriKind.Absolute);
}