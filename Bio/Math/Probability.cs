using System.Numerics;

namespace Bio.Math;

// TODO: find a new namespace that's not math
// Consider BioMath as a new project?
public static class Probability
{
    /// <summary>
    /// Given a population of x dominant, y heterozygous and z recessive individuals,
    /// What is the percentage an offspring will have the dominant phenotype?
    /// </summary>
    /// <remarks>
    /// This is a raw Mendelian Genetics calculator</remarks>
    /// <returns></returns>
    public static double PercentDominant(uint k, uint m, uint n)
    {
        var total = k + m + n;
        var totalCombinations = 4 * Combinations(total, 2);
        var dominant = (4 * Combinations(k, 2)) + (4 * k * m) + (4 * k * n) + (3 * Combinations(m, 2)) + (2 * m * n);

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
    /// Terrible perf doesn't handle duplicates as separate. But can work for now
    /// </summary>
    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat([t2]));
    }

    /*
     *nucleotides = ['A', 'T', 'C', 'G']

       def generate_all_sequences(n):

           if n < 1:
               return []

           if n == 1:
               return nucleotides

           sub_sequences = generate_all_sequences(n - 1)

           sequences = []

           for sequence in sub_sequences:

               for nucleotide in nucleotides:

                        sequences.append(nucleotide + sequence)

           return sequences
     */

    public static List<string> GenerateAllKmers(string inputString, int kmerLength)
    {
        return KmersDriver(new List<string>() { }, kmerLength, inputString);
    }

    private static List<string>? KmersDriver(List<string> currentOutput, int currentLength, string kmers)
    {
        if (currentLength < 1) return null;

        if (currentLength == 1)
        {
            var initial = new List<string>() { };
            // initialize
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

    private static void GenerateAllkmersAndSubKmers(string inputString, string current, int kmerLength, ref List<string> output)
    {
        if (kmerLength == 0)
        {
            return;
        }

        foreach (var character in inputString)
        {
            var newCurrent = current + character;
            output.Add(newCurrent);
            GenerateAllkmersAndSubKmers(inputString, newCurrent, kmerLength - 1, ref output);
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
}