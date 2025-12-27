using DnaStore.Sequence.Analysis.Types;

namespace BaseTests.Sequence.Analysis.Types;

[TestClass]
public class SequenceMatchLocationsTests
{
    [TestMethod]
    public void MatchLocationsTest()
    {
        var seq = new DnaStore.Sequence.Types.Sequence(
            "CGCCCGAATCCAGAACGCATTCCCATATTTCGGGACCACTGGCCTCCACGGTACGGACGTCAATCAAATGCCTAGCGGCTTGTGGTTTCTCCTACGCTCC");

        var matchLocations = new SequenceMatchLocations(seq, new Motif("CGCCC", 5));
        var output = matchLocations.GetLocations();
        Assert.IsTrue(output.SequenceEqual(new List<int> { 0 }));
    }

    [TestMethod]
    public void HammingMatch()
    {
        var seq = new DnaStore.Sequence.Types.Sequence(
            "CGCCCGAATCCAGAACGCATTCCCATATTTCGGGACCACTGGCCTCCACGGTACGGACGTCAATCAAATGCCTAGCGGCTTGTGGTTTCTCCTACGCTCC");

        var matchLocations = new SequenceMatchLocations(seq, new HammingMatch("ATTCTGGA", 3));
        var output = matchLocations.GetLocations();
        Assert.IsTrue(output.SequenceEqual(new List<int> { 6, 7, 26, 27, 78 }));
    }
}