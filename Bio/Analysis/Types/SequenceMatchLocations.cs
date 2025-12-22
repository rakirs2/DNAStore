using Bio.Analysis.Interfaces;
using Bio.Sequences.Types;

namespace Bio.Analysis.Types;

public class SequenceMatchLocations(Sequence sequence, IMatch matchLogic) : ISequenceMatchLocator
{
    public List<int> GetLocations()
    {
        var output = new List<int>();
        for (var i = 0; i < sequence.Length - matchLogic.ExpectedLength; i++)
            if (matchLogic.IsMatchStrict(sequence.Substring(i, matchLogic.ExpectedLength)))
                output.Add(i);

        return output;
    }
}