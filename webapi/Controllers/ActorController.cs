using Microsoft.AspNetCore.Mvc;
using Quotes.ExampleData;
using Quotes.Storage.Interface;
using Quotes;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActorController : ControllerBase
{
    private readonly ILogger<ActorController> _logger;
    private readonly global::Quotes.Quotes quotes;

    public ActorController(ILogger<ActorController> logger, IQuoteStorage storage)
    {
        _logger = logger;
        this.quotes = new global::Quotes.Quotes(storage);
    }

    [HttpGet]
    [Route("")]
    public async Task<MoviesResponse> GetActors()
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
    public Task AddActor([FromBody] Movie m)
    {
        return this.quotes.Movies.AddAsync(m, default);
    }

    [HttpGet]
    [Route("{actorId}")]
    public async Task<MovieResponse> GetActor([FromRoute] string actorId)
    {
        var movie = await this.quotes.Movies.GetAsync(actorId, default);

        return new MovieResponse()
        {
            Movie = movie,
        };
    }

    [HttpPatch]
    [Route("{actorId}")]
    public Task UpdateMovie([FromRoute] string actorId)
    {
        // not Implemented, do later
        return Task.CompletedTask;
    }

    [HttpDelete]
    [Route("{actorId}")]
    public async Task DeleteMovie([FromRoute] string actorId)
    {
        await this.quotes.Movies.DeleteAsync(actorId, default);
    }

}