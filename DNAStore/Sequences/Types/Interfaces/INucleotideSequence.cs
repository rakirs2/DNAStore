namespace DNAStore.Sequences.Types.Interfaces;

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
    
    /// <summary>
    ///  Is the nucleotide balance between both sets (AU and GC) equal.
    /// </summary>
    /// <returns></returns>
    public bool IsBalanced();
}