﻿using Cloneable;
using NadekoBot.Common.Yml;

namespace NadekoBot.Modules.Games;

[Cloneable]
public sealed partial class FishConfig : ICloneable<FishConfig>
{
    [Comment("DO NOT CHANGE")]
    public int Version { get; set; } = 1;

    public string WeatherSeed { get; set; } = string.Empty;
    public List<string> StarEmojis { get; set; } = new();
    public List<string> SpotEmojis { get; set; } = new();
    public FishChance Chance { get; set; } = new FishChance();

    public List<FishData> Fish { get; set; } = new();
    public List<FishData> Trash { get; set; } = new();
}