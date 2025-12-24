using System.Numerics;
using Base.Utils;
using Bio.Sequences.Interfaces;
using BioMath;

namespace Bio.Sequences.Types;

public class RnaSequence : NucleotideSequence, IRna
{
    private static readonly HashSet<char> pyrimidines = new(CaseInsensitiveCharComparer.Shared) { 'C', 'T' };
    private static readonly HashSet<char> purines = new(CaseInsensitiveCharComparer.Shared) { 'A', 'G' };

    public RnaSequence(string rawSequence) : base(rawSequence)
    {
    }

    public RnaSequence(string rawSequence, string name) : base(rawSequence, name)
    {
    }

    protected override HashSet<char> Pyrimidines => pyrimidines;

    protected override HashSet<char> Purines => purines;

    public string GetExpectedProteinString()
    {
        return SequenceHelpers.ConvertStringToProtein(ToString());
    }

    public int GetPotentialNumberOfDNAStrings(int mod)
    {
        throw new NotImplementedException();
    }

    public BigInteger NumberOfPerfectMatchings()
    {
        if (Counts.GetFrequency('A') == Counts.GetFrequency('U') &&
            Counts.GetFrequency('C') == Counts.GetFrequency('C'))
        {
            var gcFreq = Probability.Factorial((uint)Counts.GetFrequency('C'));
            var auFreq = Probability.Factorial((uint)Counts.GetFrequency('A'));

            return gcFreq * auFreq;
        }

        throw new ArgumentException("The AC and GC counts must be equal for this analysis");
    }

    protected override bool IsValid(char c)
    {
        return SequenceHelpers.IsValidRNA(c);
    }
}