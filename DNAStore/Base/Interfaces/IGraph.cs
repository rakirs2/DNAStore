namespace DNAStore.Base.Interfaces;

public interface IGraph<T>
{
    /// <summary>
    /// Number of Nodes in the graph
    /// </summary>
    int NumNodes { get; }
    
    /// <summary>
    /// Number of edges in the graph
    /// </summary>
    int NumEdges { get; }
    
    /// <summary>
    /// Add a edge to the graph
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    void Insert(T start, T end);
    
    /// <summary>
    /// Remove a Node
    /// </summary>
    /// <param name="item"></param>
    void Remove(T item);
    
    /// <summary>
    /// Returns EdgeList
    /// </summary>
    /// <returns></returns>
    Dictionary<T, HashSet<T>> GetEdgeList();
    
    /// <summary>
    /// Really simple calculation 
    /// </summary>
    /// <returns></returns>
    int EdgesToMakeTree();
    
    bool AreConnected(T start, T end);
    
    /// <summary>
    /// Returns the path length from start to end of a given graph.
    /// -1 indicates no connection
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    
    int BreadthFirstSearchIterative(T start, T end);
}