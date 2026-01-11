using DNAStore.Base.Utils;

namespace DNAStore.Base.Algorithms;

public class ReversalDistance
{
    private readonly int[] _a;
    private readonly int[] _b;

    private ReversalDistance(int[] a, int[] b)
    {
        if (a.Length != b.Length) throw new ArgumentException("Lengths must be equal");

        _a = a;
        _b = b;
    }
    
    // TODO: Hannenhali and Pevzner for signed

    /// <summary>
    /// Euna Parks Greedy Exact Algorithm
    /// </summary>
    /// <param name="reversals"></param>
    /// <remarks>
    /// Her implementation is loosely pseudocoded as follows:
    ///     1. Take all possible reversals
    ///     2. Get the Set of reversals with the smallest bp count
    ///     3. Greedily keep going with the set until you have the absolute smallest count and reiterate
    /// 
    /// Any given reversal can decrease the amount of breakpoints by 0,1,2. Why?
    /// The calculation for breakpoints asks if the nth element is out of place with the (n+1)th
    ///     If we have an array a, b, c, d, e, f
    ///     We compare the following:
    ///         (a,b), (b,c), (c,d), (d,e), (e,f)
    ///     Any reversal within a, b, c, d, e ,f might result in something like
    ///         a, e, d, c, b, f
    ///     Our comparisons are now:
    ///         (a,e), (e,d), (d,c), (c,b), (b,f)
    ///     The breakpoint calculation is commutative. We take the absolute value of the difference
    ///         (d,e), (d,c), (b,c), (a,e), (b,f)
    ///     and removing duplicates we have 2 new comparisons
    ///         (a,e) and (b,f)
    ///     and lose 2 comparisons (a,b) and (e,f)
    /// Any reversal can reduce bp, therefore, by at most 2 as every non boundary comparison within the reversal is preserved
    ///
    /// Because any breakpoint makes a reversal set absolutely impossible to sort
    /// Any reversal that removes the most breakpoints will be strictly more effective.
    ///
    /// Ok, so why does this work at 0 bp?
    /// At 0 bp, we effectively do an exhaustive BFS of all possible reversals and return the one with the lowest depth.
    /// This is, by definition, going to find the optimal solution.
    /// </remarks>
    /// <see>
    /// Park, Euna, "Exact and Approximation Algorithms for Computing Reversal Distances in Genome Rearrangement" (2008). Master's Projects. 104.
    /// DOI: https://doi.org/10.31979/etd.qm9e-d3gt
    /// https://scholarworks.sjsu.edu/etd_projects/104 
    /// </see>
    /// <returns></returns>
    private int ParksGreedyExactAlgorithm(out List<Tuple<int, int>> reversals)
    {
        HashSet<int[]> traversed = new HashSet<int[]>(IntArrayComparer.Shared);
        var currentIteration = new Queue<ReversalDistanceTracker>();
        var depth = 0;
        traversed.Add(_a);
        reversals = new List<Tuple<int, int>>();
        currentIteration.Enqueue(new ReversalDistanceTracker(_a));
        while (!traversed.Contains(_b))
        {
            // Go ahead and BFS here
            var nextIteration = new Queue<ReversalDistanceTracker>();
            var nextGenMinBp = int.MaxValue;
            foreach (var candidate in currentIteration)
            {
                var currentBp = SyntenyHelper.MinimumalBreakPointReversals(candidate.Values, out var nextGenCandidates, _b);
                if (currentBp > nextGenMinBp)
                    continue;
                if (currentBp < nextGenMinBp)
                {
                    nextGenMinBp = currentBp;    
                    nextIteration.Clear();
                }
                
                foreach (var nextGen in nextGenCandidates)
                {
                    var nextRevTracker =
                        candidate.GetNext(nextGen.Item1, new Tuple<int, int>(nextGen.Item2, nextGen.Item3));
                    nextIteration.Enqueue(nextRevTracker);
                    traversed.Add(nextGen.Item1);
                    if (IntArrayComparer.Shared.Equals(_b, nextGen.Item1))
                    {
                        reversals = nextRevTracker.Reversals;
                        // TODO: verify you can return depth + 1 here.
                    }
                }
            }
            currentIteration = nextIteration;
            depth++;
        }
        
        return depth;
    }
    
    public static int CalculateGreedy(int[] a, int[] b, out List<Tuple<int, int>> reversals)
    {
        return new ReversalDistance(a, b).ParksGreedyExactAlgorithm(out reversals);
    }
    
    /// <summary>
    ///     Basic Greedy Reversal sort. The order is completely optional. It exists because the problem required it
    /// </summary>
    /// <remarks>
    ///     We're assuming 1-indexing, and a clean set of data.
    /// </remarks>
    /// <param name="reversals"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    public static int ApproximateGreedyReversalSort(int[] reversals, out List<int[]> order)
    {
        var n = reversals.Length;
        order = new List<int[]>();
        for (var i = 1; i <= n; i++)
            if (reversals[i - 1] != i)
            {
                // greedily find the right index 
                var j = Array.FindIndex(reversals, x => Math.Abs(x) == i);
                SyntenyHelper.ReverseSubsequence(reversals, i - 1, j);
                var temp = (int[])reversals.Clone();
                order.Add(temp);
                // Force the value here to be positive, could just call the function on the index but no need
                if (reversals[i - 1] == -i)
                {
                    reversals[i - 1] = i;

                    var t2 = (int[])reversals.Clone();
                    order.Add(t2);
                }
            }

        return order.Count;
    }

    private class ReversalDistanceTracker
    {
        public int[] Values;
        public List<Tuple<int, int>> Reversals { get; }
        
        public ReversalDistanceTracker(int[] values,  List<Tuple<int, int>> reversals = null!)
        {
            Values = values;
            Reversals = reversals ?? [];
        }

        public ReversalDistanceTracker GetNext( int[] nextValue, Tuple<int, int> nextReversal )
        {
            var reversals = Reversals.ToList();
            reversals.Add(nextReversal);
            return new ReversalDistanceTracker(nextValue, reversals);
        }
    }
    public static string ToReversalString(int[] values)
    {
        return "(" + string.Join(" ", values.Select(x => x > 0 ? "+" + x : x.ToString())) + ")";
    }
}