namespace Bio.Sequence.Types;

public class RNASequence : AnySequence
{
    public RNASequence(string rawSequence) : base(rawSequence)
    {
    }

    protected override bool IsValid(char c)
    {
        return SequenceHelpers.IsValidRNA(c);
    }
}