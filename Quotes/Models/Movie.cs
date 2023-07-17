using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Quotes.Errors;
using Quotes.Storage.Interface;
using Quotes.Storage.Interface.Objects;

namespace Quotes;

// todo: make on function to change a movie and update it after

[DebuggerDisplay("{Name}")]
public class Movie : QuoteSource
{
    /// <summary>
    /// Construct a new move
    /// </summary>
    /// <param name="name">Name of the movie (dummy for now)</param>
    /// <param name="id">use IMDB id as an ID for now. Might not be the best idea but feels better then just creating a uuid</param>
    public Movie(string id, string name) : this(id, name, Array.Empty<Character>(), Array.Empty<Quote>())
    {
    }

    /// <summary>
    /// Construct a new move
    /// </summary>
    /// <param name="name">Name of the movie (dummy for now)</param>
    /// <param name="id">use IMDB id as an ID for now. Might not be the best idea but feels better then just creating a uuid</param>
    /// <param name="characters"></param>
    /// <param name="quotes"></param>
    public Movie(string id, string name, IEnumerable<Character> characters, IEnumerable<Quote> quotes)
    {
        this.Name = name;
        this.Id = id;
        this.characters = characters.ToDictionary(c => c.Id);
        this.quotes = quotes.ToDictionary(c => c.Id);
    }

    /// <summary>
    /// Temp default constructor till I figure out generics in Response types 
    /// </summary>
    public Movie()
    {
        this.Name = string.Empty;
        this.Id = string.Empty;
    }

    public string Name { get; set; }

    public IEnumerable<Character> Characters
    {
        get
        {
            return this.characters.Values;
        }
    }

    public IEnumerable<Quote> Quotes
    {
        get
        {
            return this.quotes.Values;
        }
    }

    private IQuoteStorage? storage;
    private Dictionary<string, Character> characters = new Dictionary<string, Character>();
    private Dictionary<string, Quote> quotes = new Dictionary<string, Quote>();

    public async Task<AddResult<Character>> AddCharacter(Character character, CancellationToken cancellationToken = default)
    {
        if (this.characters.ContainsKey(character.Id))
        {
            this.characters[character.Id] = (character);
        }
        else
        {
            this.characters.Add(character.Id, character);
        }

        if (this.storage == null)
        {
            return AddResult<Character>.Success(character);
        }

        // if connected to storage => update storage
        try
        {
            var update = await Movies.AddOrUpdateAsync(this.storage, this, cancellationToken);
            if (update.WasAdded)
            {
                // no subobjects on character, no 
                return AddResult<Character>.Success(character);
            }

            return AddResult<Character>.Failed();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return AddResult<Character>.Failed();
        }
    }

    public async Task<AddResult<Quote>> AddQuoteAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        if (this.quotes.ContainsKey(quote.Id))
        {
            this.quotes[quote.Id] = (quote);
        }
        else
        {
            this.quotes.Add(quote.Id, quote);
        }

        if (this.storage == null)
        {
            return AddResult<Quote>.Success(quote);
        }

        // todo: validate that the used characters belong to this movie

        // if connected to storage => update storage
        try
        {
            var update = await Movies.AddOrUpdateAsync(this.storage, this, cancellationToken);
            if (update.WasAdded)
            {
                // no subobjects on character, no 
                return AddResult<Quote>.Success(quote);
            }

            return AddResult<Quote>.Failed();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return AddResult<Quote>.Failed();
        }
    }

    public void AttachToStorage(IQuoteStorage s)
    {
        // todo: do checks if already attached to something else, throw otherwise.
        this.storage = s;
    }

    public Character? FindCharacterByName(string name)
    {
        return this.Characters.FirstOrDefault(c => c.Name.Is(name));
    }

    public async Task<string> DeleteCharacter(string characterId, CancellationToken cancellationToken = default)
    {
        if (this.quotes.Values.Any(q => q.Dialog.Lines.Any(l => l.Character.Id == characterId)))
        {
            return DomainErrors.CantDeletCharacterThatHaveQuotes;
        }

        if (!this.characters.ContainsKey(characterId))
        {
            return DomainErrors.CharacterNotFound;
        }

        this.characters.Remove(characterId);
        if (this.storage == null)
        {
            return string.Empty;
        }

        try
        {
            var update = await Movies.AddOrUpdateAsync(this.storage, this, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

        }
        return string.Empty;
    }

    public async Task<string> DeleteQuote(string quoteId, CancellationToken cancellationToken = default)
    {
        if (!this.quotes.ContainsKey(quoteId))
        {
            return DomainErrors.QuoteNotFound;
        }

        this.quotes.Remove(quoteId);


        if (this.storage == null)
        {
            return string.Empty;
        }

        try
        {
            var update = await Movies.AddOrUpdateAsync(this.storage, this, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

        }
        return string.Empty;
    }
}
