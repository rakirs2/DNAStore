using Bio.Math;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;

namespace BioTests.Math;

[TestClass]
public class ProbabilityTests
{
    [TestMethod]
    public void PercentDominantTest()
    {
        Assert.IsTrue(Helpers.DoublesEqualWithinRange(0.783333, Probability.PercentDominant(2, 2, 2)));
    }

    [TestMethod]
    public void GetPermutationsOfASet()
    {
        var allPerms = Probability.GetPermutations(Enumerable.Range(1, 3), 3);
        Assert.AreEqual(6, allPerms.Count());
    }
}