using Bio.Math;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;

namespace BioTests.Math
{
    [TestClass]
    public class ProbabilityTests
    {
        [TestMethod]
        public void PercentDominantTest()
        {
            var something = Probability.PercentDominant(2, 2, 2);
        }
    }
}