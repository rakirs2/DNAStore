using System.Text;
using Base.DataStructures;

namespace Bio.Sequences.Types;

// TODO: add an interface here
public class DeBrujin 
{
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

    /// <summary>
    /// Assumes 
    /// </summary>
    /// <param name="reads"></param>
    /// <param name="offset"></param>
    public void GenerateFromList(List<string> reads, int offset = 1, bool checkRc = false)
    {
        foreach (var read in reads)
        {
            var tempSeq = new DnaSequence(read);
            AddSequence(new DnaSequence(read[..^offset]), new DnaSequence(read[offset..]));
            if (checkRc)
            {
                var rc = tempSeq.GetReverseComplement();
                AddSequence(new DnaSequence(rc[..^offset]), new DnaSequence(rc[offset..]));
            }
        }
    }

    public string AssembleEulerianCycle()
    {
        //start somewhere
        var startingpoint = _underlying.GetEdgeList().Keys.First();
        var sb = new StringBuilder();
        while (_underlying.NumNodes > 0)
        {
            
        }
        
        
        return sb.ToString();
    }
}