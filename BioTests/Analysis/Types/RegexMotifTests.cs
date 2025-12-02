using System.Text.RegularExpressions;
using Bio.Analysis.Types;

namespace BioTests.Analysis.Types;

[TestClass]
public class RegexMotifTests
{
    [TestMethod]
    public void Construction()
    {
        var motif = new RegexMotif("aaaa", 4, "test");
        Assert.AreEqual("test", motif.Name);
        Assert.AreEqual("aaaa", motif.InputMotif);
        Assert.AreEqual(new Regex("aaaa").ToString(), motif.UnderlyingRegex.ToString());
    }

    [TestMethod]
    public void SimpleRegexConversionWithOperators()
    {
        var motif = new RegexMotif("[abc][abcd]efg");
        Assert.AreEqual(new Regex("[abc][abcd]efg").ToString(), motif.UnderlyingRegex.ToString());
    }

    [TestMethod]
    public void AddingParenthesis()
    {
        var motif = new RegexMotif("[abc]{abcd}efg");
        Assert.AreEqual(new Regex("[abc][^abcd]efg").ToString(), motif.UnderlyingRegex.ToString());
    }

    [TestMethod]
    public void BasicIsMatchTest()
    {
        var motif = new RegexMotif("[abc]{abcd}efg");
        Assert.IsTrue(motif.IsMatch("aeefg"));
        Assert.IsFalse(motif.IsMatch("aaefg"));
    }

    [TestMethod]
    public void VerifyDifferentLengthMatches()
    {
        var motif = new RegexMotif("[abc]{abcd}efg");
        Assert.IsTrue(motif.IsMatch("aeefgh"));
    }

    [TestMethod]
    public void VerifyDifferentLengthFailsWithStrictCheck()
    {
        var motif = new RegexMotif("[abc]{abcd}efg");
        Assert.IsFalse(motif.IsMatchStrict("aeefgh"));
    }
}