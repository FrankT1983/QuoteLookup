namespace Quotes.Storage.Interface.Objects;

public class StoredDialogLine
{
    public StoredDialogLine(double startAfterSeconds, StoredCharacter character, string line)
    {
        this.StartAfterSeconds = startAfterSeconds;
        this.Character = character;
        this.Line = line;
    }


    public string Line { get; set; }

    /// this should probably refer to a character
    public StoredCharacter Character { get; set; }

    // This might be a problem 
    public double StartAfterSeconds { get; private set; }
}