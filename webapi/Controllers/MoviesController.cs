using Microsoft.AspNetCore.Mvc;
using Quotes.ExampleData;
using Quotes.Storage.Interface;
using Quotes;
using Quotes.Errors;
using System.Net;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly ILogger<MoviesController> _logger;
    private readonly global::Quotes.Quotes quotes;

    public MoviesController(ILogger<MoviesController> logger, IQuoteStorage storage)
    {
        _logger = logger;
        this.quotes = new global::Quotes.Quotes(storage);
    }

    /// <summary>
    /// Get all available movies
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    public async Task<MoviesResponse> GetMovies()
    {
        var movies = await this.quotes.Movies.GetAsync(default);
        if (!movies.Any())
        {
            await this.quotes.Movies.AddAsync(ExampleMovies.Aliens);
            await this.quotes.Movies.AddAsync(ExampleMovies.BladeRunner);
        }
        movies = await this.quotes.Movies.GetAsync(default);

        return new MoviesResponse()
        {
            Movies = movies
        };
    }

    [HttpPut]
    [Route("")]
    public Task Put([FromBody] Movie m)
    {
        return this.quotes.Movies.AddAsync(m, default);
    }

    [HttpGet]
    [Route("{movieId}")]
    public async Task<MovieResponse> GetMovie([FromRoute] string movieId)
    {
        var movie = await this.quotes.Movies.GetAsync(movieId, default);

        return new MovieResponse()
        {
            Movie = movie,
        };
    }

    [HttpPatch]
    [Route("{movieId}")]
    public Task UpdateMovie([FromRoute] string movieId)
    {
        // not Implemented, do later
        return Task.CompletedTask;
    }

    [HttpDelete]
    [Route("{movieId}")]
    public async Task DeleteMovie([FromRoute] string movieId)
    {
        await this.quotes.Movies.DeleteAsync(movieId, default);
    }

    [HttpGet]
    [Route("{movieId}/characters/")]
    public async Task<CharactersResponse> GetCharacters([FromRoute] string movieId)
    {
        var movies = await this.quotes.Movies.GetAsync(movieId, default);

        return new CharactersResponse()
        {
            Characters = movies?.Characters ?? Array.Empty<Character>()
        };
    }

    [HttpGet]
    [Route("{movieId}/characters/{characterId}")]
    public async Task<CharacterResponse> GetCharacter([FromRoute] string movieId, [FromRoute] string characterId)
    {
        var movie = await this.quotes.Movies.GetAsync(movieId, default);

        if (movie == null)
        {
            return new CharacterResponse();
        }

        return new CharacterResponse()
        {
            Character = movie.Characters.FirstOrDefault(c => string.Equals(c.Id, characterId))
        };
    }


    [HttpPatch]
    [Route("{movieId}/characters/{characterId}")]
    public async Task UpdateCharacter([FromRoute] string movieId, [FromRoute] string characterId)
    {
        await Task.CompletedTask;
        // todo Implement 
    }

    [HttpDelete]
    [Route("{movieId}/characters/{characterId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCharacter([FromRoute] string movieId, [FromRoute] string characterId)
    {
        var movie = await this.quotes.Movies.GetAsync(movieId, default);

        if (movie == null)
        {
            return NotFound();
        }

        var deletionResult = await movie.DeleteCharacter(characterId);
        if (deletionResult == null)
        {
            return Ok();
        }

        if (deletionResult == DomainErrors.CantDeletCharacterThatHaveQuotes)
        {
            return Problem(statusCode: StatusCodes.Status409Conflict, detail: DomainErrors.CantDeletCharacterThatHaveQuotes);
        }

        return Problem(statusCode: StatusCodes.Status500InternalServerError);
    }

    [HttpGet]
    [Route("{movieId}/quotes/")]
    public async Task<QuotesResponse> GetQuotes([FromRoute] string movieId)
    {
        var movies = await this.quotes.Movies.GetAsync(movieId, default);

        return new QuotesResponse()
        {
            Quotes = movies?.Quotes ?? Array.Empty<Quote>()
        };
    }

    [HttpGet]
    [Route("{movieId}/quotes/{quoteId}")]
    public async Task<QuoteResponse> GetQuote([FromRoute] string movieId, [FromRoute] string quoteId)
    {
        var movie = await this.quotes.Movies.GetAsync(movieId, default);

        if (movie == null)
        {
            return new QuoteResponse();
        }

        return new QuoteResponse()
        {
            Quotes = movie.Quotes.FirstOrDefault(c => string.Equals(c.Id, quoteId))
        };
    }

    [HttpPatch]
    [Route("{movieId}/quotes/{quoteId}")]
    public async Task UpdateQuote([FromRoute] string movieId, [FromRoute] string quoteId)
    {
        await Task.CompletedTask;
        // todo Implement 
    }

    [HttpDelete]
    [Route("{movieId}/quotes/{quoteId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteQuote([FromRoute] string movieId, [FromRoute] string quoteId)
    {
        var movie = await this.quotes.Movies.GetAsync(movieId, default);

        if (movie == null)
        {
            return NotFound();
        }

        var deletionResult = await movie.DeleteQuote(quoteId);

        if (deletionResult == null)
        {
            return Ok();
        }

        if (deletionResult == DomainErrors.QuoteNotFound)
        {
            return Problem(statusCode: StatusCodes.Status404NotFound, detail: DomainErrors.QuoteNotFound);
        }

        return Problem(statusCode: StatusCodes.Status500InternalServerError);
    }

    [HttpPut]
    [Route("{movieId}/characters/")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> AddCharacter([FromRoute] string movieId, [FromBody] Character character)
    {
        var movie = await this.quotes.Movies.GetAsync(movieId, default);

        if (movie == null)
        {
            return Problem(statusCode: StatusCodes.Status404NotFound);
        }

        await movie.AddCharacter(character);
        // Todo: rethink return objects
        return Ok();
    }

    [HttpPut]
    [Route("{movieId}/quotes/")]
    public async Task AddQuote([FromRoute] string movieId, [FromBody] Quote quote)
    {
        var movie = await this.quotes.Movies.GetAsync(movieId, default);

        if (movie == null)
        {
            return;
        }

        await movie.AddQuoteAsync(quote);
        // Todo: rethink return objects
    }
}
