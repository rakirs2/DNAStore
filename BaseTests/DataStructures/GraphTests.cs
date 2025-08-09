namespace Base.DataStructures.Tests;

[TestClass()]
// TOOD: fix
public class GraphTests
{
    [TestMethod]
    public void Clone()
    {
        var graph = new Graph<int>();
        graph.Insert(1, 2);
        var clone = (Graph<int>)graph.Clone();
        Assert.AreNotSame(graph, clone);
        Assert.AreEqual(graph, clone);
    }
}