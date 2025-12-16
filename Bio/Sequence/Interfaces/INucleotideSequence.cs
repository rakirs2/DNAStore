using Bio.Sequence.Types;

namespace Bio.Sequence.Interfaces;

public interface INucleotideSequence
{
    /// <summary>
    ///     Calculates the locations with the min GC skew
    /// </summary>
    /// <returns></returns>
    int[] CalculateMinPrefixGCSkew();
    
    double TransitionToTransversionRatio(NucleotideSequence other);
}