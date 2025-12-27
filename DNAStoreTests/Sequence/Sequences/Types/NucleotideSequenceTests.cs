using DnaStore.Sequence.Types;

namespace BaseTests.Sequence.Sequences.Types;

[TestClass]
public class NucleotideSequenceTests
{
    [TestMethod]
    public void GetMinSkew()
    {
        var dnaSequence =
            new DnaSequence(
                "CCTATCGGTGGATTAGCATGTCCCTGTACGTTTCGCCGCGAACTAGTTCACACGGCTTGATGGCAAATGGTTTTTCCGGCGACCGTAATCGTCCACCGAG");
        var output = dnaSequence.CalculateMinPrefixGCSkew();
        Assert.IsTrue(new List<int> { 53, 97 }.SequenceEqual(output));
    }
}