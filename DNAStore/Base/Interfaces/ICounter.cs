namespace DNAStore.Base.Interfaces;

public interface ICounter<TKey, TValue>
{
    public int Count { get; }

    public TValue HighestFrequency { get; }

    public List<TKey> Keys { get; }

    void Add(TKey val);
}