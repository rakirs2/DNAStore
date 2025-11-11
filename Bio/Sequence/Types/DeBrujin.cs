using System.Text;
using Base.DataStructures;

namespace Bio.Sequence.Types;

public class DeBrujin
{
    private readonly UndirectedGraph<DNASequence> _underlying;

    public DeBrujin()
    {
        _underlying = new DirectedGraph<DNASequence>();
    }

    public void AddSequence(DNASequence start, DNASequence end)
    {
        _underlying.Insert(start, end);
    }

    public string GetEdgeList()
    {
        var temp = _underlying.GetEdgeList();
        var output = new List<string>();
        foreach (var kvp in temp)
        foreach (var value in kvp.Value)
        {
            var sb = new StringBuilder();
            sb.Append("(");
            sb.Append(kvp.Key);
            sb.Append(", ");
            sb.Append(value);
            sb.Append(")");
            var test = sb.ToString();
            output.Add(test);
        }

        return string.Join('\n', output);
    }

    public void GenerateFromString(string input, int offset = 1)
    {
        // TODO: this might need it's own class. It's a read not a full sequence
        var tempSeq = new DNASequence(input);
        AddSequence(new DNASequence(tempSeq[..^offset]), new DNASequence(tempSeq[offset..]));
        var rc = tempSeq.GetReverseComplement();
        AddSequence(new DNASequence(rc[..^offset]), new DNASequence(rc[offset..]));
    }
}