using Bio.Sequences.Interfaces;

namespace Bio.Sequences.Types;

/// <summary>
///     Returns suggested error correction for a given class
/// </summary>
public class ErrorCorrection(NucleotideSequence a, NucleotideSequence b) : IErrorCorrection<NucleotideSequence>
{
    public NucleotideSequence Previous { get; } = a;
    public NucleotideSequence Suggested { get; } = b;

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