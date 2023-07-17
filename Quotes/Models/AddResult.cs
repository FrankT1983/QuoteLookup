using System.Diagnostics.CodeAnalysis;

namespace Quotes;

public class AddResult<T> where T : new()
{
    public static AddResult<T> Success(T o)
    {
        return new AddResult<T>(o, true);
    }

    public static AddResult<T> Failed()
    {
        return new AddResult<T>(default, false);
    }

    private AddResult(T o, bool wasSuccess)
    {
        this.o = o;
        this.WasAdded = wasSuccess;
    }

    private T o;

    public T Object
    {
        get
        {
            if (WasAdded)
            {
                return this.o;
            }

            return default;
        }
    }

    public bool WasAdded { get; }
}