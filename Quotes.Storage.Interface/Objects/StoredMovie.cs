namespace Quotes.Storage.Interface.Objects;

public class StoredMovie : IHasId
{
    public string Name { get; private set; }

    public string Id { get; private set; }

    public IEnumerable<StoredCharacter> Characters { get; private set; }

    public IEnumerable<StoredQuote> Quotes { get; private set; }

    /// <summary>
    /// Construct a new move storage representation
    /// </summary>
    /// <param name="name">Name of the movie (dummy for now)</param>
    /// <param name="id">use IMDB id as an ID for now. Might not be the best idea but feels better then just creating a uuid</param>
    /// <param name="characters"></param>
    /// <param name="quotes"></param>
    public StoredMovie(string id, string name, IEnumerable<StoredCharacter> characters, IEnumerable<StoredQuote> quotes)
    {
        this.Name = name;
        this.Id = id;
        this.Characters = new List<StoredCharacter>(characters);
        this.Quotes = new List<StoredQuote>(quotes);
    }
}
