using System.Diagnostics;

namespace Quotes;

[DebuggerDisplay("{FirstName} {LastName}")]
public class Name
{
    public Name(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool Is(string characterName)
    {
        // also needs logic for full name and fuzzy search
        return this.FirstName.Equals(characterName, StringComparison.InvariantCultureIgnoreCase) ||
               this.LastName.Equals(characterName, StringComparison.InvariantCultureIgnoreCase);
    }
}