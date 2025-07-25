namespace Base.DataStructures.Tests;

[TestClass()]
[Ignore]
// TOOD: fix
public class GraphTests
{

    [TestMethod()]
    [Ignore]
    public void GraphTest()
    {
        var graph = new Graph();
        graph.Insert(1, 2);
        var expected = new Dictionary<int, HashSet<int>>()
        {
            {
                1, new HashSet<int>(){2}
            },

            {
                2, new HashSet<int>(){1}
            }
        };
        var actual = graph.GetEdgeList();


    }

    [TestMethod()]
    public void InsertTest()
    {
        Assert.Fail();
    }

    [TestMethod()]
    public void RemoveTest()
    {
        Assert.Fail();
    }

    [TestMethod()]
    public void GetEdgeListTest()
    {
        Assert.Fail();
    }

    [TestMethod()]
    public void InsertTest1()
    {
        Assert.Fail();
    }
}