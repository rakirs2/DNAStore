namespace DnaStore.Sequence.Types.Interfaces;

public interface INucleotideSequence
{
    /// <summary>
    ///     Calculates the locations with the min GC skew
    /// </summary>
    /// <returns></returns>
    int[] CalculateMinPrefixGCSkew();

    double TransitionToTransversionRatio(NucleotideSequence other);

    /// <summary>
    ///     Returns the ratio of the GC content in
    /// </summary>
    /// <returns></returns>
    double GCRatio();
}