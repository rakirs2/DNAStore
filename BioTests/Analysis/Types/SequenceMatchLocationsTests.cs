using Bio.Analysis.Types;
using Bio.Sequence.Types;

namespace BioTests.Analysis.Types;

[TestClass]
public class SequenceMatchLocationsTests
{
    [TestMethod]
    public void MatchLocationsTest()
    {
        var seq = new AnySequence(
            "CGCCCGAATCCAGAACGCATTCCCATATTTCGGGACCACTGGCCTCCACGGTACGGACGTCAATCAAATGCCTAGCGGCTTGTGGTTTCTCCTACGCTCC");

        var matchLocations = new SequenceMatchLocations(seq, new RegexMotif("CGCCC", 5));
        var output = matchLocations.GetLocations();
        Assert.IsTrue(output.SequenceEqual(new List<int> { 0 }));
    }

    [TestMethod]
    public void HammingMatch()
    {
        var seq = new AnySequence(
            "CGCCCGAATCCAGAACGCATTCCCATATTTCGGGACCACTGGCCTCCACGGTACGGACGTCAATCAAATGCCTAGCGGCTTGTGGTTTCTCCTACGCTCC");

        var matchLocations = new SequenceMatchLocations(seq, new HammingMatch("ATTCTGGA", 3));
        var output = matchLocations.GetLocations();
        Assert.IsTrue(output.SequenceEqual(new List<int> { 6, 7, 26, 27, 78 }));
    }
}