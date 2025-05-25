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
    public void RestrictionSitesTest()
    {
        var sequence = new DNASequence("TCAATGCATGCGGGTCTATATGCAT");
        var actual = sequence.RestrictionSites();
        var expected = new List<Tuple<int, int>>()
        {
            new(4, 6),
            new(5, 4),
            new(6, 6),
            new(7, 4),
            new(17, 4),
            new(18, 4),
            new(20, 6),
            new(21, 4)
        };

        Assert.IsTrue(actual.SequenceEqual(expected));
    }
}