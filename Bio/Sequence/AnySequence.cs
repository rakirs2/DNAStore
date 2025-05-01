using Base.DataStructures;

namespace Bio.Sequence;

/// <summary>
/// Base class for any sequence. This is the main driver for all types of analysis where we do not
/// know what type of string we are analyzing.
///
/// TODO: this should eventually have 1 creator which can take in a string and returns the cor
/// </summary>
public class AnySequence : ISequence
{
    public long Length { get; }
    public string RawSequence { get; }

    public AnySequence(string rawSequence)
    {
        RawSequence = rawSequence;

        foreach (var basePair in rawSequence)
            // TODO: virtual member call in constructor is an issue? why?
            if (IsValid(basePair))
                Counts.Add(basePair);
            else
                throw new Exception();

        Length = RawSequence.Length;
    }

    public BasePairDictionary Counts = new();

    // Runs before each value, ensure that the sequence upon store is valid for a given typing
    protected bool IsValid(char bp)
    {
        return true;
    }
}