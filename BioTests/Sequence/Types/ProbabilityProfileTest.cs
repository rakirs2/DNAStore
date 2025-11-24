using Bio.Sequence.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class ProbabilityProfileTest
{
    [TestMethod]
    public void GivenTest()
    {
        var profiles = new List<List<Double>>
        {
            new List<double>() { 0.2, 0.2, 0.3, 0.2, 0.3 },
            new List<double>() { 0.4, 0.3, 0.1, 0.5, 0.1 },
            new List<double>() { 0.3, 0.3, 0.5, 0.2, 0.4 },
            new List<double>() { 0.1, 0.2, 0.1, 0.1, 0.2 }
        };

        var probProfile = new ProbabilityProfile(profiles, "ACGT");
        var seq = new DnaSequence("ACCTGTTTATTGCCTAAGTTCCGAACAAACCCAATATAGCCCGAGGGCCT");
        Assert.AreEqual("CCGAG", probProfile.HighestLikelihood(seq));
    }
}