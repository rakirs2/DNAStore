namespace Base.DataStructures;

public class Graph<T> : ICloneable, IEquatable<Graph<T>>
{
    private readonly SortedDictionary<T, HashSet<T>> tracker;
    private int _numNodes;
    private int _numEdges;

    public Graph(int numNodes, IComparer<T>? comparer = null)
    {
        tracker = new SortedDictionary<T, HashSet<T>>(comparer ?? Comparer<T>.Default);
        for (int i = 1; i <= numNodes; i++)
        {
            tracker[(T)(object)i] = new HashSet<T>();
        }

        _numNodes = numNodes;
    }

    public Graph(IComparer<T>? comparer = null)
    {
        tracker = new SortedDictionary<T, HashSet<T>>(comparer ?? Comparer<T>.Default);
    }

    public void Insert(T start, T end)
    {
        if (tracker.ContainsKey(start))
        {
            tracker[start].Add(end);
        }
        else
        {
            tracker[start] = new HashSet<T>() { end };
        }

        if (tracker.ContainsKey(end))
        {
            tracker[end].Add(start);
        }
        else
        {
            tracker[end] = new HashSet<T>() { start };
        }
        // TODO: Currently, this implementation does not check for duplicate edges.
        _numEdges++;
    }

    public void Remove(T item)
    {
        throw new NotImplementedException();
    }

    public Dictionary<T, HashSet<T>> GetEdgeList()
    {
        return tracker.ToDictionary();
    }

    public int EdgesToMakeTree()
    {
        // every node that doesn't have a value is an unconnected node
        // so we need to find the total number of unconnected nodes
        return _numNodes - _numEdges - 1; // -1 because a tree with n nodes has n-1 edges
    }

    public object Clone()
    {
        return MemberwiseClone();
    }


    protected bool Equals(Graph<T> other)
    {
        return GraphEquality(this, other);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Graph<T>)obj);
    }

    public override int GetHashCode()
    {
        return tracker.GetHashCode();
    }

    public static bool GraphEquality(Graph<T> first, Graph<T> other)
    {
        return first.GetEdgeList().Count == other.GetEdgeList().Count && !first.GetEdgeList().Except(other.GetEdgeList()).Any();
    }

    bool IEquatable<Graph<T>>.Equals(Graph<T>? other)
    {
        return Equals(other);
    }
}
