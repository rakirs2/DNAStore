using Bio.Analysis.Interfaces;

namespace Bio.Analysis.Types;
public class HammingMatch : IMotif, IHammingMatch
{
    public string? Name { get; }
    public int ExpectedLength { get; }
    public bool IsMatch(string input)
    {
        throw new NotImplementedException();
    }

    public bool IsMatchStrict(string input)
    {
        throw new NotImplementedException();
    }
}
