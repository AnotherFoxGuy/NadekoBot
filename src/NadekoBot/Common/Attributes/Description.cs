﻿namespace NadekoBot.Common.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public sealed class DescriptionAttribute : SummaryAttribute
{
    // Localization.LoadCommand(memberName.ToLowerInvariant()).Desc
    public DescriptionAttribute(string text = "") : base(text)
    {
    }
}