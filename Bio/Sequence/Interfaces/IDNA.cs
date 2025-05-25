namespace Bio.Sequence.Interfaces;
public interface IDNA
{
    /// <summary>
    ///  Returns the list of all possible restriction sites and their lengths
    /// </summary>
    /// <remarks>
    /// It's worth noting that restriction sites are usually lengths 4-8. This defaults to all of length 4-12
    /// </remarks>
    /// <returns></returns>
    List<Tuple<int, int>> RestrictionSites();
}
