﻿using NadekoBot.Services.Database.Models;

namespace NadekoBot.Modules.Administration.Common;

public enum ProtectionType
{
    Raiding,
    Spamming,
    Alting
}

public class AntiRaidStats
{
    public AntiRaidSetting AntiRaidSettings { get; set; }
    public int UsersCount { get; set; }
    public ConcurrentHashSet<IGuildUser> RaidUsers { get; set; } = new();
}

public class AntiSpamStats
{
    public AntiSpamSetting AntiSpamSettings { get; set; }
    public ConcurrentDictionary<ulong, UserSpamStats> UserStats { get; set; } = new();
}

public class AntiAltStats
{
    private readonly AntiAltSetting _setting;
    public PunishmentAction Action => _setting.Action;
    public int ActionDurationMinutes => _setting.ActionDurationMinutes;
    public ulong? RoleId => _setting.RoleId;
    public TimeSpan MinAge => _setting.MinAge;
        
    private int _counter = 0;
    public int Counter => _counter;

    public AntiAltStats(AntiAltSetting setting)
        => _setting = setting;

    public void Increment() => Interlocked.Increment(ref _counter);
        
}