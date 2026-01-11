using DNAStore.Base.Algorithms;
using DNAStore.Base.Utils;

namespace DNAStoreTests.Base.Algorithms;

[TestClass]
public class SyntenyHelperTest
{
    [TestMethod]
    public void UnsignedBreakPointsBetweenTargetNoDifferences()
    {
        var sequence = new[] { 1, 2, 3, 4 };
        var target = new[] { 1, 2, 3, 4 };

        Assert.AreEqual(0, SyntenyHelper.FindUnsignedBreakPointsWithTarget(sequence, target));
    }

    [TestMethod]
    public void UnsignedBreakPointsBetweenTargetNoDifferencesOne()
    {
        var sequence = new[] { 1, 2, 3, 4 };
        var target = new[] { 1, 2, 4, 3 };

        Assert.AreEqual(1, SyntenyHelper.FindUnsignedBreakPointsWithTarget(sequence, target));
    }

    [TestMethod]
    public void UnsignedBreakPointsBetweenTargetNoDifferencesScrambled()
    {
        var sequence = new[] { 1, 2, 3, 4 };
        var target = new[] { 2, 4, 1, 3 };

        Assert.AreEqual(3, SyntenyHelper.FindUnsignedBreakPointsWithTarget(sequence, target));
    }

    [TestMethod]
    public void InPlaceReversal()
    {
        var values = new[] { 1, 2, 3, 4 };
        SyntenyHelper.ReverseSubsequence(values, 0, 3);
        Assert.IsTrue(values.SequenceEqual(new[] { -4, -3, -2, -1 }));
    }

    [TestMethod]
    public void InPlaceReversalOddElement()
    {
        var values = new[] { -3, +4, +1, +5, -2 };
        SyntenyHelper.ReverseSubsequence(values, 0, 2);
        Assert.IsTrue(values.SequenceEqual(new[] { -1, -4, 3, 5, -2 }));
    }

    [TestMethod]
    public void InPlaceReversalOdd()
    {
        var values = new[] { 1, 2, 3, 4 };
        SyntenyHelper.ReverseSubsequence(values, 0, 3);
        Assert.IsTrue(values.SequenceEqual(new[] { -4, -3, -2, -1 }));
    }

    [TestMethod]
    public void Unsigned()
    {
        var values = new[] { 1, 2, 3, 4 };
        SyntenyHelper.ReverseSubsequence(values, 0, 3, false);
        Assert.IsTrue(values.SequenceEqual(new[] { 4, 3, 2, 1 }));
    }

    [TestMethod]
    public void InPlaceReversalNull()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => SyntenyHelper.ReverseSubsequence(null, 0, 3));
    }

    [TestMethod]
    public void CountingBreakpoints()
    {
        var values = new[] { 3, 4, 5, -12, -8, -7, -6, 1, 2, 10, 9, -11, 13, 14 };
        Assert.AreEqual(8, SyntenyHelper.CountSignedBreakpoints(values));
    }

    [TestMethod]
    public void ReversalsIterator()
    {
        var values = new[] { 1, 2, 3 };
        var expected = new List<int[]>
        {
            new[] { 2, 1, 3 },
            new[] { 3, 2, 1 },
            new[] { 1, 3, 2 }
        };

        var reversals = SyntenyHelper.AllPossibleReversals(values);
        var allPossible = new List<int[]>();
        foreach (var reversal in reversals) allPossible.Add(reversal.Item1);
        Assert.AreEqual(3, allPossible.Count);
        Assert.IsTrue(allPossible.SequenceEqual(expected, IntArrayComparer.Shared));
    }

    [TestMethod]
    public void MinBreakpointGenerator()
    {
        // 3 bp in current
        var current = new[] { 3, 1, 4, 2 };
        var expected = new List<int[]>
        {
            new[] { 3, 4, 1, 2 }
        };

        var actual = SyntenyHelper.MinimumalBreakPointReversals(current, out var candidates);
        var allPossible = new List<int[]>();
        foreach (var candidate in candidates) allPossible.Add(candidate.Item1);
        Assert.IsTrue(expected.SequenceEqual(allPossible, IntArrayComparer.Shared));
        Assert.AreEqual(1, actual);
    }
}