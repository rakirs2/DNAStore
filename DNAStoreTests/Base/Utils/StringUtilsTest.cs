using DNAStore.Base.Utils;

namespace DNAStoreTests.Base.Utils;

[TestClass]
public class StringUtilsTest
{
    [TestMethod]
    public void SwapIndexTest()
    {
        var output = StringUtils.SwapIndex("test", 0, 1);
        Assert.IsTrue(output.Equals("etst"));
    }

    [TestMethod]
    public void SwapIndexTestOutOfBoundsNegative()
    {
        Assert.ThrowsExactly<ArgumentException>(() => StringUtils.SwapIndex("test", -1, 1));
    }

    [TestMethod]
    public void SwapIndexTestOutOfBoundsGreaterThanLength()
    {
        Assert.ThrowsExactly<ArgumentException>(() => StringUtils.SwapIndex("test", 5, 1));
    }

    [TestMethod]
    public void SwapIndexTestOutOfBoundsNullString()
    {
        Assert.ThrowsExactly<ArgumentException>(() => StringUtils.SwapIndex(null, 5, 1));
    }

    [TestMethod]
    public void ForceJoinPerfectOrderOffByOne()
    {
        var strings = new List<string>
        {
            "abc",
            "bcd",
            "cde"
        };

        Assert.AreEqual("abcde", strings.ForceJoinPerfectOrder());
    }

    [TestMethod]
    public void LevenshteinDistanceSame()
    {
        Assert.AreEqual(0, StringUtils.LevenshteinDistance("test", "test"));
    }

    [TestMethod]
    public void LevenshteinDistanceEmptyFirst()
    {
        Assert.AreEqual(4, StringUtils.LevenshteinDistance("", "test"));
    }

    [TestMethod]
    public void LevenshteinDistanceEmptySecond()
    {
        Assert.AreEqual(4, StringUtils.LevenshteinDistance("test", ""));
    }

    [TestMethod]
    public void LevenshteinDistanceSimpleKnownExample()
    {
        Assert.AreEqual(3, StringUtils.LevenshteinDistance("kitten", "sitting"));
    }

    [TestMethod]
    public void GivenMethod()
    {
        Assert.AreEqual(5, StringUtils.LevenshteinDistance("PLEASANTLY", "MEANLY"));
    }

    [TestMethod]
    public void WhyGreedyDoesNotWork()
    {
        Assert.AreEqual(2, StringUtils.LevenshteinDistance("ABCDEF", "GABCDE"));
    }

    [TestMethod]
    public void EditDistanceGappedStrings()
    {
        var editdistance = StringUtils.NeedlemanWunsch("PRETTY", "PRTTEIN", out var a_gapped, out var b_gapped);
        Assert.AreEqual(4, editdistance);
        Assert.AreEqual("PRET-TY", a_gapped);
        Assert.AreEqual("PRTTEIN", b_gapped);
    }

    [TestMethod]
    public void AhoCorasickBasicTest()
    {
        var text = "ABCDEFGHIABCDEFGHI";
        var result = text.AhoCorasickStringSearch(new List<string> { "ABCDEF", "GHI" });
        Assert.AreEqual(4, result.Count);
    }

    [TestMethod]
    public void GenerateRandomString()
    {
        var valid = new List<char>() { 'a', 'b' };
        var count = 5;
        var output = StringUtils.GenerateRandomString(count, valid);
        Assert.IsFalse(output.Contains('c'));
        Assert.AreEqual(count, output.Length);
    }
}