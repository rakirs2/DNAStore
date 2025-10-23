using Bio.Sequence.Types;
using JetBrains.Annotations;

namespace BioTests.Sequence.Types;

[TestClass]
public class ErrorCorrectionTest
{
    [TestMethod]
    public void VerifyToString()
    {
        var seq1 = new DNASequence("ACGT");
        var seq2 = new DNASequence("ACGG");
        var errorCorrection = new ErrorCorrection(seq1, seq2);
        Assert.AreEqual("ACGT->ACGG", errorCorrection.ToString());
    }
}