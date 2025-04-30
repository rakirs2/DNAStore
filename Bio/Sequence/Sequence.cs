using Base.DataStructures;

namespace Bio.Sequence;

public abstract class Sequence : ISequence
{
    public long Length { get; private set; }
    public string RawSequence { get; private set; }

    protected Sequence(string rawSequence)
    {
        RawSequence = rawSequence;

        foreach (var basePair in rawSequence)
            // TODO: virtual member call in constructor is an issue? why?
            if (IsValid(basePair))
                Counts.Add(basePair);
            else
                throw new Exception();
    }

    public BasePairDictionary Counts = new();

    // Runs before each value, ensure that the sequence upon store is valid for a given typing
    protected abstract bool IsValid(char bp);
}