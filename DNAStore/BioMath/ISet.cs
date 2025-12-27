namespace DnaStore.Math;

public interface ISet<T>
{
    public SortedSet<T> Values { get; }
    public void Add(T value);
    public void Remove(T value);
}