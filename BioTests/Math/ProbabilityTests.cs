using Bio.Math;

namespace BioTests.Math;

[TestClass]
public class ProbabilityTests
{
    [TestMethod]
    public void PercentDominantTest()
    {
        var something = Probability.PercentDominant(2, 2, 2);
    }
}