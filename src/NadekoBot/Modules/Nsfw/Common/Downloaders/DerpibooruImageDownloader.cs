﻿using System.Net.Http.Json;
using NadekoBot.Extensions;

namespace NadekoBot.Modules.Nsfw.Common;

public class DerpibooruImageDownloader : ImageDownloader<DerpiImageObject>
{
    public DerpibooruImageDownloader(HttpClient http) : base(Booru.Derpibooru, http)
    {
    }

    public override async Task<List<DerpiImageObject>> DownloadImagesAsync(string[] tags, int page, bool isExplicit = false, CancellationToken cancel = default)
    {
        var tagString = ImageDownloaderHelper.GetTagString(tags, isExplicit);
        var uri = $"https://www.derpibooru.org/api/v1/json/search/images?q={tagString.Replace('+', ',')}&per_page=49&page={page}";
        using var req = new HttpRequestMessage(HttpMethod.Get, uri);
        req.Headers.AddFakeHeaders();
        using var res = await _http.SendAsync(req, cancel).ConfigureAwait(false);
        res.EnsureSuccessStatusCode();
            
        var container = await res.Content.ReadFromJsonAsync<DerpiContainer>(_serializerOptions, cancel).ConfigureAwait(false);
        if (container?.Images is null)
            return new();
            
        return container.Images
            .Where(x => !string.IsNullOrWhiteSpace(x.ViewUrl))
            .ToList();
    }
}