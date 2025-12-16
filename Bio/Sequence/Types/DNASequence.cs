using System.Numerics;
using System.Text;
using Base.Utils;
using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

public class DnaSequence(string rawSequence) : NucleotideSequence(rawSequence), IDna
{
    private static readonly Dictionary<char, char> ComplementDict = new()
        { { 'A', 'T' }, { 'T', 'A' }, { 'G', 'C' }, { 'C', 'G' } };

    private static HashSet<char> pyrimidines = new HashSet<char>(new CaseInsensitiveCharComparer()) { 'C', 'T' };
    private static HashSet<char> purines = new HashSet<char>(new CaseInsensitiveCharComparer()) { 'A', 'G' };

    private static readonly Dictionary<char, int> CharValueMapper = new()
    {
        { 'A', 0 },
        { 'C', 1 },
        { 'G', 2 },
        { 'T', 3 }
    };

    private static readonly Dictionary<int, char> ValueCharMapper = new()
    {
        { 0, 'A' },
        { 1, 'C' },
        { 2, 'G' },
        { 3, 'T' }
    };

    private static readonly HashSet<char> ValidAlphabet = new()
    {
        'A',
        'C',
        'G',
        'T'
    };

    public List<Tuple<int, int>> RestrictionSites()
    {
        // Simple, unoptimized algorithm, iterate through string
        // if the reverse complement of the string 
        // n^2 complexity. There might be some interesting palindromic logic but let's avoid that for now
        var output = new List<Tuple<int, int>>();
        for (var i = 0; i < Length; i++)
        {
            var j = 4;
            // TODO: verify these
            while (i + j <= Length && j <= 12)
            {
                var subStringDNA = new DnaSequence(Substring(i, j));
                var reverseComplement = subStringDNA.GetReverseComplement();
                if (AreSequenceEqual(subStringDNA, reverseComplement)) output.Add(new Tuple<int, int>(i + 1, j));
                j++;
            }
        }

        return output;
    }

    // TODO: longs and ints was a stupid decision
    // ints all the way unless needed otherwise
    public BigInteger ToNumber()
    {
        BigInteger output = 0;
        for (var i = 0; i < Length; i++) output += CharValueMapper[this[i]] * BigInteger.Pow(4, (int)Length - i - 1);

        return output;
    }

    public int[] KmerComposition(int n)
    {
        if (n <= 0)
            throw new ArgumentException("n must be positive");
        var output = new int[(int)Math.Pow(4, n)];
        for (var i = 0; i < Length - n + 1; i++)
            // this is effectively the same thing as "ToNumber()"
            // however, we don't need all the overhead of the DNA class.
            output[KmerToNumber(Substring(i, n))]++;

        return output;
    }

    public HashSet<string> KmerCompositionUniqueString(int n)
    {
        if (n <= 0)
            throw new ArgumentException("n must be positive");
        var output = new HashSet<string>();
        for (var i = 0; i < Length - n + 1; i++)
            // this is effectively the same thing as "ToNumber()"
            // however, we don't need all the overhead of the DNA class.
            output.Add(Substring(i, n));

        return output;
    }

    public double RandomStringProbability(double gcContent)
    {
        var bpToPercentage = new Dictionary<char, double>(new CaseInsensitiveCharComparer())
        {
            { 'g', gcContent / 2 },
            { 'c', gcContent / 2 },
            { 't', (1 - gcContent) / 2 },
            { 'a', (1 - gcContent) / 2 }
        };

        var percentage = 0.0;
        foreach (char bp in RawSequence) percentage += Math.Log10(bpToPercentage[bp]);

        return percentage;
    }

    public HashSet<string> DNeighborhood(int d)
    {
        var neighborhood = new HashSet<string> { RawSequence };
        GenerateNeighborhoodRecursive(RawSequence.ToCharArray(), d, 0, neighborhood);
        return neighborhood;
    }

    public int GetMinimumDistanceForKmer(string kmer)
    {
        if (kmer.Length > RawSequence.Length) throw new ArgumentException();

        var minimum = int.MaxValue;
        for (var i = 0; i < RawSequence.Length - kmer.Length + 1; i++)
            minimum = Math.Min(minimum, HammingDistance(Substring(i, kmer.Length), kmer));

        return minimum;
    }

