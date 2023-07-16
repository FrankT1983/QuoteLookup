namespace Quotes.Storage.Interface.Objects;

public class StoredQuote
{
    public List<StoredDialogLine> Lines { get; set; }
    public string Id { get; private set; }

    /// <summary>
    /// Construct a new quote storage representation
    /// </summary>
    public StoredQuote(string id, IEnumerable<StoredDialogLine> lines)
    {
        this.Id = id;
        this.Lines = new List<StoredDialogLine>(lines);
    }
}