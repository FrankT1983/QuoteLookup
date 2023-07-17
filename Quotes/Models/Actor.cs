namespace Quotes;

public class Actor
{
    public Name Name { get; set; } = new Name(string.Empty, string.Empty);

    public string Id { get; set; } = string.Empty;
    public Actor(string id, Name name)
    {
        this.Id = id;
        this.Name = name;
    }
}