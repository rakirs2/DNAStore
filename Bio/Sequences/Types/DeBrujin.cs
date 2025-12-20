using System.Text;
using Base.DataStructures;

namespace Bio.Sequences.Types;

// TODO: this should be in the base class
public class DeBrujin
{   
    // TODO: this should be for a string, not a DNA Sequence
    // TODO: this belongs in the Base class
    private readonly UndirectedGraph<DnaSequence> _underlying;

    public DeBrujin()
    {
        _underlying = new DirectedGraph<DnaSequence>();
    }

    public void AddSequence(DnaSequence start, DnaSequence end)
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
        var tempSeq = new DnaSequence(input);
        AddSequence(new DnaSequence(tempSeq[..^offset]), new DnaSequence(tempSeq[offset..]));
        var rc = tempSeq.GetReverseComplement();
        AddSequence(new DnaSequence(rc[..^offset]), new DnaSequence(rc[offset..]));
    }
}