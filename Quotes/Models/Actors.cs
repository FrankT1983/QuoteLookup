using Quotes.Storage.Interface;
using Quotes.Storage.Interface.Objects;

namespace Quotes;

public class Actors
{
    private IQuoteStorage storage;

    public Actors(IQuoteStorage storage)
    {
        this.storage = storage;
    }

    internal async Task<IEnumerable<string>> FindIdsAsync(string actor, CancellationToken cancellationToken = default)
    {
        var actors = await this.storage.Actors.GetAsync(cancellationToken);

        return actors.Where(a => Converters.NameConverter.FromStorageObject(a.Name).Is(actor))
            .Select(a => a.Id);
    }

    public async Task AddAsync(Actor actor, CancellationToken cancellationToken = default)
    {
        await this.storage.Actors.AddAsync(new StoredActor(actor.Id, Converters.NameConverter.ToStorageObject(actor.Name)), cancellationToken);
    }
}
