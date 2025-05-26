using Bio.Analysis.Types;
using Bio.Sequence.Types;

namespace BioTests.Analysis.Types;

[TestClass()]
public class MatchLocationsTests
{
    [TestMethod()]
    public void MatchLocationsTest()
    {
        var seq = new AnySequence(
            "CGCCCGAATCCAGAACGCATTCCCATATTTCGGGACCACTGGCCTCCACGGTACGGACGTCAATCAAATGCCTAGCGGCTTGTGGTTTCTCCTACGCTCC");

        var matchLocations = new MatchLocations(seq, new Motif("CGCCC", 5));
        var output = matchLocations.GetLocations();
        Assert.IsTrue(output.SequenceEqual(new List<int> { 0 }));
    }
}