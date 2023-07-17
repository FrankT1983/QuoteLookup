using Quotes.Converters;
using Quotes.Storage.Interface;
using Quotes.Storage.Interface.Objects;

namespace Quotes;

/// <summary>
/// <see cref="Movies"/> is used to interact with all stored movies.
/// </summary>
public class Movies
{
    private IQuoteStorage storage;

    /// <summary>
    /// Construct a <see cref="Movies"/> object. 
    /// </summary>
    /// <param name="storage">Storage for persistence.</param>
    public Movies(IQuoteStorage storage)
    {
        this.storage = storage;
    }

    /// <summary>
    /// Fina a movie by it's name.
    /// </summary>
    /// <param name="name">The name of the movie. This needs to be an exact match for now.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Movie>> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var movies = await storage.Movies.GetAsync(cancellationToken);
        return movies
            .Where(m => string.Equals(name, m.Name, StringComparison.InvariantCultureIgnoreCase) == true)
            .Select(m =>
            {
                var movie = MovieConverter.FromStorageObject(m);
                movie.AttachToStorage(this.storage);
                return movie;
            });
    }

    public async Task<Movie?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var movies = await storage.Movies.GetAsync(cancellationToken);
        return movies
                .Where(m => string.Equals(id, m.Id, StringComparison.InvariantCultureIgnoreCase) == true)
                .Select(m =>
                {
                    var movie = MovieConverter.FromStorageObject(m);
                    movie.AttachToStorage(this.storage);
                    return movie;
                }).FirstOrDefault();
        ;
    }

    public async Task<IEnumerable<Movie>> GetAsync(CancellationToken cancellationToken = default)
    {
        var movies = await storage.Movies.GetAsync(cancellationToken);
        return movies
                .Select(m =>
                {
                    var movie = MovieConverter.FromStorageObject(m);
                    movie.AttachToStorage(this.storage);
                    return movie;
                })
            ;
    }

    public async Task DeleteAsync(string movieId, CancellationToken cancellationToken = default)
    {
        await this.storage.Movies.DeleteAsync(movieId, cancellationToken);
    }

    // not super happy with this pattern,
    public Task<AddResult<Movie>> AddAsync(Movie m, CancellationToken cancellationToken = default)
    {
        return AddOrUpdateAsync(this.storage, m, cancellationToken);
    }

    internal static async Task<AddResult<Movie>> AddOrUpdateAsync(IQuoteStorage storage, Movie m, CancellationToken cancellationToken)
    {
        try
        {
            var isSuccess = await storage.Movies.AddOrUpdateAsync(MovieConverter.ToStorageObject(m), cancellationToken);
            if (isSuccess)
            {
                m.AttachToStorage(storage);
                return AddResult<Movie>.Success(m);
            }
            // alternative, have the add or update return the stored update and validate + construct result from that  
        }
        catch (Exception e)
        {
            // better login
            Console.WriteLine(e);
            return AddResult<Movie>.Failed();
        }

        // skipping to provide a failure reason for now 
        return AddResult<Movie>.Failed();
    }
}
