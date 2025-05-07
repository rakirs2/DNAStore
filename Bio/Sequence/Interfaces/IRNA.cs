namespace Bio.Sequence.Interfaces;

public interface IRNA : ISequence
{
    /// <summary>
    /// Returns a protein sequence from a given RNA strand.
    /// </summary>
    /// <returns></returns>
    public string GetExpectedProteinString();
}