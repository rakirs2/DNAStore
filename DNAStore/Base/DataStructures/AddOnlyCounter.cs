using System.Numerics;
using DNAStore.Base.Interfaces;

namespace DNAStore.Base.DataStructures;

public class AddOnlyCounter<TKey, TCount>: ICounter<TKey, TCount> where TCount : IBinaryInteger<TCount>, IComparable<TCount>
{
    private readonly Dictionary<TKey, TCount> _dictionary = new Dictionary<TKey, TCount>();

    public AddOnlyCounter()
    {
        HighestFrequency = default(TKey);
    }
    
    public int Count { get; private set; }

    TCount ICounter<TKey, TCount>.HighestFrequency => _highestFrequency;
    

    public TKey HighestFrequency { get; private set; }

    private TCount _highestCount;
    private TCount _highestFrequency;

    public void Add(TKey val)
    {
        if (_dictionary.ContainsKey(val))
        {
            _dictionary[val]++;
        }
        else
        {
            _dictionary.Add(val, TCount.One);
        }

        if (_highestCount < _dictionary[val])
        {
            HighestFrequency = val;
            _highestCount = _dictionary[val];
        }
        Count++;
    }
}