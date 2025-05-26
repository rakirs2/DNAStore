using System.Text.RegularExpressions;

namespace Bio.Analysis.Interfaces;
public interface IRegexMatch
{
    /// <summary>
    /// The original string added at construction.
    /// </summary>
    string InputMotif { get; }

    /// <summary>
    /// Mostly for debugging purposes. This is the actual regex operator that the input string gets converted to
    /// </summary>
    Regex UnderlyingRegex { get; }
}
