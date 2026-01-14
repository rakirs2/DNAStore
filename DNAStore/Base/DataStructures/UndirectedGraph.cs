using DNAStore.Base.Interfaces;

namespace DNAStore.Base.DataStructures;

/// <summary>
///     Simple undirected graph implementation.
///     // TODO: reconsider implementation/structure here.
/// </summary>
/// <typeparam name="T"></typeparam>
public class UndirectedGraph<T> : ICloneable, IGraph<T>, IEquatable<UndirectedGraph<T>> where T : notnull
{
    protected readonly SortedDictionary<T, HashSet<T>> EdgeList;

    public UndirectedGraph(int numNodes, IComparer<T>? comparer = null)
    {
        EdgeList = new SortedDictionary<T, HashSet<T>>(comparer ?? Comparer<T>.Default);
        for (var i = 1; i <= numNodes; i++) EdgeList[(T)(object)i] = new HashSet<T>();

        NumNodes = numNodes;
    }

    public UndirectedGraph(IComparer<T>? comparer = null)
    {
        EdgeList = new SortedDictionary<T, HashSet<T>>(comparer ?? Comparer<T>.Default);
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    
    bool IEquatable<UndirectedGraph<T>>.Equals(UndirectedGraph<T>? other)
    {
        return other != null && Equals(other);
    }
    
    public int NumEdges { get; protected set; }
    
    public int NumNodes { get; set; }

    public virtual void Insert(T start, T end)
    {
        EnsureNode(start);
        EnsureNode(end);
        
        if (EdgeList.TryGetValue(start, out var value))
        {
            // By definition, we force any addition to be connected both ways
            if (value.Add(end))
            {
                NumEdges++;
            }
        }

        // need to add both ways
        if (EdgeList.TryGetValue(end, out  value))
        {
            value.Add(start);
        }
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

    public bool AreConnected(T start, T end)
    {
        if (!EdgeList.TryGetValue(start, out _) || !EdgeList.TryGetValue(end, out _))
            throw new InvalidOperationException();
        return BreadthFirstSearchIterative(start, end) > 0;
    }

    public int BreadthFirstSearchIterative(T start, T end)
    {
        if (start.Equals(end))
            return 0;

        var current = new Queue<T>();
        var next = new Queue<T>();
        var traversed = new HashSet<T>();
        current.Enqueue(start);
        var depth = 0;
        while (current.Count > 0)
        {
            var currentNode = current.Dequeue();
            traversed.Add(currentNode);
            if (currentNode.Equals(end)) return depth;

            foreach (var node in EdgeList[currentNode])
                if (!traversed.Contains(node))
                    next.Enqueue(node);

            if (current.Count == 0)
            {
                current = next;
                next = new Queue<T>();
                depth++;
            }
        }

        return -1;
    }

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

    // TODO: consider making this return
    protected void EnsureNode(T item)
    {
        if (EdgeList.TryGetValue(item, out _)) return;
        EdgeList.Add(item, []);
        NumNodes++;
    }
    
    private static bool GraphEquality(UndirectedGraph<T> first, UndirectedGraph<T> other)
    {
        return first.GetEdgeList().Count == other.GetEdgeList().Count &&
               !first.GetEdgeList().Except(other.GetEdgeList()).Any();
    }
}