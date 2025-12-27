using DNAStore.Base.Algorithms;

namespace DNAStoreTests.Base.Algorithms;

[TestClass]
public class InsertionSorterTest
{
    [TestMethod]
    public void SimpleCounter()
    {
        List<int> values = new()
        {
            6, 10, 4, 5, 1, 2
        };

        Assert.AreEqual(12, InsertionSorter<int>.NumberOfSwapsInList(values));
    }
}