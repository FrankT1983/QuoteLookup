using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Quotes;

[DebuggerDisplay("{Line}")]
public class DialogLine
{
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    //public DialogLineType Type { get; set; }

    public string Line { get; set; }

    public Character Character { get; set; }

    public double SecondsAfterStart { get; set; }

    public DialogLine(TimeSpan timestamp, Character character, string line)
    {
        this.SecondsAfterStart = timestamp.TotalSeconds;
        this.Character = character;
        this.Line = line;
    }

    [JsonConstructor]
    public DialogLine(double secondsAfterStart, Character character, string line)
    {
        this.SecondsAfterStart = secondsAfterStart;
        this.Character = character;
        this.Line = line;
    }
}