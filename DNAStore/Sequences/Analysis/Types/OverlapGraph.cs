using DNAStore.Base.DataStructures;
using DNAStore.Sequences.Analysis.Interfaces;
namespace DNAStore.Sequences.Analysis.Types;

public class OverlapGraph :  IOverlapGraph
{
    // Naive implementation for now. We keep track of prefixes and suffixes of the connection.
    // TODO: this should probably be replaced with a Trie.
    // The underlying graph only keeps track of the starts and
    private readonly DirectedGraph<string> _underlyingGraph = new DirectedGraph<string>();
    private readonly Trie _trie = new Trie();
    public OverlapGraph(int matchLength =1)
    {
        MatchLength = matchLength;
    }
    
    public OverlapGraph(IEnumerable<string> reads, int matchLength =1)
    {
        MatchLength = matchLength;
        foreach (var read in reads)
        {
            Insert(read);
        }
    }

    public void Insert(string read)
    {
        _trie.AddWord(read);
        _underlyingGraph.Insert(read[..^MatchLength], read[MatchLength..]);
        ReadCounts.Add(read);
    }
    
    public int MatchLength { get; }
    public AddOnlyCounter<string, int> ReadCounts { get; }
}