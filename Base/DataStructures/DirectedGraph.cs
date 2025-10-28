namespace Base.DataStructures;

/// <summary>
/// Possible overdone. Buty we ned some space for directed vs non directed implementations
/// </summary>
/// <typeparam name="T"></typeparam>
public class DirectedGraph<T> : UndirectedGraph<T> 
{
    public override void Insert(T start, T end)
    {
        if (_tracker.TryGetValue(start, out var value))
            value.Add(end);
        else
            _tracker[start] = [end];

        NumEdges++;
    }
}