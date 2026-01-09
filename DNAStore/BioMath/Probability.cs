using System.Numerics;
using DNAStore.Base.Utils;
using MathNet.Numerics.Distributions;

namespace DNAStore.BioMath;

// TODO: Split into probability and combinatorics
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
        var total = k + m + n;
        var totalCombinations = 4 * Combinations(total, 2);
        var dominant = 4 * Combinations(k, 2) + 4 * k * m + 4 * k * n + 3 * Combinations(m, 2) + 2 * m * n;

        return (double)(int)dominant / (int)totalCombinations;
    }

    // TODO: there are some computational optimizations that can be done here to avoid BigInteger
    public static BigInteger Combinations(uint n, uint r)
    {
        return Factorial(n) / (Factorial(r) * Factorial(n - r));
    }

    public static BigInteger CombinationsUpTo(uint n, uint r, int modulus = 1000000)
    {
        BigInteger total = 0;
        for (var i = 0; i <= r; i++) total += Combinations(n, (uint)i);
        return total % modulus;
    }

    // TODO: consider implementing a "pascal triangle" and a "pascal's row"
    // TODO: maybe combinatorics should be separated out from probability?
    public static BigInteger CombinationsLargerThan(uint n, uint r, int modulus = 1000000)
    {
        BigInteger total = 0;
        for (var i = r; i <= n; i++) total += Combinations(n, i);
        return total % modulus;
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
            foreach (var character in kmers) initial.Add(character.ToString());

            return initial;
        }

        var output = KmersDriver(currentOutput, currentLength - 1, kmers);


        var newOutput = new List<string>();
        foreach (var bp in kmers)
        foreach (var currentSequence in output)
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

        foreach (var character in inputString)
        {
            var newCurrent = current + character;
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

        for (var i = start; i < numbers.Length; i++)
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
            foreach (var val in tempOutput) output.Add((int[])val.Clone());
        }

        return output;
    }

    public static double SimpleBernoulli(double percentage, int k)
    {
        var b = new Bernoulli(percentage);
        return b.Probability(k);
    }
    
    /// <summary>
    /// Returns likelihood of sharing genes given a probability
    /// </summary>
    /// <remarks>
    /// This is just a binomial CDF underneath the hood. A couple of points worth remembering
    ///     1. This needs to be reversed. 2 ways to do it. Flip 'x' in the CDF calc or subtract from 1
    ///     2. Log space, per Durbin et al. in BSA is a really common trick. There's a lot of reasons for doing it
    ///        but the most important is just calculation flexibility and capability-- especially when dealing with
    ///        extreme numbers
    ///     3. A fun little detour-- why does numerics use double for a discrete value?
    ///        More than enough precision with better perf
    /// </remarks>
    /// <param name="n"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static double[] LikelihoodOfSharingGenes(int n, double p = 0.5)
    {
        var output = new double[n];
        for (var i = 0; i < n; i++)
            output[i] =  Math.Round(Math.Log10(Binomial.CDF(p, n, n-i-1)), 3);
        
        return output;
    }
    
    /// <summary>
    ///     Returns odds of having at least 1 recessive allele
    ///     // TODO: consider adding a Hardy Weinberg class/calculator that can take in distributions
    /// </summary>
    /// <remarks>
    ///     Key here is understanding hardy weinberg formula:
    ///     p^2 + 2pq + q^2 = 1;
    ///     homozygous dominant, heterozygous (carrier), homozygous recessive
    /// 
    ///     p + q = 1
    ///     we're given q^2;
    ///
    ///     q = sqrt(q^2)
    ///     p = 1-q
    ///
    ///     what's the percentage of being a carrier?
    ///     2pq + q^2
    /// </remarks>
    /// <param name="qSquared">percent homozygous recessive</param>
    /// <returns></returns>
    public static double CarrierProbability(double qSquared)
    {
        var q = Math.Sqrt(qSquared);
        return 2 * (1-q)*q + qSquared;
    }

    /// <summary>
    ///     Determines odds of a female being a carrier.
    /// </summary>
    /// <remarks>
    ///     We are given q; we can find p = (1-q)
    ///     How do we  account for the allele being on the X chromosome and "only females"
    ///
    ///     P(carrier | female) = P(carrier AND female)/ P(female)
    ///     P(female) = 1/2
    ///     carrier AND female = 1/2 * 2pq= pq
    ///     Probability = 2pq
    /// </remarks>
    /// <param name="q"></param>
    /// <returns></returns>
    public static double SexLinkedInheritance(double q)
    {
        var p = 1 - q;
        return 2 * p * q;
    }
}