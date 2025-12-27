using DnaStore.Sequence.IO;

namespace DnaStore.Sequence.Analysis.Interfaces;

internal interface IOverlapGraph
{
    public int Number { get; }
    public int MatchLength { get; }
    public List<Tuple<Fasta, Fasta>> GetOverlaps();
}