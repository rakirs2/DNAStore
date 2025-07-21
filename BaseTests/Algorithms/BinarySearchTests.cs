namespace Base.Algorithms.Tests;

[TestClass()]
public class BinarySearchTests
{
    private List<int> _array = new() { 1, 2, 3, 4, 5 };
    [TestMethod()]
    public void ContainsValueTrue()
    {
        Assert.IsTrue(BinarySearch.Contains(_array, 1));
    }

    [TestMethod()]
    public void ContainsValueFalse()
    {
        Assert.IsFalse(BinarySearch.Contains(_array, 6));
    }

    [TestMethod()]
    public void ContainsIndex()
    {
        Assert.AreEqual(0, BinarySearch.GetIndexAt(_array, 1));
        Assert.AreEqual(3, BinarySearch.GetIndexAt(_array, 4));
        Assert.AreEqual(-1, BinarySearch.GetIndexAt(_array, 6));
        Assert.AreEqual(-1, BinarySearch.GetIndexAt(_array, 0));
    }

    [TestMethod()]
    public void GetIndicesTest()
    {
        Assert.IsTrue(Enumerable.SequenceEqual(new List<int> { 1, 2, 3 }, BinarySearch.GetIndices(_array, new List<int>() { 2, 3, 4 })));
    }
}