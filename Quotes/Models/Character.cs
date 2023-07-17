using System.Diagnostics;
using Quotes.Storage.Interface.Objects;

namespace Quotes;

[DebuggerDisplay("{Name}")]
public class Character
{
    public static Character Unknown = new Character(StringId.Invalid, new Name("", "Unknown"));

    public string Id { get; set; } = StringId.Invalid;
    public Name Name { get; set; }
    public string? ActorId { get; set; }

    public Character() : this(string.Empty, new Name(string.Empty, string.Empty), null)
    {
    }

    public Character(string id, Name name) : this(id, name, null)
    {
    }

    public Character(string id, Name name, string? actorId)
    {
        this.Name = name;
        this.Id = id;
        this.ActorId = actorId;
    }
}
