using Bio.Analysis.Interfaces;
using Bio.Sequences.Types;

namespace Bio.Analysis.Types;

public class SequenceMatchLocations : ISequenceMatchLocator
{
    private readonly IMatch _matchLogic;

    private readonly Sequence _sequence;

    public SequenceMatchLocations(Sequence sequence, IMatch matchLogic)
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