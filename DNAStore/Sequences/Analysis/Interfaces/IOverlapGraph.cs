using DNAStore.Base.DataStructures;

namespace DNAStore.Sequences.Analysis.Interfaces;

internal interface IOverlapGraph
{
    public int MatchLength { get; }

    public AddOnlyCounter<string, int> ReadCounts { get; }
}