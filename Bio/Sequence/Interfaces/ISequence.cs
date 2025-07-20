using Bio.Analysis.Types;

namespace Bio.Sequence.Interfaces;

public interface ISequence
{
    /// <summary>
    /// Returns the length of the stored sequence.
    /// </summary>
    long Length { get; }

    /// <summary>
    /// The full sequence getting stored.
    /// </summary>
    string RawSequence { get; }

    /// <summary>
    /// The name of the sequence at construction
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// Returns all locations of a given motif 
    /// </summary>
    long[] MotifLocations(Motif motif, bool isZeroIndex = false);
}