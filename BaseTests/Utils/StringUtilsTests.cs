namespace Base.Utils.Tests;

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
    [ExpectedException(typeof(ArgumentException))]
    public void SwapIndexTestOutOfBoundsNegative()
    {
        string? output = StringUtils.SwapIndex("test", -1, 1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SwapIndexTestOutOfBoundsGreaterThanLength()
    {
        string? output = StringUtils.SwapIndex("test", 5, 1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SwapIndexTestOutOfBoundsNullString()
    {
        string? output = StringUtils.SwapIndex(null, 5, 1);
    }

    [TestMethod]
    public void ForceJoinPerfectOrderOffByOne()
    {
        var strings = new List<string>()
        {
            "abc",
            "bcd",
            "cde"
        };
        
        Assert.AreEqual("abcde", strings.ForceJoinPerfectOrder());
    }
}