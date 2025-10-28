namespace Base.DataStructures;

/// <summary>
/// Simple undirected graph implementation.
/// </summary>
/// <typeparam name="T"></typeparam>
public class UndirectedGraph<T> : ICloneable, IEquatable<UndirectedGraph<T>> where T : notnull
{
    private readonly int _numNodes;
    protected readonly SortedDictionary<T, HashSet<T>> _tracker;
    protected int NumEdges;

    public UndirectedGraph(int numNodes, IComparer<T>? comparer = null)
    {
        _tracker = new SortedDictionary<T, HashSet<T>>(comparer ?? Comparer<T>.Default);
        for (var i = 1; i <= numNodes; i++) _tracker[(T)(object)i] = new HashSet<T>();

        _numNodes = numNodes;
    }

    public UndirectedGraph()
    {
        _tracker = new SortedDictionary<T, HashSet<T>>();
    }

    public UndirectedGraph(IComparer<T>? comparer = null)
    {
        _tracker = new SortedDictionary<T, HashSet<T>>(comparer ?? Comparer<T>.Default);
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    bool IEquatable<UndirectedGraph<T>>.Equals(UndirectedGraph<T>? other)
    {
        return other != null && Equals(other);
    }

    public virtual void Insert(T start, T end)
    {
        if (_tracker.TryGetValue(start, out var value))
            value.Add(end);
        else
            _tracker[start] = [end];

        if (_tracker.TryGetValue(end, out var value1))
            value1.Add(start);
        else
            _tracker[end] = [start];
        
        // TODO: Currently, this implementation does not check for duplicate edges.
        NumEdges++;
    }

    public void Remove(T item)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// This should return an edge list with no counts
    /// </summary>
    /// <returns></returns>
    public Dictionary<T, HashSet<T>> GetEdgeList()
    {
        return _tracker.ToDictionary();
    }

    public int EdgesToMakeTree()
    {
        // every node that doesn't have a value is an unconnected node
        // so we need to find the total number of unconnected nodes
        return _numNodes - NumEdges - 1; // -1 because a tree with n nodes has n-1 edges
    }


    private bool Equals(UndirectedGraph<T> other)
    {
        return GraphEquality(this, other);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((UndirectedGraph<T>)obj);
    }

    public override int GetHashCode()
    {
        return _tracker.GetHashCode();
    }

    private static bool GraphEquality(UndirectedGraph<T> first, UndirectedGraph<T> other)
    {
        return first.GetEdgeList().Count == other.GetEdgeList().Count &&
               !first.GetEdgeList().Except(other.GetEdgeList()).Any();
    }
}