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
}