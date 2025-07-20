using Bio.Analysis.Interfaces;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;

public class SequenceMatchLocations : ISequenceMatchLocator
{
    private readonly IMatch _matchLogic;

    private readonly AnySequence _sequence;

    public SequenceMatchLocations(AnySequence sequence, IMatch matchLogic)
    {
        _sequence = sequence;
        _matchLogic = matchLogic;
    }

    public List<int> GetLocations()
    {
        var output = new List<int>();
        for (var i = 0; i < _sequence.Length - _matchLogic.ExpectedLength; i++)
            if (_matchLogic.IsMatchStrict(_sequence.Substring(i, _matchLogic.ExpectedLength)))
                output.Add(i);

        return output;
    }
}