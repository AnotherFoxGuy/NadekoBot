#nullable disable
using Newtonsoft.Json;

namespace NadekoBot.Modules.Searches.Common;

// todo replace newtonsoft with json.text
public class BibleVerses
{
    public string Error { get; set; }
    public BibleVerse[] Verses { get; set; }
}

public class BibleVerse
{
    [JsonProperty("book_name")]
    public string BookName { get; set; }

    public int Chapter { get; set; }
    public int Verse { get; set; }
    public string Text { get; set; }
}