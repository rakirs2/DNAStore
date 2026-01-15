using System.Numerics;
using DNAStore.Base.Interfaces;

namespace DNAStore.Base.DataStructures;

public class AddOnlyCounter<TKey, TCount> : ICounter<TKey, TCount>
    where TCount : IBinaryInteger<TCount>, IComparable<TCount>
{
    private readonly Dictionary<TKey, TCount> _dictionary = new();

    private TCount _highestCount;

    public AddOnlyCounter()
    {
        HighestFrequency = default;
    }


    public TKey HighestFrequency { get; private set; }

    public int Count { get; private set; }

    TCount ICounter<TKey, TCount>.HighestFrequency { get; }

    public void Add(TKey val)
    {
        if (_dictionary.ContainsKey(val))
            _dictionary[val]++;
        else
            _dictionary.Add(val, TCount.One);

        if (_highestCount < _dictionary[val])
        {
            HighestFrequency = val;
            _highestCount = _dictionary[val];
        }

        Count++;
    }

    // TODO: figure this out cleaner, it's fine for now
    public List<TKey> Keys => _dictionary.Keys.ToList();
}