using System.Text;

namespace DNAStore.Base.DataStructures;

public class DeBrujin
{
    private readonly DirectedGraph<string> _underlying;

    public DeBrujin()
    {
        _underlying = new DirectedGraph<string>();
    }

    public void AddSequence(string start, string end)
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
    
    public string GetEdgeListAlternate()
    {
        var temp = _underlying.GetEdgeList();
        var output = new List<string>();
        foreach (var kvp in temp)
        {
            var sb = new StringBuilder();
            sb.Append(kvp.Key);
            sb.Append(" -> ");
            if(kvp.Value.Count == 0) continue;
            sb.Append(string.Join(",", kvp.Value));
            output.Add(sb.ToString());
        }

        return string.Join('\n', output);
    }

    public void GenerateFromString(string input, int offset = 1)
    {
        AddSequence(input[..^offset], input[offset..]);
    }
}