using Quotes.Storage.InMemory;

namespace Quotes.Test
{
    public class SearchTest
    {
        [Test]
        public async Task SearchQuoteNoMovie()
        {
            var uut = new Quotes(new InMemoryQuotesStorage());
            await uut.Movies.AddAsync(ExampleData.ExampleMovies.Aliens);
            await uut.Movies.AddAsync(ExampleData.ExampleMovies.BladeRunner);
            var result = await uut.Search("orbit", "");

            Assert.That(result.Quotes.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task SearchQuoteMovie()
        {
            var uut = new Quotes(new InMemoryQuotesStorage());
            await uut.Movies.AddAsync(ExampleData.ExampleMovies.Aliens);
            await uut.Movies.AddAsync(ExampleData.ExampleMovies.BladeRunner);
            var result = await uut.Search("orbit", "alien", "");

            Assert.That(result.Quotes.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task SearchQuoteMovie_NotFound()
        {
            var uut = new Quotes(new InMemoryQuotesStorage());
            await uut.Movies.AddAsync(ExampleData.ExampleMovies.Aliens);
            await uut.Movies.AddAsync(ExampleData.ExampleMovies.BladeRunner);
            var result = await uut.Search("orbit", "blade", "");

            Assert.That(result.Quotes.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task SearchQuoteActor()
        {
            var uut = new Quotes(new InMemoryQuotesStorage());
            await uut.Movies.AddAsync(ExampleData.ExampleMovies.Aliens);
            await uut.Movies.AddAsync(ExampleData.ExampleMovies.BladeRunner);
            await uut.Actors.AddAsync(ExampleData.ExampleActors.SW);
            var result = await uut.Search("orbit", "", "weaver");

            Assert.That(result.Quotes.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task SearchQuoteActor_NotFound()
        {
            var uut = new Quotes(new InMemoryQuotesStorage());
            await uut.Movies.AddAsync(ExampleData.ExampleMovies.Aliens);
            await uut.Movies.AddAsync(ExampleData.ExampleMovies.BladeRunner);
            var result = await uut.Search("orbit", "", "ford");

            Assert.That(result.Quotes.Count, Is.EqualTo(0));
        }
    }

}



