using Bio.Analysis.Types;

namespace BioTests.Analysis.Types;

[TestClass]
public class HammingMatchTests
{
    [TestMethod]
    public void HammingMatchTest()
    {
        var hm = new HammingMatch("abcde", 1);
        Assert.AreEqual("abcde", hm.MatchString);
        Assert.AreEqual(5, hm.ExpectedLength);
        Assert.AreEqual(1, hm.Tolerance);
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void IsMatchTest()
    {
        var hm = new HammingMatch("abcde", 1);
        hm.IsMatch("abcde");
    }

    [TestMethod]
    public void IsMatchStrictTest()
    {
        var hm = new HammingMatch("abcde", 1);
        Assert.IsTrue(hm.IsMatchStrict("abcde"));
    }

    [TestMethod]
    public void IsMatchStrictTestOffBy1()
    {
        var hm = new HammingMatch("abcde", 1);
        Assert.IsTrue(hm.IsMatchStrict("abcdf"));
    }

    [TestMethod]
    public void IsMatchStrictTestOffByMoreThanTolerance()
    {
        var hm = new HammingMatch("abcff", 1);
        Assert.IsFalse(hm.IsMatchStrict("abcde"));
    }
}