using Base.Interfaces;

namespace Base.DataStructures;

/// <summary>
/// A basic counter for the total number of base pairs on a given sequence.
/// Also tracks total number of bps in the class
/// </summary>
public class BasePairDictionary : IBasePairDictionary
{
    public long Count { get; private set; }

    //TODO: there might be some perf optimizations here because we've only have so many base pairs -- might be faster to not use the hashmap search
    public void Add(char c)
    {
        if (!_dictionary.TryAdd(c, 1)) _dictionary[c] += 1;

        Count++;
    }

    public long GetFrequency(char c)
    {
        return _dictionary.GetValueOrDefault(c, 0);
    }

    public override string ToString()
    {
        var outputString = "";
        // TODO: there's probably some JSONifier that does this in one line
        // TODO: there might be some value in forcing this to be alphabetical
        foreach (var key in _dictionary.Keys)
        {
            outputString += $"{key}: {_dictionary[key]};";
        }

        return outputString;
    }

    private readonly Dictionary<char, long> _dictionary = new();
}