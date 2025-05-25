using Bio.Sequence.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class NucleotideSequenceTests
{
    [TestMethod]
    public void GetMinSkew()
    {
        var dnaSequence =
            new DNASequence(
                "CCTATCGGTGGATTAGCATGTCCCTGTACGTTTCGCCGCGAACTAGTTCACACGGCTTGATGGCAAATGGTTTTTCCGGCGACCGTAATCGTCCACCGAG");
        var output = dnaSequence.CalculateMinPrefixGCSkew();
        Assert.IsTrue(new List<int>() { 53, 97 }.SequenceEqual(output));
    }
}