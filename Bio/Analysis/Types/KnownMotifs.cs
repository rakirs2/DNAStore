namespace Bio.Analysis.Types;

/// <summary>
///     A simple list of known motifs. These aren't separated by DNA, RNA and Protein (yet)
/// </summary>
public static class KnownMotifs
{
    public static RegexMotif NGlycostatin => new("N{P}[ST]{P}", 4, "N-Glycosatin");
}