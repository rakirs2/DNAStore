using DnaStore.Base.Utils;

namespace BaseTests.Base.Utils;

[TestClass]
public class IntComparerTest
{
    private readonly IntComparer _comparer = new();

    [TestMethod]
    public void Int()
    {
        Assert.IsGreaterThan(0, _comparer.Compare(1, 0));
        Assert.AreEqual(0, _comparer.Compare(0, 0));
        Assert.IsLessThan(0, _comparer.Compare(0, 1));
    }
}