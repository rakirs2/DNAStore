using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

public class RNASequence : NucleotideSequence, IRNA
{
    public RNASequence(string rawSequence) : base(rawSequence)
    {
    }

    protected override bool IsValid(char c)
    {
        return SequenceHelpers.IsValidRNA(c);
    }

    public string GetExpectedProteinString()
    {
        return SequenceHelpers.ConvertStringToProtein(RawSequence);
    }

    public int GetPotentialNumberOfDNAStrings(int mod)
    {
        throw new NotImplementedException();
    }
}