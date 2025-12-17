using Bio.Sequence.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class DnaSequenceTests
{
    [TestMethod]
    public void TranscribeToRNATest()
    {
        var sequence = new DnaSequence("GATGGAACTTGACTACGTAAATT");
        var rnaSequence = sequence.TranscribeToRNA();
        Assert.AreEqual("GAUGGAACUUGACUACGUAAAUU", rnaSequence.ToString());
    }

    [TestMethod]
    public void ReverseComplementTest()
    {
        var sequence = new DnaSequence("AAAACCCGGT");
        var complement = sequence.GetReverseComplement();
        Assert.AreEqual("ACCGGGTTTT", complement.ToString());
    }

    [TestMethod]
    public void RestrictionSitesTest()
    {
        var sequence = new DnaSequence("TCAATGCATGCGGGTCTATATGCAT");
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
        var testDNASequence = new DnaSequence("ATGTAG");
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
            new DnaSequence(
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
        var seq1 = new DnaSequence("AGT");
        Assert.AreEqual(11, seq1.ToNumber());
    }

    [TestMethod]
    public void NumberToDNATest()
    {
        var seq1 = DnaSequence.FromNumber(11, 3);
        Assert.AreEqual(new DnaSequence("AGT"), seq1);
    }

    [TestMethod]
    public void NumberToDNABaseCase()
    {
        var seq1 = DnaSequence.FromNumber(0, 0);
        Assert.AreEqual(new DnaSequence(""), seq1);
    }

    [TestMethod]
    public void NumberToDNABaseCaseSimiliar()
    {
        var seq1 = DnaSequence.FromNumber(0, 1);
        Assert.AreEqual(new DnaSequence("A"), seq1);
    }

    [TestMethod]
    public void NumberToDNAPaddingTest()
    {
        var seq1 = DnaSequence.FromNumber(0, 2);
        Assert.AreEqual(new DnaSequence("AA"), seq1);
    }

    [TestMethod]
    public void NumberToDNAActualTest()
    {
        var seq1 = DnaSequence.FromNumber(7939, 7);
        Assert.AreEqual(new DnaSequence("CTTAAAT"), seq1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateKmerCompositionBadInput()
    {
        var seq = new DnaSequence("acgt");
        int[] _ = seq.KmerComposition(0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateKmerCompositionBadInput2()
    {
        var seq = new DnaSequence("acgt");
        int[] _ = seq.KmerComposition(-1);
    }

    [TestMethod]
    public void GenerateKmerComposition()
    {
        var seq = new DnaSequence("AAAAA");
        int[] output = seq.KmerComposition(2);
        var expected = new[] { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        Assert.IsTrue(expected.SequenceEqual(output));
    }

    [TestMethod]
    public void GenerateKmerCompositionLarge()
    {
        var seq = new DnaSequence(
            "CTTCGAAAGTTTGGGCCGAGTCTTACAGTCGGTCTTGAAGCAAAGTAACGAACTCCACGGCCCTGACTACCGAACCAGTTGTGAGTACTCAACTGGGTGAGAGTGCAGTCCCTATTGAGTTTCCGAGACTCACCGGGATTTTCGATCCAGCCTCAGTCCAGTCTTGTGGCCAACTCACCAAATGACGTTGGAATATCCCTGTCTAGCTCACGCAGTACTTAGTAAGAGGTCGCTGCAGCGGGGCAAGGAGATCGGAAAATGTGCTCTATATGCGACTAAAGCTCCTAACTTACACGTAGACTTGCCCGTGTTAAAAACTCGGCTCACATGCTGTCTGCGGCTGGCTGTATACAGTATCTACCTAATACCCTTCAGTTCGCCGCACAAAAGCTGGGAGTTACCGCGGAAATCACAG");
        int[] output = seq.KmerComposition(4);
        int[] expected = new[]
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

    [TestMethod]
    public void GenerateKmerCompositionUniqueSTrings()
    {
        var seq = new DnaSequence("CAATCCAAC");
        var output = seq.KmerCompositionUniqueString(5);
        var expected = new HashSet<string>
        {
            "AATCC",
            "ATCCA",
            "CAATC",
            "CCAAC",
            "TCCAA"
        };

        foreach (string val in expected) output.Remove(val);

        Assert.IsTrue(output.Count == 0);
    }

    [TestMethod]
    public void RandomStringTest()
    {
        var seq = new DnaSequence("ACGATACAA");
        double output = seq.RandomStringProbability(0.129);
        Assert.AreEqual(-5.737, double.Round(output, 3));
    }

    [TestMethod]
    public void DNeighborhood()
    {
        var seq = new DnaSequence("ACG");
        var output = seq.DNeighborhood(1);
        var expected = new HashSet<string>
        {
            "CCG",
            "TCG",
            "GCG",
            "AAG",
            "ATG",
            "AGG",
            "ACA",
            "ACC",
            "ACT",
            "ACG"
        };

        Assert.IsTrue(output.SetEquals(expected));
    }

    [TestMethod]
    public void TransitionToTransversion()
    {
        var seq1 = new DnaSequence("GCAACGCACAACGAAAACCCTTAGGGACTGGATTATTTCGTGATCGTTGTAGTTATTGGAAGTACGGGCATCAACCCAGTT");
        var seq2 = new DnaSequence("TTATCTGACAAAGAAAGCCGTCAACGGCTGGATAATTTCGCGATCGTGCTGGTTACTGGCGGTACGAGTGTTCCTTTGGGT");
        Assert.AreEqual(1.214, System.Math.Round(seq1.TransitionToTransversionRatio(seq2), 3));
    }

    [TestMethod]
    public void TransitionToTransversionDifferentLength()
    {
        var seq1 = new DnaSequence(
            "GCAACGCACAACGAAAACCCTTAGGGACTGGATTATTTCGTGATCGTTGTAGTTATTGGAAGTACGGGCATCAACCCAGTTA");
        var seq2 = new DnaSequence("TTATCTGACAAAGAAAGCCGTCAACGGCTGGATAATTTCGCGATCGTGCTGGTTACTGGCGGTACGAGTGTTCCTTTGGGT");
        Assert.ThrowsExactly<ArgumentException>(() => seq1.TransitionToTransversionRatio(seq2));
    }

    [TestMethod]
    public void ProbabilityOfAGivenString()
    {
        Assert.AreEqual(.689, System.Math.Round(DnaSequence.GetProbabilityOccuringGivenGCContent("ATAGCCGA", 90000, 0.6
        ), 3));
    }

    [TestMethod]
    public void OddsOfFinding()
    {
        var seq = new DnaSequence("AG");
        var gc = new double[] { 0.25, .5, .75 };
        double[] output = seq.OddsOfFinding(gc, 10);
        for (var i = 0; i < gc.Length; i++) output[i] = System.Math.Round(output[i], 3);

        Assert.AreEqual("" , string.Join(" ", output));
    }
}