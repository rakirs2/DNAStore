using Base.Utils;

namespace Base.Algorithms;

public class ReversalDistance
{
    private int[] _a;
    private int[] _b;
    private ReversalDistance(int[] a, int[] b)
    {
        if (a.Length != b.Length)
        {
            throw new ArgumentException("Lengths must be equal");
        } 
        
        _a = a;
        _b = b;
    }
    
    /// <summary>
    /// There are n^2 possible reversals at every iteration in this implementation.
    /// TODO: Hannenhali and Pevzner's clearer alg (hopefully?)
    /// </summary>
    /// <returns></returns>
    private int Calculate()
    {
        Queue<int[]> currentIteration = new Queue<int[]>();
        Queue<int[]> nextIteration = new Queue<int[]>();

        HashSet<int[]> traversed = new HashSet<int[]>( new IntArrayComparer());
        currentIteration.Enqueue(_a);
        var currentDepth = 0;
        
        while (currentIteration.Count != 0 || nextIteration.Count != 0 && currentDepth <= _a.Length)
        {
            var temp = currentIteration.Dequeue();
            if (temp.SequenceEqual(_b))
            {
                return currentDepth;
            }
            
            traversed.Add(temp);

            for (int i = 1; i <= temp.Length; i++)
            {
                for (int j = 0; j + i <= _b.Length; j++)
                {

                    var other = (int[])temp.Clone();
                    Array.Reverse(other, j, i);

                    if (!traversed.Contains(other))
                    {
                        nextIteration.Enqueue(other);
                    }
                }
            }
            
            if (currentIteration.Count == 0)
            {
                currentIteration = nextIteration;
                nextIteration = new Queue<int[]>();
                currentDepth ++;
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
    /// Really simple definition. if the n+1st term is lt the nth term
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static int CountSignedBreakpoints(int[] p)
    {
        List<int> extendedP = new List<int> { 0 };
        extendedP.AddRange(p);
        // Force add a last element
        extendedP.Add(p.Length + 1);

        int breakpoints = 0;
        
        for (int i = 0; i < extendedP.Count - 1; i++)
        {
            if (extendedP[i + 1] - extendedP[i] != 1)
            {
                breakpoints++;
            }
        }

        return breakpoints;
    }
}