﻿using Humanizer.Localisation;
using NadekoBot.Modules.Administration.Services;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using Color = Discord.Color;
using JsonSerializer = System.Text.Json.JsonSerializer;

// todo imagesharp extensions
namespace NadekoBot.Extensions;

public static class Extensions
{
    private static readonly Regex _urlRegex =
        new(@"^(https?|ftp)://(?<path>[^\s/$.?#].[^\s]*)$", RegexOptions.Compiled);

    public static Task EditAsync(this IUserMessage msg, SmartText text)
        => text switch
        {
            SmartEmbedText set => msg.ModifyAsync(x =>
                {
                    x.Embed = set.GetEmbed().Build();
                    x.Content = set.PlainText?.SanitizeMentions() ?? "";
                }
            ),
            SmartPlainText spt => msg.ModifyAsync(x =>
                {
                    x.Content = spt.Text.SanitizeMentions();
                    x.Embed = null;
                }
            ),
            _ => throw new ArgumentOutOfRangeException(nameof(text))
        };

    public static List<ulong> GetGuildIds(this DiscordSocketClient client)
        => client.Guilds.Select(x => x.Id).ToList();

    /// <summary>
    /// Generates a string in the format HHH:mm if timespan is &gt;= 2m.
    /// Generates a string in the format 00:mm:ss if timespan is less than 2m.
    /// </summary>
    /// <param name="span">Timespan to convert to string</param>
    /// <returns>Formatted duration string</returns>
    public static string ToPrettyStringHm(this TimeSpan span)
        => span.Humanize(2, minUnit: TimeUnit.Second);

    public static bool TryGetUrlPath(this string input, out string path)
    {
        var match = _urlRegex.Match(input);
        if (match.Success)
        {
            path = match.Groups["path"].Value;
            return true;
        }

        path = string.Empty;
        return false;
    }

    public static IEmote ToIEmote(this string emojiStr)
        => Emote.TryParse(emojiStr, out var maybeEmote) ? maybeEmote : new Emoji(emojiStr);

    // https://github.com/SixLabors/Samples/blob/master/ImageSharp/AvatarWithRoundedCorner/Program.cs
    public static IImageProcessingContext ApplyRoundedCorners(this IImageProcessingContext ctx, float cornerRadius)
    {
        var size = ctx.GetCurrentSize();
        var corners = BuildCorners(size.Width, size.Height, cornerRadius);

        ctx.SetGraphicsOptions(new GraphicsOptions()
            {
                Antialias = true,
                // enforces that any part of this shape that has color is punched out of the background
                AlphaCompositionMode = PixelAlphaCompositionMode.DestOut
            }
        );

        foreach (var c in corners)
        {
            ctx = ctx.Fill(SixLabors.ImageSharp.Color.Red, c);
        }

        return ctx;
    }

    private static IPathCollection BuildCorners(int imageWidth, int imageHeight, float cornerRadius)
    {
        // first create a square
        var rect = new RectangularPolygon(-0.5f,
            -0.5f,
            cornerRadius,
            cornerRadius
        );

        // then cut out of the square a circle so we are left with a corner
        var cornerTopLeft = rect.Clip(new EllipsePolygon(cornerRadius - 0.5f, cornerRadius - 0.5f, cornerRadius));

        // corner is now a corner shape positions top left
        //lets make 3 more positioned correctly, we can do that by translating the original around the center of the image

        var rightPos = imageWidth - cornerTopLeft.Bounds.Width + 1;
        var bottomPos = imageHeight - cornerTopLeft.Bounds.Height + 1;

        // move it across the width of the image - the width of the shape
        var cornerTopRight = cornerTopLeft.RotateDegree(90).Translate(rightPos, 0);
        var cornerBottomLeft = cornerTopLeft.RotateDegree(-90).Translate(0, bottomPos);
        var cornerBottomRight = cornerTopLeft.RotateDegree(180).Translate(rightPos, bottomPos);

        return new PathCollection(cornerTopLeft,
            cornerBottomLeft,
            cornerTopRight,
            cornerBottomRight
        );
    }

    /// <summary>
    /// First 10 characters of teh bot token.
    /// </summary>
    public static string RedisKey(this IBotCredentials bc)
        => bc.Token[..10];

    public static bool IsAuthor(this IMessage msg, IDiscordClient client)
        => msg.Author?.Id == client.CurrentUser.Id;

    public static string RealSummary(
        this CommandInfo cmd,
        IBotStrings strings,
        ulong? guildId,
        string prefix)
        => string.Format(strings.GetCommandStrings(cmd.Name, guildId).Desc, prefix);

    public static string[] RealRemarksArr(
        this CommandInfo cmd,
        IBotStrings strings,
        ulong? guildId,
        string prefix)
        => Array.ConvertAll(strings.GetCommandStrings(cmd.MethodName(), guildId).Args,
            arg => GetFullUsage(cmd.Name, arg, prefix)
        );

    private static string MethodName(this CommandInfo cmd)
        => ((NadekoCommandAttribute)cmd.Attributes.FirstOrDefault(x => x is NadekoCommandAttribute))?.MethodName ??
           cmd.Name;

    private static string GetFullUsage(string commandName, string args, string prefix)
        => $"{prefix}{commandName} {string.Format(args, prefix)}";

    public static IEmbedBuilder AddPaginatedFooter(this IEmbedBuilder embed, int curPage, int? lastPage)
    {
        if (lastPage != null)
            return embed.WithFooter($"{curPage + 1} / {lastPage + 1}");
        else
            return embed.WithFooter(curPage.ToString());
    }

