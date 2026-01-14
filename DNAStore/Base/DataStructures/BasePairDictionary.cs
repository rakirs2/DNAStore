using System.Text.Json;
using DNAStore.Base.Interfaces;
using DNAStore.Base.Utils;

namespace DNAStore.Base.DataStructures;

/// <summary>
///     A basic counter for the total number of base pairs on a given sequence.
///     Also tracks total number of bps in the class
///     and a protein dictionary for others
///
///     // TODO: eventually merge this with basic counter
///     // TODO: figure out how to handle different counts/what I want that behavior to look like
/// </summary>
public class BasePairDictionary : IBasePairDictionary
{
    private readonly Dictionary<char, long> _dictionary = new(CaseInsensitiveCharComparer.Shared);

    public long this[char key] => GetFrequency(key);
    public long Count { get; private set; }

    public char HighestFrequencyBasePair { get; private set; }
    public long HighestFrequencyBasePairCount { get; private set; }

    public void Add(char c)
    {
        if (!_dictionary.TryAdd(c, 1)) _dictionary[c] += 1;

        if (_dictionary[c] > HighestFrequencyBasePairCount)
        {
            HighestFrequencyBasePairCount = _dictionary[c];
            HighestFrequencyBasePair = c;
        }

        Count++;
    }

    // TODO: just use indexer everywhere
    public long GetFrequency(char c)
    {
        return _dictionary.GetValueOrDefault(c, 0);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(_dictionary);
    }
}