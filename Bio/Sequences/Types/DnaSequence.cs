using System.Numerics;
using System.Text;
using Base.Utils;
using Bio.Sequences.Interfaces;

namespace Bio.Sequences.Types;

public class DnaSequence(string rawSequence) : NucleotideSequence(rawSequence), IDna
{
    private static readonly Dictionary<char, char> ComplementDict = new(new CaseInsensitiveCharComparer())
        { { 'A', 'T' }, { 'T', 'A' }, { 'G', 'C' }, { 'C', 'G' } };

    private static readonly HashSet<char> pyrimidines = new(new CaseInsensitiveCharComparer()) { 'C', 'T' };
    private static readonly HashSet<char> purines = new(new CaseInsensitiveCharComparer()) { 'A', 'G' };

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

    private static readonly HashSet<char> ValidAlphabet = new(new CaseInsensitiveCharComparer())
    {
        'A',
        'C',
        'G',
        'T'
    };

    private static readonly HashSet<char> cgDict = new(new CaseInsensitiveCharComparer())
    {
        'C',
        'G'
    };

    private static readonly HashSet<char> atDict = new(new CaseInsensitiveCharComparer())
    {
        'A',
        'T'
    };

    protected override HashSet<char> Pyrimidines => pyrimidines;

    protected override HashSet<char> Purines => purines;

    public List<Tuple<int, int>> RestrictionSites()
    {
        // Simple, unoptimized algorithm, iterate through string
        // if the reverse complement of the string 
        // n^2 complexity. There might be some interesting palindromic logic but let's avoid that for now
        var output = new List<Tuple<int, int>>();
        for (var i = 0; i < Length; i++)
        {
            var j = 4;
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
        foreach (var bp in RawSequence) percentage += Math.Log10(bpToPercentage[bp]);

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

    /// <summary>
    ///     Adding up a bunch of probilities. Not the hardest thing but it's a good problem
    /// </summary>
    /// <param name="length"></param>
    /// <param name="gcContent"></param>
    /// <returns></returns>
    public double[] OddsOfFinding(double[] gcContent, int number)
    {
        if (Length > int.MaxValue)
            throw new ArgumentException("The length of the DNA string is way too long for this analysis");

        var sLen = (int)Length;
        var possiblePositions = number - sLen + 1;

        var expectedValues = new double[gcContent.Length];

        for (var i = 0; i < gcContent.Length; i++)
        {
            var gc = gcContent[i];
            var at = 1 - gc;
            var output = 1.0;

            // Note: because this runs off the DnaSequence, we are guaranteed valid letters a
            // that is part of the contract at construction
            foreach (var nucleotide in RawSequence)
                if (cgDict.Contains(nucleotide))
                    output *= gc / 2.0;
                else
                    output *= at / 2.0;

            expectedValues[i] = possiblePositions * output;
        }

        return expectedValues;
    }

    private static void GenerateNeighborhoodRecursive(char[] currentPatternChars, int remainingDistance, int startIndex,
        HashSet<string> neighborhood)
    {
        if (remainingDistance == 0)
        {
            neighborhood.Add(new string(currentPatternChars));
            return;
        }

        if (startIndex == currentPatternChars.Length) return;

        GenerateNeighborhoodRecursive(currentPatternChars, remainingDistance, startIndex + 1, neighborhood);

        var originalChar = currentPatternChars[startIndex];
        foreach (var newChar in ValidAlphabet)
            if (newChar != originalChar)
            {
                currentPatternChars[startIndex] = newChar;
                GenerateNeighborhoodRecursive(currentPatternChars, remainingDistance - 1, startIndex + 1, neighborhood);
            }

        // Restore
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
            var remainder = number % 4;
            pattern.Insert(0, ValueCharMapper[remainder]);
            number /= 4;
        }

        // Pad 
        while (pattern.Length < k) pattern.Insert(0, 'A');

        return new DnaSequence(pattern.ToString());
    }

    // Should this be static, should this be a class conversion
    // For now, let's just let it be an explicit conversion, pay for the new class
    public RnaSequence TranscribeToRNA()
    {
        return new RnaSequence(ToString().Replace('T', 'U'));
    }

    /// <summary>
    ///     Generates a new DNA sequence by reading in the reverse of the string and generating the opposite strand
    /// </summary>
    /// <returns></returns>
    public DnaSequence GetReverseComplement()
    {
        var dnaStrand = new StringBuilder();
        for (var i = Length - 1; i >= 0; i--) dnaStrand.Append(ComplementDict[this[(int)i]]);

        return new DnaSequence(dnaStrand.ToString());
    }
    
    /// <summary>
    ///     Generates a new DNA sequence by reading in the reverse of the string and generating the opposite strand
    /// </summary>
    /// <returns></returns>
    public string GetComplement()
    {
        var sb = new StringBuilder();
        foreach (var res in RawSequence)
        {
            sb.Append(ComplementDict[res]);
        }

        return sb.ToString();
    }

    public bool Complements(string candidateComplement)
    {
        return PerfectComplementStrand(RawSequence,candidateComplement);
    }

    /// <summary>
    ///     Simple algorithm using Open Reading frames. We go through each possible starting location.
    ///     An Open Reading Frame, by definition, must contain a start or Methionine and a stop codon.
    /// </summary>
    /// <returns></returns>
    public List<ProteinSequence> GetCandidateProteinSequences()
    {
        // TODO: this should be using the built in iterator
        var values = new List<ProteinSequence>();
        SingleReadToProteinSequences(this, ref values);
        SingleReadToProteinSequences(GetReverseComplement(), ref values);
        // TODO: This is terrible, terrible perf wise and bad form.
        // TODO: proteinhashes and dna hashes should make the following redundant

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
                var k = i + 3;
                var seqToAdd = "M";
                while (k <= dnaSequence.Length - 3)
                {
                    var current = SequenceHelpers.DNAToProteinCode[dnaSequence.Substring(k, 3)];
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

    public static double GetProbabilityOccuringGivenGCContent(string subsequence, int sequenceLength, double gcContent)
    {
        var probability = 1.0;
        foreach (var c in subsequence)
            // Probably faster to do the manual check but i'd rather have this be case insensitive
            if (cgDict.Contains(c))
                probability *= gcContent / 2.0;
            else if (atDict.Contains(c))
                probability *= (1.0 - gcContent) / 2.0;
            else throw new ArgumentException("character " + sequenceLength);

        return 1.0 - Math.Pow(1.0 - probability, sequenceLength);
    }

    /// <summary>
    /// A raw string check to see if two strands line up. Assumes orientation has been handled
    /// 
    /// </summary>
    /// <param name="seqA"></param>
    /// <param name="seqB"></param>
    /// <returns></returns>
    public static bool PerfectComplementStrand(string seqA, string seqB)
    {
        if (seqA.Length != seqB.Length)
        {
            // TODO: consider creating an invalid length comparison expcetion
            throw new ArgumentException("Invalid Lengths");
        }

        for (int i = 0; i < seqA.Length; i++)
        {
            if (ComplementDict[seqA[i]] != seqB[i])
            {
                return false;
            }
        }
        
        return true;
    }
}