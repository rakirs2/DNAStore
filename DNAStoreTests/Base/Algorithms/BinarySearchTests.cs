using DNAStore.Base.Algorithms;

namespace DNAStoreTests.Base.Algorithms;

[TestClass]
public class BinarySearchTests
{
    private readonly List<int> _array = new() { 1, 2, 3, 4, 5 };

    [TestMethod]
    public void ContainsValueTrue()
    {
        Assert.IsTrue(BinarySearch.Contains(_array, 1));
    }

    [TestMethod]
    public void ContainsValueFalse()
    {
        Assert.IsFalse(BinarySearch.Contains(_array, 6));
    }

    [TestMethod]
    public void ContainsIndex()
    {
        Assert.AreEqual(0, BinarySearch.GetIndexAt(_array, 1));
        Assert.AreEqual(3, BinarySearch.GetIndexAt(_array, 4));
        Assert.AreEqual(-1, BinarySearch.GetIndexAt(_array, 6));
        Assert.AreEqual(-1, BinarySearch.GetIndexAt(_array, 0));
    }

    [TestMethod]
    public void GetIndicesTest()
    {
        Assert.IsTrue(
            new List<int> { 1, 2, 3 }.SequenceEqual(BinarySearch.GetIndices(_array, new List<int> { 2, 3, 4 })));
    }

    [TestMethod]
    public void BetterTest()
    {
        Assert.IsTrue(new List<int> { 4, 1, -1, -1, 4, 2 }.SequenceEqual(
            BinarySearch.GetIndices(new List<int> { 10, 20, 30, 40, 50 }, new List<int> { 40, 10, 35, 15, 40, 20 },
                true)));
    }
}