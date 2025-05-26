using System.Text;
using System.Text.RegularExpressions;

using Bio.Analysis.Interfaces;

namespace Bio.Analysis.Types;

// TODO: this needs some refactoring

/// <summary>
/// Motifs are usually written as something like:
/// aaaa -- 4 a's in a row
/// [ab]cd{e} -- either a or b, followed by c,d and ending with something that's not e
///
/// These are effectively the same as regex matches except for the not case
/// So a Motif object, at construction, takes in the protein match string, converts it to the underlying
/// base regex and can determine if a given input string is an exact match.
/// <remarks>
/// Right now, this implementation is case-sensitive.
/// </remarks>
/// </summary>
public class Motif : IMotif
{
    public Motif(string motif, int expectedLength = 0, string name = "")
    {
        Name = name;
        InputMotif = motif;
        UnderlyingRegex = new Regex(MotifToRegexString(InputMotif), RegexOptions.Compiled);
        ExpectedLength = expectedLength;
    }

    public string Name { get; }
    public string InputMotif { get; }
    public int ExpectedLength { get; }
    public Regex UnderlyingRegex { get; }

    /// <summary>
    /// Returns if a given sequence matches the underlying motif
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public bool IsMatch(string input)
    {
        return UnderlyingRegex.IsMatch(input);
    }

    public bool IsMatchStrict(string input)
    {
        return input.Length == ExpectedLength && IsMatch(input);
    }

    private static string MotifToRegexString(string input)
    {
        var output = new StringBuilder();
        foreach (var c in input)
            if (c == '{')
                output.Append("[^");
            else if (c == '}')
                output.Append("]");
            else
                output.Append(c);

        return output.ToString();
    }
}