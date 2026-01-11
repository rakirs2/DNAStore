using DNAStore.Base.Algorithms;

namespace DNAStoreTests.Base.Algorithms;

[TestClass]
public class ReversalDistanceTest
{
    [TestMethod]
    public void ReversalDistanceInvalidDistances()
    {
        var a = new[] { 1, 2, 3 };
        var b = new[] { 0 };
        Assert.ThrowsExactly<ArgumentException>(() => ReversalDistance.Calculate(a, b));
    }

    [TestMethod]
    public void SimpleEquality()
    {
        var a = new[] { 1 };
        var b = new[] { 1 };
        Assert.AreEqual(0, ReversalDistance.Calculate(a, b));
    }

    [TestMethod]
    public void SingleReversal()
    {
        var a = new[] { 1, 2 };
        var b = new[] { 2, 1 };
        Assert.AreEqual(1, ReversalDistance.Calculate(a, b));
    }

    [TestMethod]
    public void Given()
    {
        var a = new[] { 1, 2, 3, 4, 5 };
        var b = new[] { 3, 1, 5, 2, 4 };
        Assert.AreEqual(4, ReversalDistance.Calculate(a, b));
    }

    [TestMethod]
    public void Size6()
    {
        var a = new[] { 1, 2, 3, 4, 6, 5 };
        var b = new[] { 6, 3, 1, 5, 2, 4 };
        Assert.AreEqual(4, ReversalDistance.Calculate(a, b));
    }

    [TestMethod]
    public void Size7()
    {
        var a = new[] { 1, 2, 7, 3, 4, 6, 5 };
        var b = new[] { 6, 3, 1, 5, 2, 4, 7 };
        Assert.AreEqual(4, ReversalDistance.Calculate(a, b));
    }

    [TestMethod]
    public void Size8()
    {
        var a = new[] { 6, 1, 7, 8, 2, 3, 4, 5 };
        var b = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        Assert.AreEqual(3, ReversalDistance.Calculate(a, b));
    }
    
    [TestMethod]
    public void ApproximateGreedyReversalTest()
    {
        Assert.AreEqual(7, ReversalDistance.ApproximateGreedyReversalSort(new[] { -3, 4, 1, 5, -2 }, out var list));
    }

    [TestMethod]
    public void ParksHandlesBaseCase()
    {
        var start = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var target = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        Assert.AreEqual(0, ReversalDistance.CalculateParksGreedyExact(start, target));
    }
    
    [TestMethod]
    public void ParksSimpleReversal()
    {
        var start = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var target = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 10,9 };
        
        Assert.AreEqual(1, ReversalDistance.CalculateParksGreedyExact(start, target));
    }

    [TestMethod]
    public void ParksFirstGiven()
    {
        var a = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var b = new[] { 3, 1, 5, 2, 7, 4, 9, 6, 10, 8 };
        Assert.AreEqual(9, ReversalDistance.CalculateParksGreedyExact(a, b));
    }
    
    [TestMethod]
    public void ParksSecondGiven()
    {
        var a = new[] { 3, 10, 8, 2, 5, 4, 7, 1, 6, 9 };
        var b = new[] { 5, 2, 3, 1, 7, 4, 10, 8, 6, 9 };
        Assert.AreEqual(4, ReversalDistance.CalculateParksGreedyExact(a, b));
    }
    
    [TestMethod]
    public void ParksReversalThirdGiven()
    {
        var a = new[] { 8, 6, 7, 9, 4, 1, 3, 10, 2, 5 };
        var b = new[] { 8, 2, 7, 6, 9, 1, 5, 3, 10, 4 };
        
        Assert.AreEqual(5, ReversalDistance.CalculateParksGreedyExact(a, b));
    }
    
    [TestMethod]
    public void ParksReversalFourthGiven()
    {
        var a = new[] { 3, 9, 10, 4, 1, 8, 6, 7, 5, 2 };
        var b = new[] { 2, 9, 8, 5, 1, 7, 3, 4, 6, 10 }; 
        Assert.AreEqual(7, ReversalDistance.CalculateParksGreedyExact(a, b));
    }
}