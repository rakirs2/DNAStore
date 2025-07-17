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

    public List<ProteinSequence> GetCandidateProteinSequences()
    {
        // TODO: should implement a 3 letter ORF class
        // TODO: this should be using the build in iterator
        // TODO: reverse as well
        var output = new List<ProteinSequence>();
        // var complement = ToReverseComplement();

        for (int i = 0; i <= RawSequence.Length - 3; i++)
        {
            if (SequenceHelpers.DNAToProteinCode[RawSequence.Substring(i, 3)].Equals("M"))
            {
                var k = i + 3;
                var current = "M";
                while (current != "Stop" && k < RawSequence.Length)
                {
                    current += SequenceHelpers.DNAToProteinCode[RawSequence.Substring(k, 3)];
                    k += 3;
                }

                output.Add(new ProteinSequence(current));
            }
        }

        return output;
    }

    private static readonly Dictionary<char, char> ComplementDict = new()
        { { 'A', 'T' }, { 'T', 'A' }, { 'G', 'C' }, { 'C', 'G' } };
}