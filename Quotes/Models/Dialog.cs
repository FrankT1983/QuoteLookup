using System.Text.Json.Serialization;

namespace Quotes;

public class Dialog
{
    public IEnumerable<DialogLine> Lines { get; set; } = new List<DialogLine>();

    [JsonConstructor]
    public Dialog(IEnumerable<DialogLine> lines)
    {
        // todo: consider deep copy here
        // potentially enforce at least on primary line
        this.Lines = lines;
    }
}