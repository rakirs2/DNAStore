using DNAStore.Base.Algorithms;

namespace DNAStoreTests.Base.Algorithms;

[TestClass]
public class ReversalDistanceTest
{
    [TestMethod]
    public void ApproximateGreedyReversalTest()
    {
        Assert.AreEqual(7, ReversalDistance.ApproximateGreedyReversalSort(new[] { -3, 4, 1, 5, -2 }, out _));
    }

    [TestMethod]
    public void ParksHandlesBaseCase()
    {
        var start = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var target = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var expectedReversal = new List<Tuple<int, int>>();

        Assert.AreEqual(0, ReversalDistance.CalculateGreedy(start, target, out var reversals));
        Assert.IsTrue(expectedReversal.SequenceEqual(reversals));
    }

    [TestMethod]
    public void ParksSimpleReversal()
    {
        var start = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var target = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 10, 9 };

        var expectedReversal = new List<Tuple<int, int>>
        {
            new(8, 9)
        };

        Assert.AreEqual(1, ReversalDistance.CalculateGreedy(start, target, out var reversals));
        Assert.IsTrue(expectedReversal.SequenceEqual(reversals));
    }

    [TestMethod]
    public void ParksFirstGiven()
    {
        var a = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var b = new[] { 3, 1, 5, 2, 7, 4, 9, 6, 10, 8 };

        var expectedReversal = new List<Tuple<int, int>>
        {
            new(7, 8),
            new(5, 6),
            new(1, 4),
            new(3, 7),
            new(5, 9),
            new(2, 8),
            new(3, 9),
            new(0, 8),
            new(0, 9)
        };
        Assert.AreEqual(9, ReversalDistance.CalculateGreedy(a, b, out var reversals));
        Assert.IsTrue(expectedReversal.SequenceEqual(reversals));
    }

    [TestMethod]
    public void ParksSecondGiven()
    {
        var a = new[] { 3, 10, 8, 2, 5, 4, 7, 1, 6, 9 };
        var b = new[] { 5, 2, 3, 1, 7, 4, 10, 8, 6, 9 };

        var expectedReversal = new List<Tuple<int, int>>
        {
            new(1, 7),
            new(4, 7),
            new(0, 5),
            new(0, 7)
        };

        Assert.AreEqual(4, ReversalDistance.CalculateGreedy(a, b, out var reversals));
        Assert.IsTrue(expectedReversal.SequenceEqual(reversals));
    }

    [TestMethod]
    public void ParksReversalThirdGiven()
    {
        var a = new[] { 8, 6, 7, 9, 4, 1, 3, 10, 2, 5 };
        var b = new[] { 8, 2, 7, 6, 9, 1, 5, 3, 10, 4 };

        var expectedReversal = new List<Tuple<int, int>>
        {
            new(6, 8),
            new(4, 6),
            new(3, 4),
            new(1, 3),
            new(6, 9)
        };

        Assert.AreEqual(5, ReversalDistance.CalculateGreedy(a, b, out var reversals));
        Assert.IsTrue(expectedReversal.SequenceEqual(reversals));
    }

    [TestMethod]
    public void ParksReversalFourthGiven()
    {
        var a = new[] { 3, 9, 10, 4, 1, 8, 6, 7, 5, 2 };
        var b = new[] { 2, 9, 8, 5, 1, 7, 3, 4, 6, 10 };

        var expectedReversal = new List<Tuple<int, int>>
        {
            new(5, 7),
            new(1, 3),
            new(3, 6),
            new(6, 8),
            new(2, 3),
            new(0, 3),
            new(0, 9)
        };

        Assert.AreEqual(7, ReversalDistance.CalculateGreedy(a, b, out var reversals));
        Assert.IsTrue(expectedReversal.SequenceEqual(reversals));
    }
}