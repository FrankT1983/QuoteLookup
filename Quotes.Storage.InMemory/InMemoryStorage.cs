using Quotes.Storage.Interface;
using Quotes.Storage.Interface.Objects;

namespace Quotes.Storage.InMemory;

internal class InMemoryStorage<T> : IStorage<T> where T : IHasId
{
    // todo: checkout thread save Dictionary 
    private Dictionary<string, T> storage = new Dictionary<string, T>();

    public Task<bool> AddAsync(T objectToStoreOrUpdate, CancellationToken? cancellationToken)
    {
        return AddMaybeUpdateAsync(objectToStoreOrUpdate, false, cancellationToken);
    }

    public Task<bool> AddOrUpdateAsync(T objectToStoreOrUpdate, CancellationToken? cancellationToken)
    {
        return AddMaybeUpdateAsync(objectToStoreOrUpdate, true, cancellationToken);
    }

    public Task<bool> AddMaybeUpdateAsync(T objectToStoreOrUpdate, bool allowUpdate, CancellationToken? cancellationToken)
    {
        lock (this.storage)
        {
            if (storage.ContainsKey(objectToStoreOrUpdate.Id))
            {
                if (!allowUpdate)
                {
                    return Task.FromResult(false);
                }

                storage[objectToStoreOrUpdate.Id] = objectToStoreOrUpdate;
                return Task.FromResult(true);
            }

            storage.Add(objectToStoreOrUpdate.Id, objectToStoreOrUpdate);
            return Task.FromResult(true);
        }
    }

    public Task DeleteAsync(string id, CancellationToken? cancellationToken)
    {
        lock (this.storage)
        {
            if (this.storage.ContainsKey(id))
            {
                this.storage.Remove(id);
            }
        }

        return Task.CompletedTask;
    }

    public Task<IEnumerable<T>> GetAsync(CancellationToken? cancellationToken)
    {
        lock (this.storage)
        {
            var values = this.storage.Values.AsEnumerable();
            return Task.FromResult(values);
        }
    }
}