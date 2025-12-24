using System.Numerics;
using Base.Utils;
using MathNet.Numerics.Distributions;

namespace BioMath;

public static class Probability
{
    public static int NumberOfSets(int i)
    {
        return (int)Math.Pow(2, i);
    }

    /// <summary>
    ///     Calculated it with modulo 1000000
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static BigInteger NumberOfSetsLarge(int i)
    {
        BigInteger returnval = 1;
        while (i > 0)
        {
            returnval *= 2;
            i--;
        }

        return returnval % 1000000;
    }

    /// <summary>
    ///     nPR. Returns a big integer because these get unwieldy.
    /// </summary>
    /// <param name="n"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static BigInteger NumberOfPermutations(int n, int r)
    {
        if (n < r || r < 0)
            throw new ArgumentException("Invalid n or r values.");

        var result = BigInteger.One;

        // a bit of a cheat, but n!/(n-r)!
        // n=5, r =2
        // so 5*4*3*2*1 /(3*2*1)
        // so we just need n-i
        for (var i = 0; i < r; i++) result *= n - i;

        return result;
    }

    /// <summary>
    ///     This is just permutations but we're special
    /// </summary>
    /// <param name="n"></param>
    /// <param name="r"></param>
    /// <param name="modulo"></param>
    /// <returns></returns>
    public static int PartialPermutations(int n, int r, int modulo = 1000000)
    {
        return (int)(NumberOfPermutations(n, r) % new BigInteger(modulo));
    }

    /// <summary>
    ///     Given a population of x dominant, y heterozygous and z recessive individuals,
    ///     What is the percentage an offspring will have the dominant phenotype?
    /// </summary>
    /// <remarks>
    ///     This is a raw Mendelian Genetics calculator
    /// </remarks>
    /// <returns></returns>
    public static double PercentDominant(uint k, uint m, uint n)
    {
        uint total = k + m + n;
        var totalCombinations = 4 * Combinations(total, 2);
        var dominant = 4 * Combinations(k, 2) + 4 * k * m + 4 * k * n + 3 * Combinations(m, 2) + 2 * m * n;

        return (double)(int)dominant / (int)totalCombinations;
    }

    // TODO: there are some computational optimizations that can be done here to avoid BigInteger
    public static BigInteger Combinations(uint n, uint r)
    {
        return Factorial(n) / (Factorial(r) * Factorial(n - r));
    }

    public static BigInteger Permutations(uint n, uint r)
    {
        // naive: return Factorial(n) / Factorial(n - r);
        return Factorial(n) / Factorial(n - r);
    }

    public static BigInteger Factorial(uint i)
    {
        if (i <= 1)
            return 1;
        return i * Factorial(i - 1);
    }

    /// <summary>
    ///     Terrible perf doesn't handle duplicates as separate. But can work for now
    /// </summary>
    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat([t2]));
    }

    public static List<string> GenerateAllKmers(string inputString, int kmerLength)
    {
        return KmersDriver(new List<string>(), kmerLength, inputString);
    }

    private static List<string>? KmersDriver(List<string> currentOutput, int currentLength, string kmers)
    {
        if (currentLength < 1) return null;

        if (currentLength == 1)
        {
            var initial = new List<string>();
            foreach (char character in kmers) initial.Add(character.ToString());

            return initial;
        }

        var output = KmersDriver(currentOutput, currentLength - 1, kmers);


        var newOutput = new List<string>();
        foreach (char bp in kmers)
        foreach (string currentSequence in output)
            newOutput.Add(bp + currentSequence);

        return newOutput;
    }

    public static List<string> GenerateAllKmersAndSubKmers(string inputString, int maxKmerLength)
    {
        var output = new List<string>();

        GenerateAllkmersAndSubKmers(inputString, "", maxKmerLength, ref output);

        return output;
    }

    private static void GenerateAllkmersAndSubKmers(string inputString, string current, int kmerLength,
        ref List<string> output)
    {
        if (kmerLength == 0) return;

        foreach (char character in inputString)
        {
            string newCurrent = current + character;
            output.Add(newCurrent);
            GenerateAllkmersAndSubKmers(inputString, newCurrent, kmerLength - 1, ref output);
        }
    }

    public static void GenerateSignedPermutations(int[] numbers, int start, HashSet<int[]> results)
    {
        if (start == numbers.Length)
        {
            results.Add((int[])numbers.Clone());
            return;
        }

        for (int i = start; i < numbers.Length; i++)
        {
            GenerateSignedPermutations(numbers, start + 1, results);
            numbers[i] = -numbers[i];
            GenerateSignedPermutations(numbers, start + 1, results);
            numbers[i] = -numbers[i];
        }
    }

    public static double ExpectedDominantOffspring(int AAAA, int AAAa, int AAaa, int AaAa, int Aaaa, int aaaa,
        int children)
    {
        var total = 0.0;
        total += AAAA * children;
        total += AAAa * children;
        total += AAaa * children;
        total += 0.75 * AaAa * children;
        total += 0.5 * Aaaa * children;
        total += 0 * aaaa * children;
        return total;
    }

    /// <summary>
    ///     TODO: clean this up if you need to call it later.
    /// </summary>
    public static IEnumerable<int[]> GenerateSignedPermutations(int highest = 1)
    {
        var values = new List<int>();
        for (var i = 1; i < highest + 1; i++) values.Add(i);

        var perms = GetPermutations(values, highest);
        // post process adding positives and negatives

        var output = new HashSet<int[]>(new IntArrayComparer());
        foreach (var perm in perms)
        {
            var tempOutput = new HashSet<int[]>();
            GenerateSignedPermutations(perm.ToArray(), 0, tempOutput);
            foreach (int[] val in tempOutput) output.Add((int[])val.Clone());
        }

        return output;
    }

    public static double SimpleBernoulli(double percentage, int k)
    {
        var b = new Bernoulli(percentage);
        return b.Probability(k);
    }
}