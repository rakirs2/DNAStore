using DNAStore.Base.DataStructures;
using DNAStore.Sequences.Types;

namespace DNAStore.Sequences.IO;

public interface IFasta
{
    string Name { get; }
    string RawSequence { get; }
    BasePairDictionary BasePairDictionary { get; }
    long Length { get; }
    public string ToJson();
    public void Compress();

    public Sequence GenerateSequence();
    public RnaSequence GenerateRNASequence();
    public DnaSequence GenerateDNASequence();
    public ProteinSequence GenerateProteinSequence();
}