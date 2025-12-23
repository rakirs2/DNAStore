namespace Bio.Analysis.Interfaces;

public interface ISequenceMatchLocator
{
    /// <summary>
    ///     Returns the match locations of the given object
    /// </summary>
    /// <returns></returns>
    List<int> GetLocations();
}