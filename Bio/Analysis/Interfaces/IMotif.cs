using System.Text.RegularExpressions;

namespace Bio.Analysis.Interfaces;

internal interface IMotif
{
    string Name { get; }

    /// <summary>
    /// The original string added at contruction
    /// </summary>
    string InputMotif { get; }

    /// <summary>
    /// Used for a strict motif match.
    /// </summary>
    int ExpectedLength { get; }

    /// <summary>
    /// Mostly for debugging purposes. This is the actual regex operater that the input string gets converted to
    /// </summary>
    Regex UnderlyingRegex { get; }

    /// <summary>
    /// Returns if the string matches the underlying motif
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public bool IsMatch(string input);

    /// <summary>
    /// Returns if the string matches the underlying motif and the expected Length
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public bool IsMatchStrict(string input);
}