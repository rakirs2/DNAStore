using Bio.Sequences.Types;

namespace Bio.Sequences.Interfaces;

public interface INucleotideSequence
{
    /// <summary>
    ///     Calculates the locations with the min GC skew
    /// </summary>
    /// <returns></returns>
    int[] CalculateMinPrefixGCSkew();

    double TransitionToTransversionRatio(NucleotideSequence other);
}