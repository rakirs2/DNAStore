using DNAStore.Sequences.IO;

namespace DNAStore.Sequences.Analysis.Interfaces;

internal interface IOverlapGraph
{
    public int MatchLength { get; }
}