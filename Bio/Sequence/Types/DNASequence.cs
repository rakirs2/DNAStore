using System.Numerics;
using System.Text;
using Base.Utils;
using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

public class DNASequence(string rawSequence) : NucleotideSequence(rawSequence), IDna
{
    private static readonly Dictionary<char, char> ComplementDict = new()
        { { 'A', 'T' }, { 'T', 'A' }, { 'G', 'C' }, { 'C', 'G' } };

    private static readonly Dictionary<char, int> _charValueMapper = new()
    {
        { 'A', 0 },
        { 'C', 1 },
        { 'G', 2 },
        { 'T', 3 }
    };

    private static readonly Dictionary<int, char> _valueCharMapper = new()
    {
        { 0, 'A' },
        { 1, 'C' },
        { 2, 'G' },
        { 3, 'T' }
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
                var subStringDNA = new DNASequence(Substring(i, j));
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
        for (var i = 0; i < Length; i++) output += _charValueMapper[this[i]] * BigInteger.Pow(4, (int)Length - i - 1);

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

    private static int KmerToNumber(string input)
    {
        var output = 0;
        for (var i = 0; i < input.Length; i++)
            output += _charValueMapper[input[i]] * (int)Math.Pow(4, input.Length - i - 1);

        return output;
    }

    public static DNASequence FromNumber(int number, int k)
    {
        if (number == 0) return new DNASequence(new string('A', k));
        var pattern = new StringBuilder();

        while (number > 0)
        {
            var remainder = number % 4;
            pattern.Insert(0, _valueCharMapper[remainder]);
            number /= 4;
        }

        // Pad the pattern with 'A's if its length is less than k.
        while (pattern.Length < k) pattern.Insert(0, 'A');

        return new DNASequence(pattern.ToString());
    }

    // Should this be static, should this be a class conversion
    // For now, let's just let it be an explicit conversion, pay for the new class
    public RNASequence TranscribeToRNA()
    {
        // TODO: maybe there's a better way to do this
        return new RNASequence(ToString().Replace('T', 'U'));
    }

    /// <summary>
    ///     Generates a new DNA sequence by reading in the reverse of the string and generating the opposite strand
    ///     TODO: What if we can't do this all in memory?
    /// </summary>
    /// <returns></returns>
    public DNASequence GetReverseComplement()
    {
        // TODO: this should probably be all ints
        var dnaStrand = new StringBuilder();
        for (var i = Length - 1; i >= 0; i--) dnaStrand.Append(ComplementDict[this[(int)i]]);

        return new DNASequence(dnaStrand.ToString());
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

    public static DNASequence operator +(DNASequence p1, DNASequence p2)
    {
        return new DNASequence(p1.RawSequence + p2.RawSequence);
    }

    public static void SingleReadToProteinSequences(DNASequence dnaSequence, ref List<ProteinSequence> output)
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
}