    public static Color ToDiscordColor(this Rgba32 color)
        => new(color.R, color.G, color.B);

    public static IEmbedBuilder WithOkColor(this IEmbedBuilder eb)
        => eb.WithColor(EmbedColor.Ok);

    public static IEmbedBuilder WithPendingColor(this IEmbedBuilder eb)
        => eb.WithColor(EmbedColor.Pending);

    public static IEmbedBuilder WithErrorColor(this IEmbedBuilder eb)
        => eb.WithColor(EmbedColor.Error);

    public static ReactionEventWrapper OnReaction(
        this IUserMessage msg,
        DiscordSocketClient client,
        Func<SocketReaction, Task> reactionAdded,
        Func<SocketReaction, Task> reactionRemoved = null)
    {
        if (reactionRemoved is null)
            reactionRemoved = _ => Task.CompletedTask;

        var wrap = new ReactionEventWrapper(client, msg);
        wrap.OnReactionAdded += r =>
        {
            var _ = Task.Run(() => reactionAdded(r));
        };
        wrap.OnReactionRemoved += r =>
        {
            var _ = Task.Run(() => reactionRemoved(r));
        };
        return wrap;
    }

    public static HttpClient AddFakeHeaders(this HttpClient http)
    {
        AddFakeHeaders(http.DefaultRequestHeaders);
        return http;
    }

    public static void AddFakeHeaders(this HttpHeaders dict)
    {
        dict.Clear();
        dict.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        dict.Add("User-Agent",
            "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1"
        );
    }

    public static IMessage DeleteAfter(this IUserMessage msg, int seconds, ILogCommandService logService = null)
    {
        if (msg is null)
            return null;

        Task.Run(async () =>
            {
                await Task.Delay(seconds * 1000).ConfigureAwait(false);
                if (logService != null)
                {
                    logService.AddDeleteIgnore(msg.Id);
                }

                try { await msg.DeleteAsync().ConfigureAwait(false); }
                catch { }
            }
        );
        return msg;
    }

    public static ModuleInfo GetTopLevelModule(this ModuleInfo module)
    {
        while (module.Parent != null)
        {
            module = module.Parent;
        }

        return module;
    }

    public static async Task<IEnumerable<IGuildUser>> GetMembersAsync(this IRole role)
    {
        var users = await role.Guild.GetUsersAsync(CacheMode.CacheOnly).ConfigureAwait(false);
        return users.Where(u => u.RoleIds.Contains(role.Id));
    }

    public static string ToJson<T>(this T any, JsonSerializerOptions options = null)
        => JsonSerializer.Serialize(any, options);

    /// <summary>
    /// Adds fallback fonts to <see cref="TextOptions"/>
    /// </summary>
    /// <param name="opts"><see cref="TextOptions"/> to which fallback fonts will be added to</param>
    /// <param name="fallback">List of fallback Font Families to add</param>
    /// <returns>The same <see cref="TextOptions"/> to allow chaining</returns>
    public static TextOptions WithFallbackFonts(this TextOptions opts, List<FontFamily> fallback)
    {
        foreach (var ff in fallback)
        {
            opts.FallbackFonts.Add(ff);
        }

        return opts;
    }

    /// <summary>
    /// Adds fallback fonts to <see cref="TextGraphicsOptions"/>
    /// </summary>
    /// <param name="opts"><see cref="TextGraphicsOptions"/> to which fallback fonts will be added to</param>
    /// <param name="fallback">List of fallback Font Families to add</param>
    /// <returns>The same <see cref="TextGraphicsOptions"/> to allow chaining</returns>
    public static TextGraphicsOptions WithFallbackFonts(this TextGraphicsOptions opts, List<FontFamily> fallback)
    {
        opts.TextOptions.WithFallbackFonts(fallback);
        return opts;
    }

    public static MemoryStream ToStream(this Image<Rgba32> img, IImageFormat format = null)
    {
        var imageStream = new MemoryStream();
        if (format?.Name == "GIF")
        {
            img.SaveAsGif(imageStream);
        }
        else
        {
            img.SaveAsPng(imageStream,
                new() { ColorType = PngColorType.RgbWithAlpha, CompressionLevel = PngCompressionLevel.BestCompression }
            );
        }

        imageStream.Position = 0;
        return imageStream;
    }

    public static Stream ToStream(this IEnumerable<byte> bytes, bool canWrite = false)
    {
        var ms = new MemoryStream(bytes as byte[] ?? bytes.ToArray(), canWrite);
        ms.Seek(0, SeekOrigin.Begin);
        return ms;
    }

    public static IEnumerable<IRole> GetRoles(this IGuildUser user)
        => user.RoleIds.Select(r => user.Guild.GetRole(r)).Where(r => r != null);

    public static bool IsImage(this HttpResponseMessage msg)
        => IsImage(msg, out _);

    public static bool IsImage(this HttpResponseMessage msg, out string mimeType)
    {
        mimeType = msg.Content.Headers.ContentType?.MediaType;
        if (mimeType is "image/png" or "image/jpeg" or "image/gif")
        {
            return true;
        }

        return false;
    }

    public static long? GetImageSize(this HttpResponseMessage msg)
    {
        if (msg.Content.Headers.ContentLength is null)
        {
            return null;
        }

        return msg.Content.Headers.ContentLength.Value / 1.Mb();
    }

    public static string GetText(this IBotStrings strings, in LocStr str, ulong? guildId = null)
        => strings.GetText(str.Key, guildId, str.Params);

    public static string GetText(this IBotStrings strings, in LocStr str, CultureInfo culture)
        => strings.GetText(str.Key, culture, str.Params);
}