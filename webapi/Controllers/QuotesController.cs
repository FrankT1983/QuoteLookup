using Microsoft.AspNetCore.Mvc;
using Quotes.Storage.Interface;
using webapi.Models;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuotesController : ControllerBase
{
    private readonly ILogger<QuotesController> _logger;
    private readonly global::Quotes.Quotes quotes;

    public QuotesController(ILogger<QuotesController> logger, IQuoteStorage storage)
    {
        _logger = logger;
        this.quotes = new global::Quotes.Quotes(storage);
    }

    [HttpGet]
    [Route("")]
    public async Task<QuoteSearchResults> GetQuotes([FromQuery] string? movie, [FromQuery] string? actor, [FromQuery] int? start, [FromQuery] int? count)
    {
        // todo: pagination
        // Todo: rethink return objects
        await this.quotes.Search(movie ?? string.Empty, actor ?? string.Empty);

        return new QuoteSearchResults();
    }
}
