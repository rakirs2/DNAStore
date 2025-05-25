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
        for (int i = RawSequence.Length - 1; i >= 0; i--) dnaStrand.Append(ComplementDict[RawSequence[i]]);

        return new DNASequence(dnaStrand.ToString());
    }

    public List<Tuple<int, int>> RestrictionSites()
    {
        // Simple, unoptimized algorithm, iterate through string
        // if the reverse complement of the string 
        // n^2 complexity. There might be some interesting palindromic logic but let's avoid that for now
        var output = new List<Tuple<int, int>>();
        for (int i = 0; i < RawSequence.Length; i++)
        {
            int j = 4;
            while (i + j < RawSequence.Length && j < 12)
            {
                var subStringDNA = new DNASequence(RawSequence.Substring(i, j));
                var reverseComplement = subStringDNA.ToReverseComplement();

                j++;
            }
        }

        return output;
    }

    private static readonly Dictionary<char, char> ComplementDict = new()
        { { 'A', 'T' }, { 'T', 'A' }, { 'G', 'C' }, { 'C', 'G' } };

    public List<Tuple<int, int>> CalculateRestrictionSites()
    {
        throw new NotImplementedException();
    }
}