#nullable disable
using Newtonsoft.Json;
using System.Globalization;

namespace NadekoBot.Services;

public class Localization : ILocalization
{
    private static readonly Dictionary<string, CommandData> _commandData =
        JsonConvert.DeserializeObject<Dictionary<string, CommandData>>(
            File.ReadAllText("./data/strings/commands/commands.en-US.json"));

    private readonly ConcurrentDictionary<ulong, CultureInfo> _guildCultureInfos;

    public IDictionary<ulong, CultureInfo> GuildCultureInfos
        => _guildCultureInfos;

    public CultureInfo DefaultCultureInfo
        => _bss.Data.DefaultLocale;

    private readonly BotConfigService _bss;
    private readonly DbService _db;

    public Localization(BotConfigService bss, Bot bot, DbService db)
    {
        _bss = bss;
        _db = db;

        var cultureInfoNames = bot.AllGuildConfigs.ToDictionary(x => x.GuildId, x => x.Locale);

        _guildCultureInfos = new(cultureInfoNames
                                 .ToDictionary(x => x.Key,
                                     x =>
                                     {
                                         CultureInfo cultureInfo = null;
                                         try
                                         {
                                             if (x.Value is null)
                                                 return null;
                                             cultureInfo = new(x.Value);
                                         }
                                         catch { }

                                         return cultureInfo;
                                     })
                                 .Where(x => x.Value is not null));
    }

    public void SetGuildCulture(IGuild guild, CultureInfo ci)
        => SetGuildCulture(guild.Id, ci);

    public void SetGuildCulture(ulong guildId, CultureInfo ci)
    {
        if (ci.Name == _bss.Data.DefaultLocale.Name)
        {
            RemoveGuildCulture(guildId);
            return;
        }

        using (var uow = _db.GetDbContext())
        {
            var gc = uow.GuildConfigsForId(guildId, set => set);
            gc.Locale = ci.Name;
            uow.SaveChanges();
        }

        _guildCultureInfos.AddOrUpdate(guildId, ci, (_, _) => ci);
    }

    public void RemoveGuildCulture(IGuild guild)
        => RemoveGuildCulture(guild.Id);

    public void RemoveGuildCulture(ulong guildId)
    {
        if (_guildCultureInfos.TryRemove(guildId, out _))
        {
            using var uow = _db.GetDbContext();
            var gc = uow.GuildConfigsForId(guildId, set => set);
            gc.Locale = null;
            uow.SaveChanges();
        }
    }

    public void SetDefaultCulture(CultureInfo ci)
        => _bss.ModifyConfig(bs =>
        {
            bs.DefaultLocale = ci;
        });

    public void ResetDefaultCulture()
        => SetDefaultCulture(CultureInfo.CurrentCulture);

    public CultureInfo GetCultureInfo(IGuild guild)
        => GetCultureInfo(guild?.Id);

    public CultureInfo GetCultureInfo(ulong? guildId)
    {
        if (guildId is null || !GuildCultureInfos.TryGetValue(guildId.Value, out var info) || info is null)
            return _bss.Data.DefaultLocale;

        return info;
    }

    public static CommandData LoadCommand(string key)
    {
        _commandData.TryGetValue(key, out var toReturn);

        if (toReturn is null)
        {
            return new()
            {
                Cmd = key,
                Desc = key,
                Usage = [key]
            };
        }

        return toReturn;
    }
}