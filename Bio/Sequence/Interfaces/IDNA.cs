using System.Numerics;

namespace Bio.Sequence.Interfaces;

public interface IDNA
{
    /// <summary>
    ///     Returns the list of all possible restriction sites and their lengths
    /// </summary>
    /// <remarks>
    ///     It's worth noting that restriction sites are usually lengths 4-8. This defaults to all of length 4-12
    /// </remarks>
    /// <returns></returns>
    List<Tuple<int, int>> RestrictionSites();

    /// <summary>
    /// Generates the given number for a given DNA sequence
    /// </summary>
    /// <remarks>
    /// Optimistically assumes that the final number is less than DNA max size.
    /// </remarks>
    /// <returns></returns>
    BigInteger ToNumber();

    /// <summary>
    /// Returns an array of size 4^n where n is the length of the Kmers being analyzed.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    int[] KmerComposition(int n);
}