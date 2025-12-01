using Base.Algorithms;

namespace BaseTests.Algorithms;

[TestClass]
public class SearchTest
{
    [TestMethod]
    public void ThreeSumKnownTests()
    {
        var arr1 = new List<int> { 2, -3, 4, 10, 5 };
        var arr2 = new List<int> { 8, -6, 4, -2, -8 };
        var arr3 = new List<int> { -5, 2, 3, 2, -4 };
        var arr4 = new List<int> { 2, 4, -5, 6, 8 };

        Assert.AreEqual(0, Search.ThreeSumNoSort(arr1, 0).Count);
        Assert.IsTrue(Search.ThreeSumNoSort(arr2, 0)
            .Any(innerList => innerList.SequenceEqual(new List<int> { 0, 1, 3 })));
        Assert.IsTrue(Search.ThreeSumNoSort(arr3, 0)
            .Any(innerList => innerList.SequenceEqual(new List<int> { 0, 1, 2 })));
        Assert.AreEqual(0, Search.ThreeSumNoSort(arr4, 0).Count);
    }
}