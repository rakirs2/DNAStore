namespace Base.DataStructures.Tests;

[TestClass]
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

    [TestMethod]
    public void EdgesToTree()
    {
        var graph = new Graph<int>(10);
        graph.Insert(1, 2);
        graph.Insert(2, 8);
        graph.Insert(4, 10);
        graph.Insert(5, 9);
        graph.Insert(6, 10);
        graph.Insert(7, 9);
        Assert.AreEqual(3, graph.EdgesToMakeTree());
    }
}