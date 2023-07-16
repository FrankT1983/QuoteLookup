using System.Security;
using Quotes.Storage.Interface;
using Quotes.Storage.Interface.Objects;

namespace Quotes.Storage.InMemory;

public class InMemoryQuotesStorage : IQuoteStorage
{
    public IStorage<StoredMovie> Movies { get; } = new InMemoryStorage<StoredMovie>();

    public IStorage<StoredActor> Actors { get; } = new InMemoryStorage<StoredActor>();
}