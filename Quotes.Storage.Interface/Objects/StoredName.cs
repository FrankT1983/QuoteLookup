namespace Quotes.Storage.Interface.Objects;

public class StoredName
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    /// <summary>
    /// Construct a new character storage representation
    /// </summary>
    public StoredName(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }
}