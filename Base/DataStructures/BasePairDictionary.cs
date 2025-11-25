using System.Text.Json;
using Base.Interfaces;

namespace Base.DataStructures;

/// <summary>
///     A basic counter for the total number of base pairs on a given sequence.
///     Also tracks total number of bps in the class
///     TODO: this should just be a character dictionary which extends into a basepair dictionary for RNA and DNA
///     and a protein dictionary for others
/// </summary>
public class BasePairDictionary : IBasePairDictionary
{
    private readonly Dictionary<char, long> _dictionary = new();
    public long Count { get; private set; }

    // This is a very, very optimistic implementation. This only works for addonly. Worry about it if we get to an edit case
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

    public long GetFrequency(char c)
    {
        return _dictionary.GetValueOrDefault(c, 0);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(_dictionary);
    }
}