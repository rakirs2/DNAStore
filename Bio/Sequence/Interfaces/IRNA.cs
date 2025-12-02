namespace Bio.Sequence.Interfaces;

public interface IRNA : IStrictSequence
{
    /// <summary>
    ///     Returns a protein sequence from a given RNA strand.
    /// </summary>
    /// <returns></returns>
    public string GetExpectedProteinString();

    /// <summary>
    ///     Returns the number of DNA strings from the given RNA sequence mod the input
    /// </summary>
    /// <returns></returns>
    public int GetPotentialNumberOfDNAStrings(int mod);
}