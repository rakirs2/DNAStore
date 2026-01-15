using DNAStore.Base.DataStructures;

namespace DNAStoreTests.Base.DataStructures;

[TestClass]
public class DirectedGraphTest
{
    [TestMethod]
    public void SimpleBFS()
    {
        var graph = new DirectedGraph<int>(2);
        graph.Insert(1, 2);
        Assert.AreEqual(-1, graph.BreadthFirstSearchIterative(2, 1));
    }

    [TestMethod]
    public void BreathFirstSearchDirection()
    {
        var graph = new DirectedGraph<int>(2);
        graph.Insert(1, 2);
        Assert.AreEqual(1, graph.BreadthFirstSearchIterative(1, 2));
    }

    [TestMethod]
    public void BreathFirstSearchGiven()
    {
        var graph = new DirectedGraph<int>(6);
        graph.Insert(4, 6);
        graph.Insert(6, 5);
        graph.Insert(4, 3);
        graph.Insert(3, 5);
        graph.Insert(2, 1);
        graph.Insert(1, 4);
        Assert.AreEqual(0, graph.BreadthFirstSearchIterative(1, 1));
        Assert.AreEqual(-1, graph.BreadthFirstSearchIterative(1, 2));
        Assert.AreEqual(2, graph.BreadthFirstSearchIterative(1, 3));
        Assert.AreEqual(1, graph.BreadthFirstSearchIterative(1, 4));
        Assert.AreEqual(3, graph.BreadthFirstSearchIterative(1, 5));
        Assert.AreEqual(2, graph.BreadthFirstSearchIterative(1, 6));
    }

    [TestMethod]
    public void InsertingEdgeOnEmptyGraph()
    {
        var graph = new DirectedGraph<int>();
        graph.Insert(4, 6);
        Assert.AreEqual(1, graph.NumEdges);
        Assert.AreEqual(2, graph.NumNodes);
    }

    [TestMethod]
    public void DoubleInsertGraph()
    {
        var graph = new DirectedGraph<int>();
        graph.Insert(4, 6);
        Assert.AreEqual(1, graph.NumEdges);
        Assert.AreEqual(2, graph.NumNodes);
        graph.Insert(4, 6);
        Assert.AreEqual(1, graph.NumEdges);
        Assert.AreEqual(2, graph.NumNodes);
    }
}