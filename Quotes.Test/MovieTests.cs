using Quotes.Storage.InMemory;

namespace Quotes.Test;

public class MovieTests
{
    [Test]
    public async Task AddingAndRetrievingAMovieWorkflow()
    {
        var name = "Aliens";
        var id = "fn_al_tt_1";

        var uut = new Quotes(new InMemoryQuotesStorage());
        var stored = await uut.Movies.AddAsync(new Movie(id, name));

        var search = await uut.Movies.FindByNameAsync("Aliens");

        Assert.That(search, Is.Not.Null);
        Assert.That(search.Count(), Is.EqualTo(1));
        var m = search.Single();
        Assert.That(m.Name, Is.EqualTo(name));
        Assert.That(m.Id, Is.EqualTo(id));
    }
}

public class CharacterTests
{
    [Test]
    public async Task ConstructThenAddCharacter()
    {
        // Arrange
        var movieName = "Aliens";
        var movieId = "fn_al_tt_1";

        var characterFirstName = "Ellen";
        var characterLastName = "Rippley";

        var uut = new Quotes(new InMemoryQuotesStorage());
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

    [Test]
    public async Task AddCharacterToStoredMovie()
    {
        var movieName = "Aliens";
        var movieId = "fn_al_tt_1";

        var characterFirstName = "Ellen";
        var characterLastName = "Rippley";

        // Act
        var uut = new Quotes(new InMemoryQuotesStorage());
        await uut.Movies.AddAsync(new Movie(movieId, movieName));
        var searchToUpdate = await uut.Movies.FindByNameAsync("Aliens");
        var aliens = searchToUpdate.Single();
        await aliens.AddCharacter(new Character("1", new Name(characterFirstName, characterLastName)));

        // Assert
        var searchToValidate = await uut.Movies.FindByNameAsync("Aliens");
        Assert.That(searchToValidate, Is.Not.Null);
        Assert.That(searchToValidate.Count(), Is.EqualTo(1));
        var m = searchToValidate.Single();
        Assert.That(m.Characters.Count(), Is.EqualTo(1));
    }
}