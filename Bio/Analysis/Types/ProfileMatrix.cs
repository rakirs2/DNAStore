using Base.DataStructures;
using Bio.IO;
using Bio.Sequence.Types;
using BioTests.Analysis.Interfaces;

namespace Bio.Analysis.Types;

// For now, it's fine if it's accessible, but if the only use case is for a fasta read, it should probably be subclassed and interfaced
public class ProfileMatrix : IProfileMatrix
{
    public ProfileMatrix(IList<Fasta> inputs)
    {
        // TODO null, 0 length
        Length = inputs[0].Length;
        
        foreach (var input in inputs)
        {
            for (int i = 0; i < input.Length; i++)
            {
            }
        }
    }

    public long Length { get; private set; }
    public long QuantityAnalyzed { get; }
    public AnySequence GetProfileString()
    {
        throw new NotImplementedException();
    }

    public string GetCleanOutput()
    {
        throw new NotImplementedException();
    }

    private List<BasePairDictionary> listOfFrequencies;
}