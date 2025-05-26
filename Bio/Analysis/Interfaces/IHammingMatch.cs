namespace Bio.Analysis.Interfaces;

public interface IHammingMatch : IMatch
{
    /// <summary>
    /// The number of mismatches allowed on a given match
    /// </summary>
    int Tolerance { get; }


    /// <summary>
    /// The string the IHammingMatchObject checks for
    /// </summary>
    public string MatchString { get; }
}