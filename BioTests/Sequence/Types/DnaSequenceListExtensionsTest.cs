using Bio.Sequence.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class DnaSequenceListExtensionsTest
{
    [TestMethod]
    public void GenerateErrorCorrectionsReturnsNonNull()
    {
        var dnaList = new List<DnaSequence>();
        Assert.IsNotNull(dnaList.GenerateErrorCorrections());
    }

    [TestMethod]
    public void ReturnsSingleErrorCorrectionForSimpleCase()
    {
        var dnaList = new List<DnaSequence>
        {
            new("TCATC"),
            new("TCATC"),
            new("TCATG")
        };

        var expectedErrorCorrections = new List<ErrorCorrection>
        {
            new(new DnaSequence("TCATG"), new DnaSequence("TCATC"))
        };

        var errorCorrections = dnaList.GenerateErrorCorrections();
        Assert.AreEqual(1, errorCorrections.Count());
        Assert.IsTrue(expectedErrorCorrections.SequenceEqual(errorCorrections));
    }

    [TestMethod]
    public void ReturnsSingleErrorCorrectionForMultipleCases()
    {
        var dnaList = new List<DnaSequence>
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
            new(new DnaSequence("TTCAT"), new DnaSequence("TTGAT")),
            new(new DnaSequence("GAGGA"), new DnaSequence("GATGA")),
            new(new DnaSequence("TTTCC"), new DnaSequence("TTTCA"))
        };

        var errorCorrections = dnaList.GenerateErrorCorrections();
        Assert.AreEqual(3, errorCorrections.Count());
        Assert.IsTrue(expectedErrorCorrections.SequenceEqual(errorCorrections));
    }

    [TestMethod]
    public void MotifEnumerationTest()
    {
        var dnaList = new List<DnaSequence>
        {
            new("ATTTGGC"),
            new("TGCCTTA"),
            new("CGGTATC"),
            new("GAAAATT")
        };

        var output = dnaList.MotifEnumeration(3, 1);
        var expected = new HashSet<string>
        {
            "ATA",
            "ATT",
            "GTT",
            "TTT"
        };

        Assert.IsTrue(output.SetEquals(expected));
    }

    [TestMethod]
    public void MedianString()
    {
        var dnaList = new List<DnaSequence>
        {
            new("AAATTGACGCAT"),
            new("GACGACCACGTT"),
            new("CGTCAGCGCCTG"),
            new("GCTGAGCACCGG"),
            new("AGTACGGGACAG")
        };

        var output = dnaList.MedianString(3);
        Assert.IsTrue(output.Contains("GAC"));
    }

    [TestMethod]
    public void MedianStringTestEdges()
    {
        var dnaList = new List<DnaSequence>
        {
            new("AAG"),
            new("AAT")
        };

        var output = dnaList.MedianString(3);
        Assert.IsTrue(output.Contains("AAG"));
    }

    [TestMethod]
    public void MedianStringKmerNotInSet()
    {
        var dnaList = new List<DnaSequence>
        {
            new("ATA"),
            new("ACA"),
            new("AGA"),
            new("AAT"),
            new("AAC")
        };

        var output = dnaList.MedianString(3);
        Assert.IsTrue(output.Contains("AAA"));
    }

    [TestMethod]
    public void MedianStringLengthKmer()
    {
        var dnaList = new List<DnaSequence>
        {
            new("ACGT"),
            new("ACGT"),
            new("ACGT")
        };

        var output = dnaList.MedianString(3);
        Assert.IsTrue(output.Contains("ACG"));
    }

    [TestMethod]
    public void GreedyMotifSearchGiven()
    {
        var inputs = new List<DnaSequence>
        {
            new("GGCGTTCAGGCA"),
            new("AAGAATCAGTCA"),
            new("CAAGGAGTTCGC"),
            new("CACGTCAATCAC"),
            new("CAATAATATTCG")
        };

        var actual = inputs.GreedyMotifSearch(3, 5);
        var expected = new List<string>
        {
            "CAG",
            "CAG",
            "CAA",
            "CAA",
            "CAA"
        };

        Assert.IsTrue(expected.SequenceEqual(actual));
    }
}