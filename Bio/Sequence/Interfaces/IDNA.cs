using System.Numerics;

namespace Bio.Sequence.Interfaces;

public interface IDna
{
    /// <summary>
    ///     Returns the list of all possible restriction sites and their lengths
    /// </summary>
    /// <remarks>
    ///     It's worth noting that restriction sites are usually lengths 4-8.
    /// </remarks>
    /// <returns></returns>
    List<Tuple<int, int>> RestrictionSites();

    /// <summary>
    ///     Generates the given number for a given DNA sequence
    /// </summary>
    /// <remarks>
    ///     Optimistically assumes that the final number is less than DNA max size.
    /// </remarks>
    /// <returns></returns>
    BigInteger ToNumber();

    /// <summary>
    ///     Returns an array of size 4^n where n is the length of the Kmers being analyzed.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    int[] KmerComposition(int n);
    
    /// <summary>
    ///     Returns the unique Kmer strings as a hashset
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    HashSet<string> KmerCompositionUniqueString(int n);

    double RandomStringProbability(double gcContent);
}