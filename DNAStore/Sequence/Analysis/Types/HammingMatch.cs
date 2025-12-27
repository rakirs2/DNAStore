using DnaStore.Sequence.Analysis.Interfaces;

namespace DnaStore.Sequence.Analysis.Types;

public class HammingMatch : IHammingMatch
{
    public HammingMatch(string matchString, int tolerance = 0)
    {
        MatchString = matchString;
        ExpectedLength = MatchString.Length;
        Tolerance = tolerance;
    }

    // TODO: maybe some clean up here
    public bool IsMatch(string input)
    {
        throw new NotImplementedException();
    }

    public bool IsMatchStrict(string input)
    {
        return Sequence.Types.Sequence.HammingDistance(input, MatchString) <= Tolerance;
    }

    public int ExpectedLength { get; }
    public int Tolerance { get; }

    public string MatchString { get; }
}