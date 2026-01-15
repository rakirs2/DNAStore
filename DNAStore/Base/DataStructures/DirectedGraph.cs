namespace DNAStore.Base.DataStructures;

/// <summary>
///     Simple Directed graph implementation
///     separation between directed and undirected is handled at insertion time.
/// </summary>
/// <typeparam name="T"></typeparam>
public class DirectedGraph<T> : UndirectedGraph<T>
{
    public DirectedGraph()
    {
    }

    public DirectedGraph(int nodes) : base(nodes)
    {
    }

    // TODO: this has a bug. We need to insert the nodes for both here.
    // TODO: consider using the actual node vs the values. For now it's a very minute difference.
    public override void Insert(T start, T end)
    {
        EnsureNode(start);
        EnsureNode(end);

        if (EdgeList.TryGetValue(start, out var value))
            if (value.Add(end))
                NumEdges++;
    }
}