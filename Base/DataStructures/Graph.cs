using Base.Utils;

namespace Base.DataStructures;

// TODO: get this with generics as an exercise
public class Graph
{
    public Graph()
    {
        tracker = new SortedDictionary<int, HashSet<int>>(new IntComparer());
    }

    public void Insert(int start, int end)
    {
        if (tracker.ContainsKey(start))
        {
            tracker[start].Add(end);
        }
        else
        {
            tracker[start] = new HashSet<int>() { end };
        }

        if (tracker.ContainsKey(end))
        {
            tracker[end].Add(start);
        }
        else
        {
            tracker[end] = new HashSet<int>() { start };
        }
    }

    public void Remove<T>(T item)
    {
        throw new NotImplementedException();
    }

    public Dictionary<int, HashSet<int>> GetEdgeList()
    {
        return tracker.ToDictionary();
    }



    private SortedDictionary<int, HashSet<int>> tracker;
}
