using Bio.Analysis.Types;
using Bio.Sequence.Types;

namespace Bio.Sequence.Interfaces;

public interface ISequence
{
    /// <summary>
    ///     Returns the length of the stored sequence.
    /// </summary>
    long Length { get; }

    /// <summary>
    ///     The name of the sequence at construction
    /// </summary>
    string? Name { get; }

    /// <summary>
    ///     Returns all locations of a given motif
    /// </summary>
    long[] MotifLocations(Motif motif, bool isZeroIndex = false);

    /// <summary>
    ///     Assumes that the instance is the main sequence, removes all possible introns within the sequence
    /// </summary>
    /// <param name="introns"></param>
    /// <returns></returns>
    public AnySequence RemoveIntrons(List<AnySequence> introns);

    /// <summary>
    ///     Returns the indices of the first possible subsequence within the sequence
    /// </summary>
    /// <param name="subsequence"></param>
    /// <param name="isZeroIndex"></param>
    /// <returns></returns>
    public List<int> FindFirstPossibleSubSequence(AnySequence subsequence, bool isZeroIndex = false);

    /// <summary>
    ///     Returns if the DNAString
    /// </summary>
    /// <param name="stringToMatch"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public bool ContainsString(string stringToMatch, int distance);
}