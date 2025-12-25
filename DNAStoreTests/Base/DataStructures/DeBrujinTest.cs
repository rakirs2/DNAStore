using Base.DataStructures;

namespace BaseTests.DataStructures;

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
        Assert.AreEqual("(ACG, CGT)", graph.GetEdgeList());
    }

    [TestMethod]
    public void SimpleString()
    {
        var graph = new DeBrujin();
        graph.GenerateFromString("TGAT");
        Assert.AreEqual("(TGA, GAT)", graph.GetEdgeList());
    }
}