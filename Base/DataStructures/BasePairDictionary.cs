namespace Bio.Sequence;

/// <summary>
/// A basic counter for the total number of base pairs on a given sequence.
/// Also tracks total number of bps in the class
/// </summary>
public class BasePairDictionary
{
    public long Count => count;

    //TODO: there might be some perf optimizations here because we've only have so many base bairs -- might be faster to not use the hashmap search
    public void Add(char c)
    {
        // TODO: isn't there some optimization
        if (!_dictionary.TryAdd(c, 1)) _dictionary[c] += 1;

        count++;
    }

    public long GetFrequency(char c)
    {
        if (_dictionary.ContainsKey(c))
            return _dictionary[c];
        else
            // Might be worth throwing here
            return 0;
    }

    private long count = 0;
    private readonly Dictionary<char, long> _dictionary = new();
}