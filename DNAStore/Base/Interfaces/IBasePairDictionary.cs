namespace DnaStore.Base.Interfaces;

// Should be input only for now
public interface IBasePairDictionary
{
    public long Count { get; }

    /// <summary>
    ///     This currently assumes add only. No removes no edits
    /// </summary>
    public char HighestFrequencyBasePair { get; }

    /// <summary>
    ///     This currently assumes add only. No removes no edits
    /// </summary>
    public long HighestFrequencyBasePairCount { get; }

    void Add(char c);
}