using DNAStore.Base.Algorithms;

namespace BaseTests.Base.Algorithms;

[TestClass]
public class MajorityElementTest
{
    [TestMethod]
    public void SimpleBoyerMooreTest()
    {
        var array = new List<int> { 1, 2, 3, 4, 5 };
        Assert.AreEqual(0, MajorityElement<int>.BoyerMoore(array));
        Assert.AreEqual(0, MajorityElement<int>.SimpleDictionary(array));
    }

    [TestMethod]
    public void Given()
    {
        var array = new List<int> { 8, 7, 7, 7, 1, 7, 3, 7 };
        Assert.AreEqual(7, MajorityElement<int>.BoyerMoore(array));
        Assert.AreEqual(7, MajorityElement<int>.SimpleDictionary(array));
    }

    [TestMethod]
    public void NoMajority()
    {
        var array = new List<int> { 5, 1, 6, 7, 1, 1, 10, 1 };
        Assert.AreEqual(0, MajorityElement<int>.BoyerMoore(array));
        Assert.AreEqual(0, MajorityElement<int>.SimpleDictionary(array));
    }
}