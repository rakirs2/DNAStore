using Base.DataStructures;

namespace Bio.Sequence.Types;

public class DeBrujin
{
    private readonly Graph<DNASequence> _underlying;

    public DeBrujin()
    {
        _underlying = new Graph<DNASequence>();
    }

    public void AddSequence(DNASequence start, DNASequence end)
    {
        _underlying.Insert(start, end);
    }

    public Dictionary<DNASequence, HashSet<DNASequence>> GetEdgeList()
    {
        return _underlying.GetEdgeList();
    }

    public void GenerateFromString(string input, int offset = 1)
    {
        // TODO: this might need it's own class. It's a read not a full sequence
        var tempSeq = new DNASequence(input);
        AddSequence(new DNASequence(tempSeq[offset..]), new DNASequence(tempSeq[..^offset]));
        var rc = tempSeq.GetReverseComplement();
        AddSequence(new DNASequence(rc[offset..]), new DNASequence(rc[..^offset]));
    }
}