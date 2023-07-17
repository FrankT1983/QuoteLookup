using System.Runtime.CompilerServices;
using Quotes.Storage.Interface;
using webapi.Models;

namespace Quotes;

public class Quotes
{
    public Movies Movies { get; private set; }

    public Actors Actors { get; private set; }

    public Quotes(IQuoteStorage storage)
    {
        this.Movies = new Movies(storage);
        this.Actors = new Actors(storage);
    }

    public async Task<QuoteSearchResults> Search(string quoteFragment, string movie = "", string actor = "")
    {
        var movies = await this.Movies.GetAsync();
        var filtered = string.IsNullOrWhiteSpace(movie)
            ? movies
            : movies.Where(m => m.Name.Contains(movie, StringComparison.InvariantCultureIgnoreCase));
        // this will not work with internationallization
        // going by ID might be better



        var actorIds = string.IsNullOrWhiteSpace(actor)
            ? new HashSet<string>()
        : (await this.Actors.FindIdsAsync(actor)).ToHashSet();



        await this.Movies.FindByNameAsync(movie);

        var result = new List<QuoteSearchResult>();

        foreach (var m in filtered)
        {
            foreach (var q in m.Quotes)
            {
                if (!string.IsNullOrWhiteSpace(actor))
                {
                    if (!q.Dialog.Lines.Any(l => actorIds.Contains(l.Character.ActorId)))
                    {
                        continue;
                    }
                }

                if (q.Dialog.Lines.Any(l => l.Line.Contains(quoteFragment, StringComparison.InvariantCultureIgnoreCase)))
                {
                    result.Add(new QuoteSearchResult(q.Id, m.Id));
                }
            }
        }

        return new QuoteSearchResults()
        {
            Quotes = result
        };
    }
}

