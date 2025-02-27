﻿#nullable disable
namespace NadekoBot.Modules.Utility.Services;

public class VerboseErrorsService : INService
{
    private readonly ConcurrentHashSet<ulong> _guildsDisabled;
    private readonly DbService _db;
    private readonly CommandHandler _ch;
    private readonly ICommandsUtilityService _hs;
    private readonly IMessageSenderService _sender;

    public VerboseErrorsService(
        IBot bot,
        DbService db,
        CommandHandler ch,
        IMessageSenderService sender,
        ICommandsUtilityService hs)
    {
        _db = db;
        _ch = ch;
        _hs = hs;
        _sender = sender;

        _ch.CommandErrored += LogVerboseError;

        _guildsDisabled = new(bot.AllGuildConfigs.Where(x => !x.VerboseErrors).Select(x => x.GuildId));
    }

    private async Task LogVerboseError(CommandInfo cmd, ITextChannel channel, string reason)
    {
        if (channel is null || _guildsDisabled.Contains(channel.GuildId))
            return;

        try
        {
            var embed = _hs.GetCommandHelp(cmd, channel.Guild)
                           .WithTitle("Command Error")
                           .WithDescription(reason)
                           .WithFooter("Admin may disable verbose errors via `.ve` command")
                           .WithErrorColor();

            await _sender.Response(channel).Embed(embed).SendAsync();
        }
        catch
        {
            Log.Information("Verbose error wasn't able to be sent to the server: {GuildId}",
                channel.GuildId);
        }
    }

    public bool ToggleVerboseErrors(ulong guildId, bool? maybeEnabled = null)
    {
        using var uow = _db.GetDbContext();
        var gc = uow.GuildConfigsForId(guildId, set => set);

        if (maybeEnabled is bool isEnabled) // set it
            gc.VerboseErrors = isEnabled;
        else // toggle it
            isEnabled = gc.VerboseErrors = !gc.VerboseErrors; 

        uow.SaveChanges();

        if (isEnabled) // This doesn't need to be duplicated inside the using block
            _guildsDisabled.TryRemove(guildId);
        else
            _guildsDisabled.Add(guildId);

        return isEnabled;
    }
}