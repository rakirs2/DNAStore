using Base.DataStructures;

namespace Bio.Sequence.Types;

/// <summary>
/// Base class for any sequence. This is the main driver for all types of analysis where the program does not
/// know what type of string we are analyzing.
///
/// TODO: this should eventually have 1 static creator which can take in a string and returns the implied typing
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
            // Ah it's a design flaw on my part -- what's a better way to do this
            // abstract, 
            
            if (IsValid(basePair))
            {
                Counts.Add(basePair);
            }
            else
            {
                throw new Exception();
            }

        Length = RawSequence.Length;
    }

    public BasePairDictionary Counts = new();

    /// <summary>
    /// This is a pretty simple cleanup t
    /// </summary>
    /// <param name="bp"></param>
    /// <returns></returns>
    protected virtual bool IsValid(char bp)
    {
        return true;
    }
}