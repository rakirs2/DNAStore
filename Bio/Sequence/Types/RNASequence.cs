using Base.Utils;
using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

// TODO: rename to RnaSequence
public class RNASequence : NucleotideSequence, IRNA
{
    public RNASequence(string rawSequence) : base(rawSequence)
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

    protected override bool IsValid(char c)
    {
        return SequenceHelpers.IsValidRNA(c);
    }
    
    private static HashSet<char> pyrimidines = new HashSet<char>(new CaseInsensitiveCharComparer()) { 'C', 'T' };
    private static HashSet<char> purines = new HashSet<char>(new CaseInsensitiveCharComparer()) { 'A', 'G' };
    
    protected override HashSet<char> Pyrimdines =>pyrimidines;
    
    protected override HashSet<char> Purines => purines;

}