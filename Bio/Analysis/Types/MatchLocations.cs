using Bio.Analysis.Interfaces;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;
public class MatchLocations : IMatchSuperStructure
{
    public MatchLocations(AnySequence sequence, IMatch matchLogic)
    {
        _sequence = sequence;
        _matchLogic = matchLogic;
    }

    private readonly AnySequence _sequence;
    private readonly IMatch _matchLogic;

    public List<int> GetLocations()
    {
        var output = new List<int>();
        for (int i = 0; i < _sequence.RawSequence.Length - _matchLogic.ExpectedLength; i++)
        {
            if (_matchLogic.IsMatchStrict(_sequence.RawSequence.Substring(i, _matchLogic.ExpectedLength)))
            {
                output.Add(i);
            }
        }

        return output;
    }

}
