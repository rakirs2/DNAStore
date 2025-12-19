using Bio.Sequences.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class ErrorCorrectionTest
{
    [TestMethod]
    public void VerifyToString()
    {
        var seq1 = new DnaSequence("ACGT");
        var seq2 = new DnaSequence("ACGG");
        var errorCorrection = new ErrorCorrection(seq1, seq2);
        Assert.AreEqual("ACGT->ACGG", errorCorrection.ToString());
    }

    [TestMethod]
    public void VerifyEquality()
    {
        var seq1 = new DnaSequence("ACGT");
        var seq2 = new DnaSequence("ACGC");
        var seq3 = new DnaSequence("ACGG");
        var obj1 = new object();
        var errorCorrection = new ErrorCorrection(seq1, seq2);
        var equalErrorCorrection = new ErrorCorrection(seq1, seq2);
        var unequalErrorCorrection = new ErrorCorrection(seq1, seq3);

        Assert.AreNotEqual(errorCorrection, obj1);
        Assert.AreEqual(equalErrorCorrection, errorCorrection);
        Assert.AreNotEqual(unequalErrorCorrection, errorCorrection);
    }
}