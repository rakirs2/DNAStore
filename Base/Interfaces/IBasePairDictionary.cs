namespace Base.Interfaces;

// Should be input only for now
public interface IBasePairDictionary
{
    void Add(char c);
    public long Count { get; }
}