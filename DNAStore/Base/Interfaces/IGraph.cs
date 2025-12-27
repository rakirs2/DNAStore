namespace DNAStore.Base.Interfaces;

internal interface IGraph<T>
{
    int NumNodes { get; }
    int NumEdges { get; }
    void Insert(T start, T end);
    void Remove(T item);
    Dictionary<T, HashSet<T>> GetEdgeList();
    int EdgesToMakeTree();
}