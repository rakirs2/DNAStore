using Base.Utils;

namespace BaseTests.Utils;

[TestClass]
public class StringUtilsTests
{
    [TestMethod]
    public void SwapIndexTest()
    {
        string? output = StringUtils.SwapIndex("test", 0, 1);
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
}