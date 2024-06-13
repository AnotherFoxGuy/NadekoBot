﻿using NadekoBot.Modules.Administration;

namespace NadekoBot.Modules.Utility;

public partial class UtilityCommands
{
    public class PromptCommands : NadekoModule<IAiAssistantService>
    {
        [Cmd]
        [RequireContext(ContextType.Guild)]
        public async Task Prompt([Leftover] string query)
        {
            await ctx.Channel.TriggerTypingAsync();
            var res = await _service.TryExecuteAiCommand(ctx.Guild, ctx.Message, (ITextChannel)ctx.Channel, query);
        }

        private string GetCommandString(NadekoCommandCallModel res)
            => $"{_bcs.Data.Prefix}{res.Name} {res.Arguments.Select((x, i) => GetParamString(x, i + 1 == res.Arguments.Count)).Join(" ")}";

        private static string GetParamString(string val, bool isLast)
            => isLast ? val : "\"" + val + "\"";
    }
}