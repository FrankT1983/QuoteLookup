using Quotes.Storage.Interface.Objects;

namespace Quotes.Storage.Interface;

public interface IQuoteStorage
{
    public IStorage<StoredMovie> Movies { get; }

    public IStorage<StoredActor> Actors { get; }
}