    private static void GenerateNeighborhoodRecursive(char[] currentPatternChars, int remainingDistance, int startIndex,
        HashSet<string> neighborhood)
    {
        // Base case: if no more distance allowed, add the current pattern to the neighborhood
        if (remainingDistance == 0)
        {
            neighborhood.Add(new string(currentPatternChars));
            return;
        }

        // Base case: if we've reached the end of the string, and still have remaining distance,
        // it means we can't make enough changes within the string length, so we stop.
        if (startIndex == currentPatternChars.Length) return;

        // Option 1: Don't change the character at the current position
        GenerateNeighborhoodRecursive(currentPatternChars, remainingDistance, startIndex + 1, neighborhood);

        // Option 2: Change the character at the current position
        char originalChar = currentPatternChars[startIndex];
        foreach (char newChar in ValidAlphabet)
            if (newChar != originalChar)
            {
                currentPatternChars[startIndex] = newChar;
                GenerateNeighborhoodRecursive(currentPatternChars, remainingDistance - 1, startIndex + 1, neighborhood);
            }

        // Backtrack: restore the original character for subsequent calls
        currentPatternChars[startIndex] = originalChar;
    }

    private static int KmerToNumber(string input)
    {
        var output = 0;
        for (var i = 0; i < input.Length; i++)
            output += CharValueMapper[input[i]] * (int)Math.Pow(4, input.Length - i - 1);

        return output;
    }

    public static DnaSequence FromNumber(int number, int k)
    {
        if (number == 0) return new DnaSequence(new string('A', k));
        var pattern = new StringBuilder();

        while (number > 0)
        {
            int remainder = number % 4;
            pattern.Insert(0, ValueCharMapper[remainder]);
            number /= 4;
        }

        // Pad the pattern with 'A's if its length is less than k.
        while (pattern.Length < k) pattern.Insert(0, 'A');

        return new DnaSequence(pattern.ToString());
    }

    // Should this be static, should this be a class conversion
    // For now, let's just let it be an explicit conversion, pay for the new class
    public RNASequence TranscribeToRNA()
    {
        return new RNASequence(ToString().Replace('T', 'U'));
    }

    /// <summary>
    ///     Generates a new DNA sequence by reading in the reverse of the string and generating the opposite strand
    /// </summary>
    /// <returns></returns>
    public DnaSequence GetReverseComplement()
    {
        var dnaStrand = new StringBuilder();
        for (long i = Length - 1; i >= 0; i--) dnaStrand.Append(ComplementDict[this[(int)i]]);

        return new DnaSequence(dnaStrand.ToString());
    }

    /// <summary>
    ///     Simple algorithm using Open Reading frames. We go through each possible starting location.
    ///     An Open Reading Frame, by definition, must contain a start or Methionine and a stop codon.
    /// </summary>
    /// <returns></returns>
    public List<ProteinSequence> GetCandidateProteinSequences()
    {
        // TODO: should implement a 3 letter ORF class
        // TODO: this should be using the built in iterator
        // TODO: reverse as well
        var values = new List<ProteinSequence>();
        // var complement = ToReverseComplement();

        SingleReadToProteinSequences(this, ref values);
        SingleReadToProteinSequences(GetReverseComplement(), ref values);
        // TODO: This is terrible, terrible perf wise and bad form.
        // But, it might be the right answer for now

        HashSet<string> filter = new();
        List<ProteinSequence> output = new();
        foreach (var sequence in values)
            if (!filter.Contains(sequence.ToString()))
            {
                output.Add(sequence);
                filter.Add(sequence.ToString());
            }


        return output.ToList();
    }

    public static DnaSequence operator +(DnaSequence p1, DnaSequence p2)
    {
        return new DnaSequence(p1.RawSequence + p2.RawSequence);
    }

    public static void SingleReadToProteinSequences(DnaSequence dnaSequence, ref List<ProteinSequence> output)
    {
        for (var i = 0; i <= dnaSequence.Length - 3; i++)
            if (SequenceHelpers.DNAToProteinCode[dnaSequence.Substring(i, 3)].Equals("M"))
            {
                int k = i + 3;
                var seqToAdd = "M";
                while (k <= dnaSequence.Length - 3)
                {
                    string current = SequenceHelpers.DNAToProteinCode[dnaSequence.Substring(k, 3)];
                    if (current.Equals("Stop"))
                    {
                        output.Add(new ProteinSequence(seqToAdd));
                        break;
                    }

                    seqToAdd += current;
                    k += 3;
                }
            }
    }

    protected override HashSet<char> Pyrimdines =>pyrimidines;
    
    protected override HashSet<char> Purines => purines;
}