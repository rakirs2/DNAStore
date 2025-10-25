using Bio.Sequence.Types;

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

    [TestMethod]
    public void VerifyEquality()
    {
        var seq1 = new DNASequence("ACGT");
        var seq2 = new DNASequence("ACGC");
        var seq3 = new DNASequence("ACGG");
        object obj1 = new Object();
        var errorCorrection = new ErrorCorrection(seq1, seq2);
        var equalErrorCorrection = new ErrorCorrection(seq1, seq2);
        var unequalErrorCorrection = new ErrorCorrection(seq1, seq3);
        
        Assert.AreNotEqual(errorCorrection, obj1);
        Assert.AreEqual(equalErrorCorrection, errorCorrection);
        Assert.AreNotEqual(unequalErrorCorrection, errorCorrection);
    }
}