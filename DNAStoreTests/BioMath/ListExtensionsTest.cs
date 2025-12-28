using DNAStore.BioMath;

namespace DNAStoreTests.BioMath;

[TestClass]
public class ListExtensionsTest
{
    [TestMethod]
    public void LongestIncreasingSubsequenceTest()
    {
        var list = new List<int> { 5, 1, 4, 2, 3 };
        var expected = new List<int> { 1, 2, 3 };
        var result = list.LongestIncreasingSubsequence();
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void LongestDecreasingSubsequenceTest()
    {
        var list = new List<int> { 5, 1, 4, 2, 3 };
        var expected = new List<int> { 5, 4, 2 };
        var result = list.LongestDecreasingSubsequence();
        Assert.IsTrue(expected.SequenceEqual(result));
    }
}