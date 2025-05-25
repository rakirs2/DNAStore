using Bio.Sequence.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class DNASequenceTests
{
    [TestMethod]
    public void TranscribeToRNATest()
    {
        var sequence = new DNASequence("GATGGAACTTGACTACGTAAATT");
        var rnaSequence = sequence.TranscribeToRNA();
        Assert.AreEqual("GAUGGAACUUGACUACGUAAAUU", rnaSequence.RawSequence);
    }

    [TestMethod]
    public void ReverseComplementTest()
    {
        var sequence = new DNASequence("AAAACCCGGT");
        var complement = sequence.ToReverseComplement();
        Assert.AreEqual("ACCGGGTTTT", complement.RawSequence);
    }

    [TestMethod()]
    [Ignore]
    public void RestrictionSitesTest()
    {
        var sequence = new DNASequence("AAAACCCGGT");
        sequence.CalculateRestrictionSites();
    }
}