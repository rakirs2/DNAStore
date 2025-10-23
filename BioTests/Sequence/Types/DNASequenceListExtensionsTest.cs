using Bio.Sequence.Types;
using JetBrains.Annotations;

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
        Assert.AreEqual(expectedErrorCorrections, errorCorrections);
    }
}