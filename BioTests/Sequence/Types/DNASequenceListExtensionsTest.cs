using Bio.Sequence.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class DnaSequenceListExtensionsTest
{
    [TestMethod]
    public void GenerateErrorCorrectionsReturnsNonNull()
    {
        var dnaList = new List<DNASequence>();
        Assert.IsNotNull(dnaList.GenerateErrorCorrections());
    }

    [TestMethod]
    public void ReturnsSingleErrorCorrectionForSimpleCase()
    {
        var dnaList = new List<DNASequence>
        {
            new("TCATC"),
            new("TCATC"),
            new("TCATG")
        };

        var expectedErrorCorrections = new List<ErrorCorrection>
        {
            new(new DNASequence("TCATG"), new DNASequence("TCATC"))
        };

        var errorCorrections = dnaList.GenerateErrorCorrections();
        Assert.AreEqual(1, errorCorrections.Count());
        Assert.IsTrue(expectedErrorCorrections.SequenceEqual(errorCorrections));
    }

    [TestMethod]
    public void ReturnsSingleErrorCorrectionForMultipleCases()
    {
        var dnaList = new List<DNASequence>
        {
            new("TCATC"),
            new("TTCAT"),
            new("TCATC"),
            new("TGAAA"),
            new("GAGGA"),
            new("TTTCA"),
            new("ATCAA"),
            new("TTGAT"),
            new("TTTCC")
        };

        var expectedErrorCorrections = new List<ErrorCorrection>
        {
            new(new DNASequence("TTCAT"), new DNASequence("TTGAT")),
            new(new DNASequence("GAGGA"), new DNASequence("GATGA")),
            new(new DNASequence("TTTCC"), new DNASequence("TTTCA"))
        };

        var errorCorrections = dnaList.GenerateErrorCorrections();
        Assert.AreEqual(3, errorCorrections.Count());
        Assert.IsTrue(expectedErrorCorrections.SequenceEqual(errorCorrections));
    }
}