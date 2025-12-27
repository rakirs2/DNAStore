using DnaStore.Base.Algorithms;

namespace BaseTests.Base.Algorithms;

[TestClass]
public class SearchTest
{
    [TestMethod]
    public void KMPFailureArrayCLRSExample()
    {
        var expected = new[] { 0, 0, 1, 2, 3, 0, 1 };
        var sequence = "ababaca";
        Assert.IsTrue(sequence.KMPFailureArray().SequenceEqual(expected));
    }

    [TestMethod]
    public void KMPFailureArrayDnaVersion()
    {
        var expected = new[] { 0, 0, 0, 1, 2, 0, 0, 0, 0, 0, 0, 1, 2, 1, 2, 3, 4, 5, 3, 0, 0 };
        var sequence = "CAGCATGGTATCACAGCAGAG";
        Assert.IsTrue(sequence.KMPFailureArray().SequenceEqual(expected));
    }

    [TestMethod]
    public void SimpleSearch()
    {
        var expected = new[] { 0, 13, 16 };
        var sequence = "CAGCATGGTATCACAGCAGAG";
        var pattern = "CAG";
        Assert.IsTrue(sequence.KnuthMorrisPratt(pattern).SequenceEqual(expected));
    }
}