using Bio.Sequences.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class NucleotideSequenceTests
{
    [TestMethod]
    public void GetMinSkew()
    {
        var dnaSequence =
            new DnaSequence(
                "CCTATCGGTGGATTAGCATGTCCCTGTACGTTTCGCCGCGAACTAGTTCACACGGCTTGATGGCAAATGGTTTTTCCGGCGACCGTAATCGTCCACCGAG");
        int[]? output = dnaSequence.CalculateMinPrefixGCSkew();
        Assert.IsTrue(new List<int> { 53, 97 }.SequenceEqual(output));
    }
}