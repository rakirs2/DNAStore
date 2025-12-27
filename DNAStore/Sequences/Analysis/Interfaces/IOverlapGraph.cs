using DNAStore.Sequences.IO;

namespace DNAStore.Sequences.Analysis.Interfaces;

internal interface IOverlapGraph
{
    public int Number { get; }
    public int MatchLength { get; }
    public List<Tuple<Fasta, Fasta>> GetOverlaps();
}