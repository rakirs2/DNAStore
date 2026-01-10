using DNAStore.Base.Utils;

namespace DNAStoreTests.Base.Utils;

[TestClass]
public class IntegerArrayUtilsTest
{
    [TestMethod]
    public void UnsignedBreakPointsBetweenTargetNoDifferences()
    {
        var sequence = new[] { 1, 2, 3, 4 };
        var target = new[] { 1, 2, 3, 4 };
        
        Assert.AreEqual(0, IntegerArrayUtils.FindUnsignedBreakPoints(sequence, target));
    }
    
    [TestMethod]
    public void UnsignedBreakPointsBetweenTargetNoDifferencesOne()
    {
        var sequence = new[] { 1, 2, 3, 4 };
        var target = new[] { 1, 2, 4,3 };
        
        Assert.AreEqual(1, IntegerArrayUtils.FindUnsignedBreakPoints(sequence, target));
    }
    
    [TestMethod]
    public void UnsignedBreakPointsBetweenTargetNoDifferencesScrambled()
    {
        var sequence = new[] { 1, 2, 3, 4 };
        var target = new[] { 2,4, 1,3 };
         
        Assert.AreEqual(3, IntegerArrayUtils.FindUnsignedBreakPoints(sequence, target));
    }
    
    [TestMethod]
    public void InPlaceReversal()
    {
        var values = new[] { 1, 2, 3, 4 };
        IntegerArrayUtils.ReverseSubsequence(values, 0, 3);
        Assert.IsTrue(values.SequenceEqual(new[] { -4, -3, -2, -1 }));
    }

    [TestMethod]
    public void InPlaceReversalOddElement()
    {
        var values = new[] { -3, +4, +1, +5, -2 };
        IntegerArrayUtils.ReverseSubsequence(values, 0, 2);
        Assert.IsTrue(values.SequenceEqual(new[] { -1, -4, 3, 5, -2 }));
    }

    [TestMethod]
    public void InPlaceReversalOdd()
    {
        var values = new[] { 1, 2, 3, 4 };
        IntegerArrayUtils.ReverseSubsequence(values, 0, 3);
        Assert.IsTrue(values.SequenceEqual(new[] { -4, -3, -2, -1 }));
    }
    
    [TestMethod]
    public void Unsigned()
    {
        var values = new[] { 1, 2, 3, 4 };
        IntegerArrayUtils.ReverseSubsequence(values, 0, 3, signed: false);
        Assert.IsTrue(values.SequenceEqual(new[] { 4, 3, 2, 1 }));
    }

    [TestMethod]
    public void InPlaceReversalNull()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => IntegerArrayUtils.ReverseSubsequence(null, 0, 3));
    }
    
    [TestMethod]
    public void CountingBreakpoints()
    {
        var values = new[] { 3, 4, 5, -12, -8, -7, -6, 1, 2, 10, 9, -11, 13, 14 };
        Assert.AreEqual(8, IntegerArrayUtils.CountSignedBreakpoints(values));
    }
}