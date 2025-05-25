using Bio.Math;

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

    [TestMethod()]
    public void ExpectedDominantOffspringTest()
    {
        double total = Probability.ExpectedDominantOffspring(1, 0, 0, 1, 0, 1, 2);
        Assert.AreEqual(3.5, total);
    }
}