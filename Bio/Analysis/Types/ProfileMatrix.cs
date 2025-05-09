using Base.DataStructures;
using Bio.Analysis.Interfaces;
using Bio.IO;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;

// For now, it's fine if it's accessible, but if the only use case is for a fasta read, it should probably be subclassed and interfaced
public class ProfileMatrix : IProfileMatrix
{
    public ProfileMatrix(IList<Fasta> inputs)
    {
        // TODO null, 0 length
        LengthOfSequences = inputs[0].RawSequence.Length;
        listOfFrequencies = new List<BasePairDictionary>();

        for (long i = 0; i < LengthOfSequences; i++)
        {
            listOfFrequencies.Add(new BasePairDictionary());
        }

        foreach (var input in inputs)
        {
            for (int i = 0; i < input.Length; i++)
            {
                listOfFrequencies[i].Add(input.RawSequence[i]);
            }
        }

        QuantityAnalyzed = inputs.Count;
    }

    public long LengthOfSequences { get; }
    public long QuantityAnalyzed { get; }
    public AnySequence GetProfileString()
    {
        throw new NotImplementedException();
    }

    public string GetCleanOutput()
    {
        throw new NotImplementedException();
    }

    private readonly List<BasePairDictionary> listOfFrequencies;
}