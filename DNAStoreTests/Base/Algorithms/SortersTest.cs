using Base.Algorithms;

namespace BaseTests.Algorithms;

[TestClass]
public class SortersTest
{
    [TestMethod]
    public void Merge2SortedArrays()
    {
        var a = new[] { 2, 4, 10, 18 };
        int[]? b = new[] { -5, 11, 12 };
        Assert.IsTrue(new[] { -5, 2, 4, 10, 11, 12, 18 }.SequenceEqual(Sorters<int>.Merge2SortedArrays(a, b)));
    }

    [TestMethod]
    public void MergeSortTest()
    {
        int[]? a = new[] { 2, -5, 11, 4, 12, 10, 18 };
        Sorters<int>.InPlaceMergeSort(ref a);
        Assert.IsTrue(new[] { -5, 2, 4, 10, 11, 12, 18 }.SequenceEqual(a));
    }

    [TestMethod]
    public void CountInversions()
    {
        int[]? a = new[] { -6, 1, 15, 8, 10 };
        long inversions = Sorters<int>.InPlaceMergeSort(ref a);
        Assert.AreEqual(2, inversions);
    }
}