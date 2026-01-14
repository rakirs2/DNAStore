namespace DNAStore.Base.Interfaces;

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