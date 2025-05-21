namespace Base.Utils.Tests;

[TestClass()]
public class StringUtilsTests
{
    [TestMethod()]
    public void SwapIndexTest()
    {
        var output = StringUtils.SwapIndex("test", 0, 1);
        Assert.IsTrue(output.Equals("etst"));
    }

    [TestMethod()]
    [ExpectedException(typeof(InvalidDataException))]
    public void SwapIndexTestOutOfBoundsNegative()
    {
        var output = StringUtils.SwapIndex("test", -1, 1);
    }

    [TestMethod()]
    [ExpectedException(typeof(InvalidDataException))]
    public void SwapIndexTestOutOfBoundsGreaterThanLength()
    {
        var output = StringUtils.SwapIndex("test", 5, 1);
    }

    [TestMethod()]
    [ExpectedException(typeof(InvalidDataException))]
    public void SwapIndexTestOutOfBoundsNullString()
    {
        var output = StringUtils.SwapIndex(null, 5, 1);
    }
}