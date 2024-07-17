#nullable disable
using NadekoBot.Db;
using NadekoBot.Modules.Games.Services;
using NadekoBot.Db.Models;

namespace NadekoBot.Modules.Games;

public partial class Games
{
    [Group]
    public partial class ChatterBotCommands : NadekoModule<ChatterBotService>
    {
        private readonly DbService _db;

        public ChatterBotCommands(DbService db)
            => _db = db;

        [Cmd]
        [RequireContext(ContextType.Guild)]
        [UserPerm(GuildPerm.ManageMessages)]
        public async Task CleverBot()
        {
            var channel = (ITextChannel)ctx.Channel;

            var newState = await _service.ToggleChatterBotAsync(ctx.Guild.Id);

            if (!newState)
            {
                await Response().Confirm(strs.chatbot_disabled).SendAsync();
                return;
            }

            await Response().Confirm(strs.chatbot_enabled).SendAsync();
            
        }
    }
}