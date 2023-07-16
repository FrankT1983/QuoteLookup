using System.Reflection;
using Quotes.ExampleData;
using Quotes.Storage.InMemory;

namespace Quotes.Test;

public class QuoteTests
{
    [Test]
    public void WithoutQuotesExtension()
    {
        Assert.That(ExampleMovies.Aliens.Quotes.Count(), Is.GreaterThan(0));
        Assert.That(ExampleMovies.Aliens.WithoutQuotes().Quotes.Count(), Is.Zero);
    }

    [Test]
    public async Task AddingQuoteWorkflow()
    {
        var uut = new Quotes(new InMemoryQuotesStorage());
        await uut.Movies.AddAsync(ExampleMovies.Aliens.WithoutQuotes());

        var movies = await uut.Movies.FindByNameAsync("Aliens");
        var movie = movies.Single();
        var rippley = movie.FindCharacterByName("Rippley");

        var quote = new Quote("1",
            new Dialog(new[]
            {
                new DialogLine(
                    TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(21)).Add(TimeSpan.FromSeconds(36)),
                    rippley,
                    "I say we take off and nuke the entire site from orbit."
                ),
                new DialogLine(
                    TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(21)).Add(TimeSpan.FromSeconds(43)),
                    rippley,
                    "It's the only way to be sure"
                ),
            }));
        await movie.AddQuoteAsync(quote);

        var search = await uut.Movies.FindByNameAsync("Aliens");
        Assert.That(search, Is.Not.Null);
        Assert.That(search.Count(), Is.EqualTo(1));
        var m = search.Single();
        Assert.That(m.Quotes.Single().Dialog.Lines.Count(), Is.EqualTo(2));
    }
}