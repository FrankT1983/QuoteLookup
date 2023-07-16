
using Quotes;

public class MoviesResponse
{
    public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();
}

public class MovieResponse
{
    public Movie? Movie { get; set; }

}

public class QuotesResponse
{
    public IEnumerable<Quote> Quotes { get; set; } = new List<Quote>();
}

public class QuoteResponse
{
    public Quote? Quotes { get; set; }
}

public class CharactersResponse
{
    public IEnumerable<Character> Characters { get; set; } = new List<Character>();
}

public class CharacterResponse
{
    public Character? Character { get; set; }
}