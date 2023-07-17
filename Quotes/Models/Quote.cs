using System.Text.Json.Serialization;

namespace Quotes;

public class Quote
{
    public Dialog Dialog { get; set; }

    public string Id { get; set; } = StringId.Invalid;

    public Quote()
    {
        this.Dialog = new Dialog(Array.Empty<DialogLine>());
    }

    [JsonConstructor]
    public Quote(string id, Dialog dialog)
    {
        this.Id = id;
        this.Dialog = dialog;
    }
}