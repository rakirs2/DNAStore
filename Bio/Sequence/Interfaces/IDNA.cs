using System.Numerics;
using Bio.Sequence.Types;

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
    /// Generates the list of a neighbors of a given sequence
    /// with Hamming Distance allowance of 'd'
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    List<DNASequence> GenerateDNeighbors(int d);
}