using Base.Algorithms;

namespace BaseTests.Algorithms;

[TestClass]
public class MergeSortTest
{
    [TestMethod]
    public void Merge2SortedArrays()
    {
        var a = new[] { 2, 4, 10, 18 };
        var b = new[] { -5, 11, 12 };
        Assert.IsTrue(new[] { -5, 2, 4, 10, 11, 12, 18 }.SequenceEqual(MergeSort<int>.Merge2SortedArrays(a, b)));
    }
}