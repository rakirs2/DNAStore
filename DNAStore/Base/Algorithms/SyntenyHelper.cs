using DNAStore.Base.Utils;

namespace DNAStore.Base.Algorithms;

public static class SyntenyHelper
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

    /// <summary>
    /// Nondestructively generates all possible reversals for the given array. The step of taking the clone of the input
    /// ensures that we don't have to deal with mutations during the iteration.
    /// </summary>
    /// <param name="current"></param>
    /// <param name="signed"></param>
    /// <returns></returns>
    public static IEnumerable<int[]> AllPossibleReversals(int[] current, bool signed = false)
    {
        var temp = (int[])current.Clone();
        for (int i = 0; i < temp.Length - 1; i++)
        {
            for (int j = i+1; j < temp.Length; j++)
            {
                ReverseSubsequence(temp, i, j, signed);
                yield return (int[]) temp.Clone();
                ReverseSubsequence(temp, i, j, signed);
            }
        }
    }
    
    /// <summary>
    ///     Generates the set of candidates, the number of minimal breakpoint reversals in the next gen
    ///     If target is not specified, generates the default set.
    /// </summary>
    /// <remarks>
    ///     This is a potentially silly design. Minimal Breakpoints should return whatever is given as an input.
    ///     If the caller puts the "correct" string, this returns the minimal breakpoints from the standard sequence.
    ///     Hopefully this is always 1.
    ///
    ///     If the caller doesn't add a target, we go ahead and run this against the default sequence.
    ///     TODO: verify this/see if I can prove that.
    /// </remarks>
    /// <param name="current"></param>
    /// <param name="candidates"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int MinimumalBreakPointReversals(int[] current,  out HashSet<int[]> candidates, int[] target = null)
    {
        if (target == null)
        {
            target = new int[current.Length];
            for (int i = 0; i < current.Length; i++)
                target[i] = i + 1;
        }
        
        candidates = new HashSet<int[]>(IntArrayComparer.Shared) { };
        
        // Technically, this should be strictly decreasing. however, we will use int.Max for now
        var reversalMax = int.MaxValue;
        foreach (var candidate in AllPossibleReversals(current))
        {
            // 3 possibilities, candidate has more bp, equal bp, or less bp than the current value
            var numBreakPoints = FindUnsignedBreakPointsWithTarget(candidate, target);
            if (numBreakPoints > reversalMax)
            {
                continue;
            }
            
            if (numBreakPoints < reversalMax)
            {
                reversalMax = numBreakPoints;
                candidates = new HashSet<int[]>(IntArrayComparer.Shared);
            }
            
            candidates.Add(candidate);
        }
        
        return reversalMax;
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