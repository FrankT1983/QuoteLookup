namespace Quotes.Storage.Interface.Objects;

public class StoredCharacter
{
    public string Id { get; private set; }
    public StoredName Name { get; private set; }

    public string? ActorId { get; private set; }

    // public string MovieId { get; private set; } // don't do this for now, just makes everything harder

    /// <summary>
    /// Construct a new character storage representation
    /// </summary>
    public StoredCharacter(string id, StoredName name, string? actorId)
    {
        this.Id = id;
        this.Name = name;
        this.ActorId = actorId;
    }
}