using DNAStore.Base.Interfaces;

namespace DNAStore.Base.DataStructures;

/// <summary>
///     Simple undirected graph implementation.
/// </summary>
/// <typeparam name="T"></typeparam>
public class UndirectedGraph<T> : ICloneable, IGraph<T>, IEquatable<UndirectedGraph<T>> where T : notnull
{
    protected readonly SortedDictionary<T, HashSet<T>> EdgeList;
    // TODO: tests for NumEdges
    public int NumEdges { get; protected set; }

    public int NumberOfConnectedComponents()
    {
        var untraversed = EdgeList.Keys.ToHashSet();
        var connectedComponentCount = 0;
        while (untraversed.Count > 0)
        {
            var current = untraversed.First();
            untraversed.Remove(current);
            connectedComponentCount++;
            
            var tracker = new Queue<T>();
            tracker.Enqueue(current);
            while (tracker.Count > 0)
            {
                var currentBfs = tracker.Dequeue();
                untraversed.Remove(currentBfs);
                foreach (var edge in EdgeList[currentBfs])
                {
                    if (!untraversed.Contains(edge)) continue;
                    tracker.Enqueue(edge);
                    
                }
            }
        }
        
        return connectedComponentCount;
    }
    
    public UndirectedGraph(int numNodes, IComparer<T>? comparer = null)
    {
        EdgeList = new SortedDictionary<T, HashSet<T>>(comparer ?? Comparer<T>.Default);
        for (var i = 1; i <= numNodes; i++) EdgeList[(T)(object)i] = new HashSet<T>();

        NumNodes = numNodes;
    }

    public UndirectedGraph()
    {
        EdgeList = new SortedDictionary<T, HashSet<T>>();
    }

    public UndirectedGraph(IComparer<T>? comparer = null)
    {
        EdgeList = new SortedDictionary<T, HashSet<T>>(comparer ?? Comparer<T>.Default);
    }

    public int NumNodes { get; }

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
        if (EdgeList.TryGetValue(start, out var value))
            value.Add(end);
        else
            EdgeList[start] = [end];

        if (EdgeList.TryGetValue(end, out var value1))
            value1.Add(start);
        else
            EdgeList[end] = [start];

        // TODO: Currently, this implementation does not check for duplicate edges.
        NumEdges++;
    }

    public void Remove(T item)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     This should return an edge list with no counts
    /// </summary>
    /// <returns></returns>
    public Dictionary<T, HashSet<T>> GetEdgeList()
    {
        return EdgeList.ToDictionary();
    }

    public int EdgesToMakeTree()
    {
        // every node that doesn't have a value is an unconnected node
        // so we need to find the total number of unconnected nodes
        return NumNodes - NumEdges - 1; // -1 because a tree with n nodes has n-1 edges
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
        return EdgeList.GetHashCode();
    }

    private static bool GraphEquality(UndirectedGraph<T> first, UndirectedGraph<T> other)
    {
        return first.GetEdgeList().Count == other.GetEdgeList().Count &&
               !first.GetEdgeList().Except(other.GetEdgeList()).Any();
    }
}