using Quotes.Storage.Interface.Objects;

namespace Quotes.Converters;

public static class MovieConverter
{
    public static StoredMovie ToStorageObject(Movie m)
    {
        return new StoredMovie(
             m.Id,
             m.Name,
             m.Characters.Select(CharacterConverter.ToStorageObject),
             m.Quotes.Select(QuoteConverter.ToStorageObject));
    }

    public static Movie FromStorageObject(StoredMovie storedMovie)
    {
        return new Movie(
             storedMovie.Id,
             storedMovie.Name,
             storedMovie.Characters.Select(CharacterConverter.FromStorageObject),
             storedMovie.Quotes.Select(QuoteConverter.FromStorageObject));
    }
}

public static class CharacterConverter
{
    public static StoredCharacter ToStorageObject(Character c)
    {
        return new StoredCharacter(c.Id, NameConverter.ToStorageObject(c.Name), c.ActorId);
    }

    public static Character FromStorageObject(StoredCharacter c)
    {
        return new Character(c.Id, NameConverter.FromStorageObject(c.Name), c.ActorId);
    }
}

public static class NameConverter
{
    public static StoredName ToStorageObject(Name n)
    {
        return new StoredName(n.FirstName, n.LastName);
    }

    public static Name FromStorageObject(StoredName n)
    {
        return new Name(n.FirstName, n.LastName);
    }
}

public static class QuoteConverter
{
    public static StoredQuote ToStorageObject(Quote c)
    {
        return new StoredQuote(c.Id,
             c.Dialog.Lines.Select(l => new StoredDialogLine(
                  l.SecondsAfterStart,
                  CharacterConverter.ToStorageObject(l.Character),
                  l.Line
                  ))
             );
    }

    public static Quote FromStorageObject(StoredQuote c)
    {
        return new Quote(c.Id, new Dialog(
                  c.Lines.Select(l => new DialogLine(
                       TimeSpan.FromSeconds(l.StartAfterSeconds),
                       CharacterConverter.FromStorageObject(l.Character),
                       l.Line))
                  )
        );
    }
}