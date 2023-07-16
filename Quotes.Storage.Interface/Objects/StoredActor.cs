namespace Quotes.Storage.Interface.Objects;

public class StoredActor : IHasId
{
    public StoredName Name { get; private set; }

    public string Id { get; private set; }


    public StoredActor(string id, StoredName name)
    {
        this.Id = id;
        this.Name = name;
    }
}