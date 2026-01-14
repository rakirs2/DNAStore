using System.Collections.Specialized;

namespace DNAStore.Base.Interfaces;

public interface ICounter<TKey, TValue>
{
    public int Count { get; }
    
    public TValue HighestFrequency{ get; }

    void Add(TKey val);
    
    public List<TKey> Keys { get; }
}