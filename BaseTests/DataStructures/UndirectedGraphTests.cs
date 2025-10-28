namespace Base.DataStructures.Tests;

[TestClass]
public class UndirectedGraphTests
{
    [TestMethod]
    public void Clone()
    {
        var graph = new UndirectedGraph<int>();
        graph.Insert(1, 2);
        var clone = (UndirectedGraph<int>)graph.Clone();
        Assert.AreNotSame(graph, clone);
        Assert.AreEqual(graph, clone);
    }

    [TestMethod]
    public void EdgesToTree()
    {
        var graph = new UndirectedGraph<int>(10);
        graph.Insert(1, 2);
        graph.Insert(2, 8);
        graph.Insert(4, 10);
        graph.Insert(5, 9);
        graph.Insert(6, 10);
        graph.Insert(7, 9);
        Assert.AreEqual(3, graph.EdgesToMakeTree());
    }
}