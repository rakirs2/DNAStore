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

        HashSet<int[]> traversed = new(new IntArrayComparer());
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

    public static int Calculate(int[] a, int[] b)
    {
        return new ReversalDistance(a, b).Calculate();
    }

    /// <summary>
    ///     Really simple definition. if the n+1st term is lt the nth term
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
                ReverseSubsequence(reversals, i - 1, j);
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

    public static void ReverseSubsequence(int[] s, int start, int end)
    {
        if (s == null) throw new ArgumentNullException(nameof(s));

        int left = start, right = end;
        while (left <= right)
        {
            (s[left], s[right]) = (-s[right], -s[left]);
            left++;
            right--;
        }
    }

    public static string ToReversalString(int[] values)
    {
        return "(" + string.Join(" ", values.Select(x => x > 0 ? "+" + x : x.ToString())) + ")";
    }
}