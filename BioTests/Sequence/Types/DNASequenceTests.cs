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
        Assert.AreEqual("GAUGGAACUUGACUACGUAAAUU", rnaSequence.ToString());
    }

    [TestMethod]
    public void ReverseComplementTest()
    {
        var sequence = new DNASequence("AAAACCCGGT");
        var complement = sequence.GetReverseComplement();
        Assert.AreEqual("ACCGGGTTTT", complement.ToString());
    }

    [TestMethod]
    public void RestrictionSitesTest()
    {
        var sequence = new DNASequence("TCAATGCATGCGGGTCTATATGCAT");
        var actual = sequence.RestrictionSites();
        var expected = new List<Tuple<int, int>>
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

    [TestMethod]
    public void GetCandidateProteinSequencesTest()
    {
        var testDNASequence = new DNASequence("ATGTAG");
        var result = testDNASequence.GetCandidateProteinSequences();
        var expected = new List<ProteinSequence>
        {
            new("M")
        };

        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void GetCandidateProteinSequencesWithReverseComplementTest()
    {
        var testDNASequence =
            new DNASequence(
                "AGCCATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCCGAGTAGCATCTCAG");
        var result = testDNASequence.GetCandidateProteinSequences();
        var expected = new List<ProteinSequence>
        {
            new("M"),
            new("MGMTPRLGLESLLE"),
            new("MTPRLGLESLLE"),
            new("MLLGSFRLIPKETLIQVAGSSPCNLS")
        };

        // TODO: this is not the right way to test this
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void DnaToNumberTest()
    {
        var seq1 = new DNASequence("AGT");
        Assert.AreEqual(11, seq1.ToNumber());
    }

    [TestMethod]
    public void NumberToDNATest()
    {
        var seq1 = DNASequence.FromNumber(11, 3);
        Assert.AreEqual(new DNASequence("AGT"), seq1);
    }

    [TestMethod]
    public void NumberToDNABaseCase()
    {
        var seq1 = DNASequence.FromNumber(0, 0);
        Assert.AreEqual(new DNASequence(""), seq1);
    }

    [TestMethod]
    public void NumberToDNABaseCaseSimiliar()
    {
        var seq1 = DNASequence.FromNumber(0, 1);
        Assert.AreEqual(new DNASequence("A"), seq1);
    }

    [TestMethod]
    public void NumberToDNAPaddingTest()
    {
        var seq1 = DNASequence.FromNumber(0, 2);
        Assert.AreEqual(new DNASequence("AA"), seq1);
    }

    [TestMethod]
    public void NumberToDNAActualTest()
    {
        var seq1 = DNASequence.FromNumber(7939, 7);
        Assert.AreEqual(new DNASequence("CTTAAAT"), seq1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateKmerCompositionBadInput()
    {
        var seq = new DNASequence("acgt");
        var _ = seq.KmerComposition(0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateKmerCompositionBadInput2()
    {
        var seq = new DNASequence("acgt");
        var _ = seq.KmerComposition(-1);
    }

    [TestMethod]
    public void GenerateKmerComposition()
    {
        var seq = new DNASequence("AAAAA");
        var output = seq.KmerComposition(2);
        var expected = new[] { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        Assert.IsTrue(expected.SequenceEqual(output));
    }

    [TestMethod]
    public void GenerateKmerCompositionLarge()
    {
        var seq = new DNASequence(
            "CTTCGAAAGTTTGGGCCGAGTCTTACAGTCGGTCTTGAAGCAAAGTAACGAACTCCACGGCCCTGACTACCGAACCAGTTGTGAGTACTCAACTGGGTGAGAGTGCAGTCCCTATTGAGTTTCCGAGACTCACCGGGATTTTCGATCCAGCCTCAGTCCAGTCTTGTGGCCAACTCACCAAATGACGTTGGAATATCCCTGTCTAGCTCACGCAGTACTTAGTAAGAGGTCGCTGCAGCGGGGCAAGGAGATCGGAAAATGTGCTCTATATGCGACTAAAGCTCCTAACTTACACGTAGACTTGCCCGTGTTAAAAACTCGGCTCACATGCTGTCTGCGGCTGGCTGTATACAGTATCTACCTAATACCCTTCAGTTCGCCGCACAAAAGCTGGGAGTTACCGCGGAAATCACAG");
        var output = seq.KmerComposition(4);
        var expected = new[]
        {
            4, 1, 4, 3, 0, 1, 1, 5, 1, 3, 1, 2, 2, 1, 2, 0, 1, 1, 3, 1, 2, 1, 3, 1, 1, 1, 1, 2, 2, 5, 1, 3, 0, 2, 2, 1,
            1, 1, 1, 3, 1, 0, 0, 1, 5, 5, 1, 5, 0, 2, 0, 2, 1, 2, 1, 1, 1, 2, 0, 1, 0, 0, 1, 1, 3, 2, 1, 0, 3, 2, 3, 0,
            0, 2, 0, 8, 0, 0, 1, 0, 2, 1, 3, 0, 0, 0, 1, 4, 3, 2, 1, 1, 3, 1, 2, 1, 3, 1, 2, 1, 2, 1, 1, 1, 2, 3, 2, 1,
            1, 0, 1, 1, 3, 2, 1, 2, 6, 2, 1, 1, 1, 2, 3, 3, 3, 2, 3, 0, 3, 2, 1, 1, 0, 0, 1, 4, 3, 0, 1, 5, 0, 2, 0, 1,
            2, 1, 3, 0, 1, 2, 2, 1, 1, 0, 3, 0, 0, 4, 5, 0, 3, 0, 2, 1, 1, 3, 0, 3, 2, 2, 1, 1, 0, 2, 1, 0, 2, 2, 1, 2,
            0, 2, 2, 5, 2, 2, 1, 1, 2, 1, 2, 2, 2, 2, 1, 1, 3, 4, 0, 2, 1, 1, 0, 1, 2, 2, 1, 1, 1, 5, 2, 0, 3, 2, 1, 1,
            2, 2, 3, 0, 3, 0, 1, 3, 1, 2, 3, 0, 2, 1, 2, 2, 1, 2, 3, 0, 1, 2, 3, 1, 1, 3, 1, 0, 1, 1, 3, 0, 2, 1, 2, 2,
            0, 2, 1, 1
        };
        Assert.IsTrue(expected.SequenceEqual(output));
    }
}