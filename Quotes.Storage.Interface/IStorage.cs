using Quotes.Storage.Interface.Objects;

namespace Quotes.Storage.Interface;

/// <summary>
/// A minimal interface for storing and retrieving objects.
/// </summary>
/// <typeparam name="T">Type of the object to be stored.</typeparam>
public interface IStorage<T> where T : IHasId
{
    /// <summary>
    /// Add an object. Do nothing if it already exists
    /// </summary>
    /// <param name="objectToStoreOrUpdate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> AddAsync(T objectToStoreOrUpdate, CancellationToken? cancellationToken);

    /// <summary>
    /// Add an object. Replace it, if it already exists
    /// </summary>
    /// <param name="objectToStoreOrUpdate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> AddOrUpdateAsync(T objectToStoreOrUpdate, CancellationToken? cancellationToken);

    /// <summary>
    /// Add an object to the 
    /// </summary>
    /// <param name="objectToStoreOrUpdate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<T>> GetAsync(CancellationToken? cancellationToken);


    /// <summary>
    /// Remove an object, identified by it's id
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task DeleteAsync(string Id, CancellationToken? cancellationToken);
}