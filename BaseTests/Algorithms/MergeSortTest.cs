using Base.Algorithms;
using JetBrains.Annotations;

namespace BaseTests.Algorithms;

[TestClass]
public class MergeSortTest
{
    [TestMethod]
    public void Merge2SortedArrays()
    {
        var a = new int[] { 2, 4, 10, 18};
        var b = new int[] { -5, 11, 12};
        Assert.IsTrue( Enumerable.SequenceEqual(new int[]{-5, 2, 4, 10, 11, 12, 18}, MergeSort<int>.Merge2SortedArrays(a, b)));
    }
}