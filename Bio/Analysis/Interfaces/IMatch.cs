namespace Bio.Analysis.Interfaces;

public interface IMatch
{
    public int ExpectedLength { get; }

    /// <summary>
    ///     Returns if the string matches the underlying motif
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public bool IsMatch(string input);

    /// <summary>
    ///     Returns if the string matches the underlying motif and the expected Length
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public bool IsMatchStrict(string input);
}