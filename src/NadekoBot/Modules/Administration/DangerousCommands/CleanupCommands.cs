﻿using NadekoBot.Modules.Administration.DangerousCommands;

namespace NadekoBot.Modules.Administration;

public partial class Administration 
{
    [Group]
    public partial class CleanupCommands : CleanupModuleBase
    {
        private readonly ICleanupService _svc;

        public CleanupCommands(ICleanupService svc)
            => _svc = svc;

        [Cmd]
        [OwnerOnly]
        [RequireContext(ContextType.DM)]
        public async Task CleanupGuildData()
        {
            var result = await _svc.DeleteMissingGuildDataAsync();

            if (result is null)
            {
                await ctx.ErrorAsync();
                return;
            }

            await Response()
                  .Confirm($"{result.GuildCount} guilds' data remain in the database.")
                  .SendAsync();
        }

        [Cmd]
        [RequireContext(ContextType.Guild)]
        [UserPerm(GuildPerm.Administrator)]
        public async Task Keep()
        {
            var result = await _svc.KeepGuild(Context.Guild.Id);

            await Response().Text("This guild's bot data will be saved.").SendAsync();
        }
    }
}