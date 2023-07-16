using Quotes.ExampleData;
using Quotes.Storage.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Test
{
    public class ActorTest
    {
        [Test]
        public async Task AddingAndRetrievingAMovieWorkflow()
        {
            // Arrange
            var movieName = "Aliens";
            var movieId = "fn_al_tt_1";

            var characterFirstName = "Ellen";
            var characterLastName = "Rippley";

            var uut = new Quotes(new InMemoryQuotesStorage());
            await uut.Actors.AddAsync(new Actor("sw", new Name("Sigourney", "Weaver")));


            var movie = new Movie(movieId, movieName);
            await movie.AddCharacter(new Character("1", new Name(characterFirstName, characterLastName)));
            await uut.Movies.AddAsync(movie);

            // Act
            var search = await uut.Movies.FindByNameAsync("Aliens");

            // Assert
            Assert.That(search, Is.Not.Null);
            Assert.That(search.Count(), Is.EqualTo(1));
            var m = search.Single();
            Assert.That(m.Characters.Count(), Is.EqualTo(1));
        }
    }

}
