namespace DNAStore.Base.Utils;

public class IntArrayComparer : IEqualityComparer<int[]>
{
    public bool Equals(int[] x, int[] y)
    {
        // Check for reference equality first for performance
        if (ReferenceEquals(x, y)) return true;
        // Check for nulls
        if (x is null || y is null) return false;
        // Check for sequence equality using LINQ
        return x.SequenceEqual(y);
    }

    public int GetHashCode(int[] obj)
    {
        if (obj is null) return 0;

        // some prime number hash
        unchecked
        {
            var hash = 17;
            foreach (var item in obj) hash = hash * 23 + item.GetHashCode();
            return hash;
        }
    }
}