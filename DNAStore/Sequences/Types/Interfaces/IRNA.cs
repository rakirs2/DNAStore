using System.Numerics;

namespace DNAStore.Sequences.Types.Interfaces;

public interface IRna : ISequence
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

    /// <summary>
    ///     Returns the number of perfect matchings in a RNA graph
    /// </summary>
    /// <remarks>
    ///     A-U and G-C counts need to be the same
    ///     Throws if not
    ///     Does not check for hairpin tightness (AU....) is treated the same as the more likely
    ///     (A......U) as there's less steric force
    ///     AUcount! * GCCount!
    /// </remarks>
    /// <returns></returns>
    public BigInteger NumberOfPerfectMatchings();
}