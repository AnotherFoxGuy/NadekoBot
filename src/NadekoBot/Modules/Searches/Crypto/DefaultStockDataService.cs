﻿using AngleSharp;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text.Json;

namespace NadekoBot.Modules.Searches;

public sealed class DefaultStockDataService : IStockDataService, INService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DefaultStockDataService(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;

    public async Task<StockData?> GetStockDataAsync(string query)
    {
        try
        {
            if (!query.IsAlphaNumeric())
                return default;

            using var http = _httpClientFactory.CreateClient();

            var quoteHtmlPage = $"https://finance.yahoo.com/quote/{query.ToUpperInvariant()}";

            var config = Configuration.Default.WithDefaultLoader();
            using var document = await BrowsingContext.New(config).OpenAsync(quoteHtmlPage);

            var tickerName = document.QuerySelector("div.top > .left > .container > h1")
                                     ?.TextContent;
            
            if (tickerName is null)
                return default;
            
            var marketcap = document
                            .QuerySelector("li > span > fin-streamer[data-field='marketCap']")
                            ?.TextContent;


            var volume = document.QuerySelector("li > span > fin-streamer[data-field='regularMarketVolume']")
                                 ?.TextContent;

            var close = document.QuerySelector("li > span > fin-streamer[data-field='regularMarketPreviousClose']")
                                ?.TextContent
                        ?? "0";

            var price = document.QuerySelector("fin-streamer.livePrice > span")
                                ?.TextContent
                        ?? "0";

            return new()
            {
                Name = tickerName,
                Symbol = query,
                Price = double.Parse(price, NumberStyles.Any, CultureInfo.InvariantCulture),
                Close = double.Parse(close, NumberStyles.Any, CultureInfo.InvariantCulture),
                MarketCap = marketcap,
                DailyVolume = (long)double.Parse(volume ?? "0", NumberStyles.Any, CultureInfo.InvariantCulture),
            };
        }
        catch (Exception ex)
        {
            Log.Warning(ex, "Error getting stock data: {ErrorMessage}", ex.ToString());
            return default;
        }
    }

    public async Task<IReadOnlyCollection<SymbolData>> SearchSymbolAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentNullException(nameof(query));

        query = Uri.EscapeDataString(query);

        using var http = _httpClientFactory.CreateClient();

        var res = await http.GetStringAsync(
            "https://finance.yahoo.com/_finance_doubledown/api/resource/searchassist"
            + $";searchTerm={query}"
            + "?device=console");

        var data = JsonSerializer.Deserialize<YahooFinanceSearchResponse>(res);

        if (data is null or { Items: null })
            return Array.Empty<SymbolData>();

        return data.Items
                   .Where(x => x.Type == "S")
                   .Select(x => new SymbolData(x.Symbol, x.Name))
                   .ToList();
    }

    private static CsvConfiguration _csvConfig = new(CultureInfo.InvariantCulture);

    public async Task<IReadOnlyCollection<CandleData>> GetCandleDataAsync(string query)
    {
        using var http = _httpClientFactory.CreateClient();
        await using var resStream = await http.GetStreamAsync(
            $"https://query1.finance.yahoo.com/v7/finance/download/{query}"
            + $"?period1={DateTime.UtcNow.Subtract(30.Days()).ToTimestamp()}"
            + $"&period2={DateTime.UtcNow.ToTimestamp()}"
            + "&interval=1d");

        using var textReader = new StreamReader(resStream);
        using var csv = new CsvReader(textReader, _csvConfig);
        var records = csv.GetRecords<YahooFinanceCandleData>().ToArray();

        return records
            .Map(static x => new CandleData(x.Open, x.Close, x.High, x.Low, x.Volume));
    }
}