using Base.Algorithms;

namespace BaseTests.Algorithms;

[TestClass]
public class ReversalDistanceTest
{

    [TestMethod]
    public void ReversalDistanceInvalidDistances()
    {
        var a = new int[] { 1,2,3};
        var b = new int[] { 0 };
        Assert.ThrowsExactly<ArgumentException>(()=>ReversalDistance.Calculate(a,b));
    }
    
    [TestMethod]
    public void SimpleEquality()
    {
        var a = new int[] { 1};
        var b = new int[] { 1 };
        Assert.AreEqual(0, ReversalDistance.Calculate(a,b));
    }
    
    [TestMethod]
    public void SingleReversal()
    {
        var a = new int[] { 1,2};
        var b = new int[] { 2,1};
        Assert.AreEqual(1, ReversalDistance.Calculate(a,b));
    }
    
    [TestMethod]
    public void Given()
    {
        var a = new int[] { 1, 2, 3 ,4 ,5};
        var b = new int[] { 3, 1, 5, 2, 4};
        Assert.AreEqual(4, ReversalDistance.Calculate(a,b));
    }
    [TestMethod]
    public void Size6()
    {
        var a = new int[] { 1, 2, 3 ,4,6 ,5};
        var b = new int[] { 6, 3, 1, 5, 2, 4};
        Assert.AreEqual(4, ReversalDistance.Calculate(a,b));
    }
    
    [TestMethod]
    public void Size7()
    {
        var a = new int[] { 1, 2,7, 3 ,4,6 ,5};
        var b = new int[] { 6, 3, 1, 5, 2, 4,7};
        Assert.AreEqual(4, ReversalDistance.Calculate(a,b));
    }
    
    [TestMethod]
    public void Size8()
    {
        var a = new int[] { 6,1, 7,8, 2, 3 ,4 ,5};
        var b = new int[] { 1,2,3,4,5,6,7,8};
        Assert.AreEqual(3, ReversalDistance.Calculate(a,b));
    }
   
    [TestMethod]
    [Ignore("Currently unable to run this locally due to memory.")]
    public void GivenTests()
    {
        var a = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var b = new int[] { 3, 1, 5, 2, 7, 4, 9, 6, 10, 8 };
        Assert.AreEqual(9, ReversalDistance.Calculate(a,b));

        var c = new int[] { 3, 10, 8, 2, 5, 4, 7, 1, 6, 9 };
        var d = new int[] { 5, 2, 3, 1, 7, 4, 10, 8, 6, 9 };
        Assert.AreEqual(6, ReversalDistance.Calculate(c,d));

        var e = new int[] { 8, 6, 7, 9, 4, 1, 3, 10, 2, 5 };
        var f = new int[] { 8, 2, 7, 6, 9, 1, 5, 3, 10, 4 };
        Assert.AreEqual(5, ReversalDistance.Calculate(e,f));

        var g = new int[] { 3, 9, 10, 4, 1, 8, 6, 7, 5, 2 };
        var h = new int[] { 2, 9, 8, 5, 1, 7, 3, 4, 6, 10 };
        Assert.AreEqual(7, ReversalDistance.Calculate(g,h));

        var i = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var j = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Assert.AreEqual(0, ReversalDistance.Calculate(g,h));
    }

    [TestMethod]
    public void CountingBreakpoints()
    {
        var values = new int[] { 3, 4, 5, -12, -8, -7, -6, 1, 2, 10, 9, -11, 13, 14 };
        Assert.AreEqual(8, ReversalDistance.CountSignedBreakpoints(values));
    }

    [TestMethod]
    public void InPlaceReversal()
    {
        var values = new int []{1, 2, 3,4};
        ReversalDistance.ReverseSubsequence(values, 0, 3);
        Assert.IsTrue(values.SequenceEqual(new int[]{-4,-3,-2,-1}));
    }
    
    [TestMethod]
    public void InPlaceReversalOddElement()
    {
        var values = new int []{-3, +4, +1, +5, -2};
        ReversalDistance.ReverseSubsequence(values, 0, 2);
        Assert.IsTrue(values.SequenceEqual(new int[]{-1,-4,3,5, -2}));
    }
    
    [TestMethod]
    public void InPlaceReversalOdd()
    {
        var values = new int []{1, 2, 3,4};
        ReversalDistance.ReverseSubsequence(values, 0, 3);
        Assert.IsTrue(values.SequenceEqual(new int[]{-4,-3,-2,-1}));
    }
    
    [TestMethod]
    public void InPlaceReversalNull()
    {
        Assert.ThrowsExactly<ArgumentNullException>(()=>ReversalDistance.ReverseSubsequence(null, 0, 3));
    }

    [TestMethod]
    public void ApproximateGreedyReversalTest()
    {
        Assert.AreEqual(7, ReversalDistance.ApproximateGreedyReversalSort(new []{-3,4,1,5,-2}, out var list));
    }
}