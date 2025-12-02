using Base.Utils;
using Bio.Analysis.Interfaces;
using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

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
    
    public static HashSet<char> ValidAlphabet { get; } = new(new CaseInsensitiveCharComparer())
    {
        'A',
        'C',
        'G',
        'U'
    };
}