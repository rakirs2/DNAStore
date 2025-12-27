namespace DnaStore.Base.DataStructures;

/// <summary>
///     Simple Directed graph implementation
///     separation between directed and undirected is handled at insertion time.
/// </summary>
/// <typeparam name="T"></typeparam>
public class DirectedGraph<T> : UndirectedGraph<T>
{
    public override void Insert(T start, T end)
    {
        if (EdgeList.TryGetValue(start, out var value))
            value.Add(end);
        else
            EdgeList[start] = [end];

        NumEdges++;
    }
}