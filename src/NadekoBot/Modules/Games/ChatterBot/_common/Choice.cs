﻿using System.Text.Json.Serialization;

namespace NadekoBot.Modules.Games.Common.ChatterBot;

public class Choice
{
    [JsonPropertyName("message")]
    public Message Message { get; init; }
}