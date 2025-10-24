using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

/// <summary>
/// Returns suggested error correction for a given class
/// TODO: consider abstracting this to RNA as well
/// </summary>
public class ErrorCorrection(DNASequence a, DNASequence b) : IErrorCorrection<DNASequence>
{
    public override string ToString()
    {
        return Previous.RawSequence + "->" + Suggested.RawSequence;
    }

    public override int GetHashCode()
    {
        return Previous.RawSequence.GetHashCode() ^ Suggested.RawSequence.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ErrorCorrection other)
        {
            return false;
        }

        return Previous.Equals(other.Previous) &&  Suggested.Equals(other.Suggested);
    }

    public DNASequence Previous { get; private set; } = a;
    public DNASequence Suggested { get; private set; } = b;
}