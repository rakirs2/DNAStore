using Bio.Sequence.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class DnaSequenceListExtensionsTest
{
    [TestMethod]
    public void GenerateErrorCorrectionsReturnsNonNull()
    {
        var dnaList = new List<DNASequence>() { };
        Assert.IsNotNull(dnaList.GenerateErrorCorrections());
    }
    
    [TestMethod]
    public void ReturnsSingleErrorCorrectionForSimpleCase()
    {
        var dnaList = new List<DNASequence>()
        {
            new DNASequence("TCATC"),
            new DNASequence("TCATC"),
            new DNASequence("TCATG"),
        };

        var expectedErrorCorrections = new List<ErrorCorrection>()
        {
            new ErrorCorrection(new DNASequence("TCATG"), new DNASequence("TCATC")),
        };
        
        var errorCorrections = dnaList.GenerateErrorCorrections();
        Assert.AreEqual(1, errorCorrections.Count());
        Assert.IsTrue(Enumerable.SequenceEqual(expectedErrorCorrections, errorCorrections));
    }
    
    [TestMethod]
    public void ReturnsSingleErrorCorrectionForMultipleCases()
    {
        var dnaList = new List<DNASequence>()
        {
            new DNASequence("TCATC"),
            new DNASequence("TTCAT"),
            new DNASequence("TCATC"),
            new DNASequence("TGAAA"),
            new DNASequence("GAGGA"),
            new DNASequence("TTTCA"),
            new DNASequence("ATCAA"),
            new DNASequence("TTGAT"),
            new DNASequence("TTTCC"),
        };
        
        var expectedErrorCorrections = new List<ErrorCorrection>()
        {
            new ErrorCorrection(new DNASequence("TTCAT"), new DNASequence("TTGAT")),
            new ErrorCorrection(new DNASequence("GAGGA"), new DNASequence("GATGA")),
            new ErrorCorrection(new DNASequence("TTTCC"), new DNASequence("TTTCA")),
        };
        
        var errorCorrections = dnaList.GenerateErrorCorrections();
        Assert.AreEqual(3, errorCorrections.Count());
        Assert.IsTrue(Enumerable.SequenceEqual(expectedErrorCorrections, errorCorrections));
    }
}