namespace webapi.Models
{
    public class QuoteSearchResult
    {
        public string QuoteId { get; set; }

        public string MovieId { get; set; }

        public QuoteSearchResult(string quoteId, string movieId)
        {
            QuoteId = quoteId;
            MovieId = movieId;
        }
    }
}
