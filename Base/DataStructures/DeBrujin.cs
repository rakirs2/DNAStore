using System.Text;

namespace Base.DataStructures;

public class DeBrujin
{
    private readonly UndirectedGraph<string> _underlying;

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

    public void GenerateFromString(string input, int offset = 1)
    {
        AddSequence(input[..^offset], input[offset..]);
    }
}