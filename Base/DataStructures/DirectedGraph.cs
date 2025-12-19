namespace Base.DataStructures;

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
    // TODO: tests
    public override void Remove(T item)
    {
        if (EdgeList.TryGetValue(item, out var edge))
        {
            var nodesRemoved = 0;
            foreach (var kvp in EdgeList)
            {
                if (kvp.Value.Contains(item))
                {
                    kvp.Value.Remove(item);
                    nodesRemoved++;
                }
            }

            EdgeList.Remove(item);
            NumEdges-= nodesRemoved;
            NumNodes--;
        }
    }
}