using DnaStore.Sequence.Analysis.Interfaces;

namespace DnaStore.Sequence.Analysis.Types;

public class SequenceMatchLocations(Sequence.Types.Sequence sequence, IMatch matchLogic) : ISequenceMatchLocator
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