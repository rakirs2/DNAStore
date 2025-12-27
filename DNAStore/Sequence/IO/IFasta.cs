using DnaStore.Base.DataStructures;
using DnaStore.Sequence.Types;

namespace DnaStore.Sequence.IO;

public interface IFasta
{
    string Name { get; }
    string RawSequence { get; }
    BasePairDictionary BasePairDictionary { get; }
    long Length { get; }
    public string ToJson();
    public void Compress();

    public Types.Sequence GenerateSequence();
    public RnaSequence GenerateRNASequence();
    public DnaSequence GenerateDNASequence();
    public ProteinSequence GenerateProteinSequence();
}