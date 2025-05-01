namespace Bio.Sequence.Types;

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
}