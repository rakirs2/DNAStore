namespace Base.Interfaces;
internal interface IGraph<T>
{
    void Insert(T start, T end);
    void Remove(T item);
    Dictionary<T, HashSet<T>> GetEdgeList();
    int EdgesToMakeTree();
    int NumNodes { get; }
    int NumEdges { get; }
}