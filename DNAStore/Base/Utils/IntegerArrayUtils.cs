namespace DNAStore.Base.Utils;

///     TODO: It might be possible to combine all of these.
///     TODO: It also might be worth putting this in a separate class/putting them in the algorithms folder
public static class IntegerArrayUtils
{
    /// <summary>
    /// Returns the number of unsigned breakpoints.
    /// </summary>
    /// <param name="current"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int FindUnsignedBreakPointsWithTarget(int[] current, int[] target)
    {
        var bp = 0;
        for (int i = 0; i < current.Length - 1; i++)
        {
            if (Math.Abs(Array.IndexOf(current, target[i]) - Array.IndexOf(current, target[i+1]))>1)
            {
                bp++;
            }
        }
        
        return bp;
    }

    public static IEnumerable<int[]> AllPossibleReversals(int[] current, bool signed = false)
    {
        for (int i = 0; i < current.Length - 1; i++)
        {
            for (int j = i+1; j < current.Length; j++)
            {
                ReverseSubsequence(current, i, j, signed);
                yield return (int[]) current.Clone();
                ReverseSubsequence(current, i, j, signed);
            }
        }
    }
    
    public static int MinimumalBreakPointReversals(int[] current, int[] target, out HashSet<int[]> candidates)
    {
        candidates = new HashSet<int[]>(IntArrayComparer.Shared) { };
        var reversalMax = FindUnsignedBreakPointsWithTarget(current, target);
        
        return 0;
    }
    
    public static void ReverseSubsequence(int[] s, int start, int end, bool signed = true)
    {
        if (s == null) throw new ArgumentNullException(nameof(s));

        int left = start, right = end;
        while (left <= right)
        {
            if(signed)
                (s[left], s[right]) = (-s[right], -s[left]);
            else
                (s[left], s[right]) = (s[right], s[left]);
            left++;
            right--;
        }
    }
    
    /// <summary>
    ///     Really simple definition. if the n+1st term is lt the nth term
    ///     // TODO: Rething this/reread Bergeron
    ///     // TODO: consider unsigned def
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static int CountSignedBreakpoints(int[] p)
    {
        var extendedP = new List<int> { 0 };
        extendedP.AddRange(p);
        // Force add a last element
        extendedP.Add(p.Length + 1);

        var breakpoints = 0;

        for (var i = 0; i < extendedP.Count - 1; i++)
            if (extendedP[i + 1] - extendedP[i] != 1)
                breakpoints++;

        return breakpoints;
    }
}