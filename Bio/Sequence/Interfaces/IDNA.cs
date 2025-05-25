namespace Bio.Sequence.Interfaces;
public interface IDNA
{
    /// <summary>
    ///  Returns the list of all possible restriction sites and their lengths
    /// </summary>
    /// <returns></returns>
    List<Tuple<int, int>> CalculateRestrictionSites();
}
