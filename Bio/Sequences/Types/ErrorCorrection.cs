using Bio.Sequences.Interfaces;

namespace Bio.Sequences.Types;

/// <summary>
///     Returns suggested error correction for a given class
///     TODO: consider abstracting this to RNA as well
/// </summary>
public class ErrorCorrection(DnaSequence a, DnaSequence b) : IErrorCorrection<DnaSequence>
{
    public DnaSequence Previous { get; } = a;
    public DnaSequence Suggested { get; } = b;

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
        if (obj is not ErrorCorrection other) return false;

        return Previous.Equals(other.Previous) && Suggested.Equals(other.Suggested);
    }
}