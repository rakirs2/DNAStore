using System.Text;
using Bio.Sequence.Types;
namespace BioTests.Sequence.Types;

[TestClass]
public class DeBrujinTest
{

    [TestMethod]
    public void Existence()
    {
        var graph = new DeBrujin();
        Assert.IsNotNull(graph);
    }

    [TestMethod]
    public void AddFromString()
    {
        var graph = new DeBrujin();
        graph.GenerateFromString("ACGT");
        var something = graph.GetEdgeList();
        var output = new List<string>();
        foreach (var kvp in something)
        {
            var sb = new StringBuilder();
            sb.Append("(");
            sb.Append(kvp.Key);
            sb.Append(") ");
            foreach (var value in kvp.Value)
            {
                sb.Append(value);
            }
            sb.Append(")");
            var test =  sb.ToString();
        }

        var idea = string.Join('\n', output);
    }
}