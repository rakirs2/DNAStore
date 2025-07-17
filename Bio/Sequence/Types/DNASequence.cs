using System.Text;

using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

public class DNASequence(string rawSequence) : NucleotideSequence(rawSequence), IDNA
{
    // Should this be static, should this be a class conversion
    // For now, let's just let it be an explicit conversion, pay for the new class
    public RNASequence TranscribeToRNA()
    {
        return new RNASequence(RawSequence.Replace('T', 'U'));
    }

    /// <summary>
    /// Generates a new DNA sequence by reading in the reverse of the string and generating the opposite strand
    /// TODO: What if we can't do this all in memory?
    /// </summary>
    /// <returns></returns>
    public DNASequence ToReverseComplement()
    {
        var dnaStrand = new StringBuilder();
        for (var i = RawSequence.Length - 1; i >= 0; i--) dnaStrand.Append(ComplementDict[RawSequence[i]]);

        return new DNASequence(dnaStrand.ToString());
    }

    public List<Tuple<int, int>> RestrictionSites()
    {
        // Simple, unoptimized algorithm, iterate through string
        // if the reverse complement of the string 
        // n^2 complexity. There might be some interesting palindromic logic but let's avoid that for now
        var output = new List<Tuple<int, int>>();
        for (var i = 0; i < RawSequence.Length; i++)
        {
            var j = 4;
            // TODO: verify these
            while (i + j <= RawSequence.Length && j <= 12)
            {
                var subStringDNA = new DNASequence(RawSequence.Substring(i, j));
                var reverseComplement = subStringDNA.ToReverseComplement();
                if (AreSequenceEqual(subStringDNA, reverseComplement)) output.Add(new Tuple<int, int>(i + 1, j));
                j++;
            }
        }

        return output;
    }

    /// <summary>
    /// Simple algorithm using Open Reading frames. We go through each possible starting location.
    /// An Open Reading Frame, by definition, must contain a start or Methionine and a stop codon.
    /// </summary>
    /// <returns></returns>
    public List<ProteinSequence> GetCandidateProteinSequences()
    {
        // TODO: should implement a 3 letter ORF class
        // TODO: this should be using the build in iterator
        // TODO: reverse as well
        var output = new List<ProteinSequence>();
        // var complement = ToReverseComplement();

        SingleReadToProteinSequences(this, ref output);
        SingleReadToProteinSequences(ToReverseComplement(), ref output);

        return output;
    }

    public static void SingleReadToProteinSequences(DNASequence dnaSequence, ref List<ProteinSequence> output)
    {
        for (int i = 0; i <= dnaSequence.RawSequence.Length - 3; i++)
        {
            if (SequenceHelpers.DNAToProteinCode[dnaSequence.RawSequence.Substring(i, 3)].Equals("M"))
            {
                var k = i + 3;
                var seqToAdd = "M";
                while (k <= dnaSequence.RawSequence.Length - 3)
                {
                    var current = SequenceHelpers.DNAToProteinCode[dnaSequence.RawSequence.Substring(k, 3)];
                    if (current.Equals("Stop"))
                    {
                        output.Add(new ProteinSequence(seqToAdd));
                    }

                    seqToAdd += current;
                    k += 3;
                }
            }
        }
    }

    private static readonly Dictionary<char, char> ComplementDict = new()
        { { 'A', 'T' }, { 'T', 'A' }, { 'G', 'C' }, { 'C', 'G' } };
}