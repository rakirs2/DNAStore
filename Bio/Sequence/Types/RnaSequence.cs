using System.Numerics;
using Base.Utils;
using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

public class RnaSequence : NucleotideSequence, IRna
{
    public RnaSequence(string rawSequence) : base(rawSequence)
    {
    }

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
            var gcFreq = BioMath.Probability.Factorial((uint)Counts.GetFrequency('C'));
            var auFreq = BioMath.Probability.Factorial((uint)Counts.GetFrequency('A'));
            
            return gcFreq * auFreq;
        }
        
        throw new ArgumentException("The AC and GC counts must be equal for this analysis");
    }

    protected override bool IsValid(char c)
    {
        return SequenceHelpers.IsValidRNA(c);
    }
    
    private static HashSet<char> pyrimidines = new HashSet<char>(new CaseInsensitiveCharComparer()) { 'C', 'T' };
    private static HashSet<char> purines = new HashSet<char>(new CaseInsensitiveCharComparer()) { 'A', 'G' };
    
    protected override HashSet<char> Pyrimdines =>pyrimidines;
    
    protected override HashSet<char> Purines => purines;

}