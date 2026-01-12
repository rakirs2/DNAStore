using DNAStore.Base.DataStructures;

namespace DNAStoreTests.Base.DataStructures;

[TestClass]
public class UndirectedGraphTest
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

    [TestMethod]
    public void NumberOfConnectedComponents()
    {
        // TODO: there should be a better way to do this
        var graph = new UndirectedGraph<int>(12);
        graph.Insert(1, 2);
        graph.Insert(1, 5);
        graph.Insert(5, 9);
        graph.Insert(5, 10);
        graph.Insert(9, 10);
        graph.Insert(3, 4);
        graph.Insert(3, 7);
        graph.Insert(3, 8);
        graph.Insert(4, 8);
        graph.Insert(7, 11);
        graph.Insert(8, 11);
        graph.Insert(11, 12);
        graph.Insert(8, 12);
        Assert.AreEqual(3, graph.NumberOfConnectedComponents());
    }

    [TestMethod]
    public void NumberOfConnectedComponentsNone()
    {
        var graph = new UndirectedGraph<int>(0);
        Assert.AreEqual(0, graph.NumberOfConnectedComponents());
    }

    [TestMethod]
    public void NumberOfConnectedComponentsBase()
    {
        var graph = new UndirectedGraph<int>(1);
        Assert.AreEqual(1, graph.NumberOfConnectedComponents());
    }

    [TestMethod]
    public void SimpleGraphConnectedness()
    {
        var graph = new UndirectedGraph<int>(2);
        graph.Insert(1, 2);
        Assert.IsTrue(graph.AreConnected(1, 2));
    }
}