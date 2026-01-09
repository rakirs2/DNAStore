using DNAStore.Base.Algorithms;
using DNAStore.Base.Utils;

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
    [Ignore("Currently unable to run this locally due to memory.")]
    public void GivenTests()
    {
        var a = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var b = new[] { 3, 1, 5, 2, 7, 4, 9, 6, 10, 8 };
        Assert.AreEqual(9, ReversalDistance.Calculate(a, b));

        var c = new[] { 3, 10, 8, 2, 5, 4, 7, 1, 6, 9 };
        var d = new[] { 5, 2, 3, 1, 7, 4, 10, 8, 6, 9 };
        Assert.AreEqual(6, ReversalDistance.Calculate(c, d));

        var e = new[] { 8, 6, 7, 9, 4, 1, 3, 10, 2, 5 };
        var f = new[] { 8, 2, 7, 6, 9, 1, 5, 3, 10, 4 };
        Assert.AreEqual(5, ReversalDistance.Calculate(e, f));

        var g = new[] { 3, 9, 10, 4, 1, 8, 6, 7, 5, 2 };
        var h = new[] { 2, 9, 8, 5, 1, 7, 3, 4, 6, 10 };
        Assert.AreEqual(7, ReversalDistance.Calculate(g, h));

        var i = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var j = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Assert.AreEqual(0, ReversalDistance.Calculate(g, h));
    }

    [TestMethod]
    public void CountingUnsignedBreakpoints()
    {
        var values = new[] { 2 ,3 , 1 , 6 ,5 ,4 };
        Assert.AreEqual(4, ReversalDistance.CountSignedBreakpoints(values));
    }
    
    [TestMethod]
    [Ignore]
    public void CountingBreakpoints()
    {
        var values = new[] { 3, 4, 5, -12, -8, -7, -6, 1, 2, 10, 9, -11, 13, 14 };
        Assert.AreEqual(8, ReversalDistance.CountSignedBreakpoints(values));
    }

    [TestMethod]
    public void InPlaceReversal()
    {
        var values = new[] { 1, 2, 3, 4 };
        ReversalDistance.ReverseSubsequence(values, 0, 3);
        Assert.IsTrue(values.SequenceEqual(new[] { -4, -3, -2, -1 }));
    }

    [TestMethod]
    public void InPlaceReversalOddElement()
    {
        var values = new[] { -3, +4, +1, +5, -2 };
        ReversalDistance.ReverseSubsequence(values, 0, 2);
        Assert.IsTrue(values.SequenceEqual(new[] { -1, -4, 3, 5, -2 }));
    }

    [TestMethod]
    public void InPlaceReversalOdd()
    {
        var values = new[] { 1, 2, 3, 4 };
        ReversalDistance.ReverseSubsequence(values, 0, 3);
        Assert.IsTrue(values.SequenceEqual(new[] { -4, -3, -2, -1 }));
    }

    [TestMethod]
    public void InPlaceReversalNull()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => ReversalDistance.ReverseSubsequence(null, 0, 3));
    }

    [TestMethod]
    public void ApproximateGreedyReversalTest()
    {
        Assert.AreEqual(7, ReversalDistance.ApproximateGreedyReversalSort(new[] { -3, 4, 1, 5, -2 }, out var list));
    }

    [TestMethod]
    public void ConvertBetweenArrays()
    {
        var a = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var b = new[] { 1, 10, 3, 4, 5, 6, 7, 8, 9, 2 };
        
        Assert.AreEqual(b, ReversalDistance.ConvertWithGivenOrder(a,b), new IntArrayComparer());
    }
    
    [TestMethod]
    public void ConvertBetweenArrays2()
    {
        var a = new[] { 1,2,3,4,5,6,7,8,9,10 };
        var b = new[] { 10,9,8,7,6,5,4,3,2,1 };
        
        Assert.AreEqual(b, ReversalDistance.ConvertWithGivenOrder(a,b), new IntArrayComparer());
    }

    [TestMethod]
    public void BestReversals()
    {
        var a = new int[] { 2, 1 };
        var bestRevCount = ReversalDistance.ReversalsWithTheLeastBreakPoints(a, out var values);
        Assert.IsTrue(new List<int[]>{new []{1,2}}.SequenceEqual( values,  new IntArrayComparer() ));
        Assert.AreEqual(0, bestRevCount);
    }
    
    [TestMethod]
    public void BestReveralsShouldHaveNone()
    {
        var a = new int[] { 1,2};
        var revCount = ReversalDistance.ReversalsWithTheLeastBreakPoints(a, out var bestReversals);
        Assert.IsTrue(new List<int[]>{}.SequenceEqual( bestReversals,  new IntArrayComparer() ));
        Assert.AreEqual(0, revCount);
    }
    
    [TestMethod]
    public void BestReversalsFewBreakpoints()
    {
        // 5 breakpoints
        var a = new int[] { 3, 1,4,2 };
        var revCount = ReversalDistance.ReversalsWithTheLeastBreakPoints(a, out var bestReversals);
        Assert.IsTrue(new List<int[]>{new []{1,3,4,2}, new[]{3,4,1,2}, new[]{3,1,2,4}}.SequenceEqual( bestReversals,  new IntArrayComparer() ));
        Assert.AreEqual(3, revCount);
    }
}