using System.Diagnostics.SymbolStore;
using System.Transactions;
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

    /// <summary>
    ///     There are n^2 possible reversals at every iteration in this implementation.
    ///     TODO: Hannenhali and Pevzner's clearer alg (hopefully?)
    /// </summary>
    /// <returns></returns>
    private int Calculate()
    {
        Queue<int[]> currentIteration = new();
        Queue<int[]> nextIteration = new();

        HashSet<int[]> traversed = new(IntArrayComparer.Shared);
        currentIteration.Enqueue(_a);
        var currentDepth = 0;

        while (currentIteration.Count != 0 || (nextIteration.Count != 0 && currentDepth <= _a.Length))
        {
            var temp = currentIteration.Dequeue();
            if (temp.SequenceEqual(_b)) return currentDepth;

            traversed.Add(temp);

            for (var i = 1; i <= temp.Length; i++)
            for (var j = 0; j + i <= _b.Length; j++)
            {
                var other = (int[])temp.Clone();
                Array.Reverse(other, j, i);

                if (!traversed.Contains(other)) nextIteration.Enqueue(other);
            }

            if (currentIteration.Count == 0)
            {
                currentIteration = nextIteration;
                nextIteration = new Queue<int[]>();
                currentDepth++;
            }
        }

        // This shouldn't be reached
        return -1;
    }
    // TODO: Hannenhali and Pevzner for signed
    
    
    /// <summary>
    /// Euna Parks Greedy Exact Algorithm
    /// </summary>
    /// <remarks>
    /// Her implementation is loosely pseudocoded as follows:
    ///     1. Take all possible reversals
    ///     2. Get the Set of reversals with the smallest bp count
    ///     3. Greedily keep going with the set until you have the absolute smallest count and reiterate
    ///
    /// This was really cool. How can we prove that this is strictly decreasing.
    /// </remarks>
    /// <see>
    /// Park, Euna, "Exact and Approximation Algorithms for Computing Reversal Distances in Genome Rearrangement" (2008). Master's Projects. 104.
    /// DOI: https://doi.org/10.31979/etd.qm9e-d3gt
    /// https://scholarworks.sjsu.edu/etd_projects/104 
    /// </see>
    /// <returns></returns>
    private int ParksGreedyExactAlgorithm()
    {
        // TODO: consider throwing if any values are signed
        // TODO: consider uints
        HashSet<int[]> traversed = new HashSet<int[]>(IntArrayComparer.Shared);
        Queue<int[]> currentIteration = new();
        var depth = 0;
        traversed.Add(_a);
        currentIteration.Enqueue(_a);
        while (!traversed.Contains(_b))
        {
            // Go ahead and BFS here
            var nextIteration = new Queue<int[]>();
            var nextGenMinBp = int.MaxValue;
            foreach (var candidate in currentIteration)
            {
                var currentBp = SyntenyHelper.MinimumalBreakPointReversals(candidate, out var nextGenCandidates, _b);
                if (currentBp > nextGenMinBp)
                    continue;
                if (currentBp < nextGenMinBp)
                {
                    nextGenMinBp = currentBp;    
                    nextIteration.Clear();
                }
                
                foreach (var nextGen in nextGenCandidates)
                {
                    nextIteration.Enqueue(nextGen);
                    traversed.Add(nextGen);
                }
            }
            currentIteration = nextIteration;
            depth++;
        }
        return depth;
    }
    
    public static int Calculate(int[] a, int[] b)
    {
        return new ReversalDistance(a, b).Calculate();
    }
    
    public static int CalculateParksGreedyExact(int[] a, int[] b)
    {
        return new ReversalDistance(a, b).ParksGreedyExactAlgorithm();
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

    public static string ToReversalString(int[] values)
    {
        return "(" + string.Join(" ", values.Select(x => x > 0 ? "+" + x : x.ToString())) + ")";
    }
}