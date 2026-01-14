using System.Runtime.InteropServices.JavaScript;
using DNAStore.Base.DataStructures;
using DNAStore.Sequences.Analysis.Interfaces;
using DNAStore.Sequences.IO;

namespace DNAStore.Sequences.Analysis.Types;

public class OverlapGraph : DirectedGraph<string>, IOverlapGraph
{
    public OverlapGraph(int matchLength =1)
    {
        MatchLength = matchLength;
    }
    
    public OverlapGraph(IEnumerable<string> reads, int matchLength =1)
    {
        MatchLength = matchLength;
        foreach (var read in reads)
        {
            
        }
    }
    
    public int MatchLength { get; }
}