
using Base.DataStructures;

namespace BaseTests.DataStructures;

[TestClass]
public class AlignmentMatrixTest
{
    [TestMethod]
    public void Existence()
    {
        Assert.IsNotNull(new AlignmentMatrix("a", "a"));
    }

    [TestMethod]
    public void ToString()
    {
        var am = new AlignmentMatrix("a", "a");
        Assert.AreEqual("(none, 0), (none, 0)\n(none, 0), (none, 0)", am.ToString());
    }

    [TestMethod]
    public void Alignment()
    {
        var am = new AlignmentMatrix("AACCTTGG", "ACACTGTGA");
        Assert.AreEqual("AACTTG", am.LongestCommonSubSequence());
    }
}