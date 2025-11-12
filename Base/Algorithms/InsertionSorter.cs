namespace Base.Algorithms;

public class InsertionSorter<T>  where  T : IComparable
{
    private int swaps = 0;

    public static int NumberOfSwapsInList(List<T> values)
    {
        return new InsertionSorter<T>(values).swaps;
    }
    
    private InsertionSorter(List<T> values)
    {
        // create a copy
        var temp = values.ToList();
        for (int i = 1; i < values.Count; i++)
        {
            var k = i;
            while (k > 0 && temp[k].CompareTo(temp[k - 1]) <0)
            {
                (temp[k], temp[k - 1]) = (temp[k - 1], temp[k]);
                swaps++;
                k = k-1;
            }
        }
    }
}