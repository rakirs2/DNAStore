using DNAStore.Base.DataStructures;
using DNAStore.Sequences.IO;

namespace DNAStore.Sequences.Analysis.Interfaces;

internal interface IOverlapGraph
{
    public int MatchLength { get; }

    public AddOnlyCounter<string, int> ReadCounts{ get; }
}