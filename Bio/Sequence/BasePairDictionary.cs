using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio.Sequence;

public class BasePairDictionary
{
    public long Count => _dictionary.Count;

    //TODO: there might be some perf optimizations here because we've only have so many base bairs -- might be faster to not use the hashmap search
    public void Add(char c)
    {
        // TODO: isn't there some optimization
        if (!_dictionary.TryAdd(c, 1)) _dictionary[c] += 1;
    }

    public long GetFrequency(char c)
    {
        if (_dictionary.ContainsKey(c))
            return _dictionary[c];
        else
            // Might be worth throwing here
            return 0;
    }

    private readonly Dictionary<char, long> _dictionary = new();
}