namespace Base.Utils;

public class IntComparer : IComparer<int>
{
    // TODO: tests
    public int Compare(int x, int y)
    {
        return x.CompareTo(y);
    }
}