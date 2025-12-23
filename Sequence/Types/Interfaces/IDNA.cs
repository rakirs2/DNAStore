using System.Numerics;
using Bio.Sequences.Types;

namespace Bio.Sequences.Interfaces;

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

    // TODO: move this to NucleotideSequence
    double RandomStringProbability(double gcContent);

    /// <summary>
    ///     Generates the list of kmers that are within the hamming distance specified
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    HashSet<string> DNeighborhood(int distance);

    /// <summary>
    ///     Returns the minimum distance of a given kmer against the given DNA strand.
    /// </summary>
    int GetMinimumDistanceForKmer(string kmer);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    double[] OddsOfFinding(double[] gcContents, int n);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    string GetComplement();

    /// <summary>
    ///     Returns if thd suggested string actually complements the given strand.
    ///     Assumes orientation is correct coming in.
    /// </summary>
    /// <param name="candidateComplement"></param>
    /// <returns></returns>
    bool Complements(string candidateComplement);

    public DnaSequence GetReverseComplement();
}