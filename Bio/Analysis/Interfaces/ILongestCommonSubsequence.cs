using Bio.Sequences.Types;

namespace Bio.Analysis.Interfaces;

// Only cares about 1, not all
public interface ILongestCommonSubsequence
{
    /// <summary>
    ///     Returns any longest subsequence in the List
    /// </summary>
    /// <returns></returns>
    Sequence GetAnyLongest();
}