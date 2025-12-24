using Bio.Sequences;

namespace BioTests.Sequence;

[TestClass]
public class SequenceHelpersTests
{
    private static readonly HashSet<char> KnownProteinSequenceDifferentiators =
        new() { 'E', 'F', 'I', 'L', 'P', 'Q', 'Z', 'X', '*' };

    [TestMethod]
    public void IsRNADifferentiatorFalse()
    {
        Assert.IsFalse(SequenceHelpers.IsKnownRNADifferentiator('a'));
    }

    [TestMethod]
    public void IsRNADifferentiatorTrue()
    {
        Assert.IsTrue(SequenceHelpers.IsKnownRNADifferentiator('U'));
    }

    [TestMethod]
    public void IsRNADifferentiatorCaseSensitive()
    {
        Assert.IsTrue(SequenceHelpers.IsKnownRNADifferentiator('u'));
    }

    [TestMethod]
    public void IsProteinSequenceDifferentiator()
    {
        foreach (char c in KnownProteinSequenceDifferentiators)
            Assert.IsTrue(SequenceHelpers.IsKnownProteinDifferentiator(c));
    }

    [TestMethod]
    public void IsProteinSequenceDifferentiatorCaseSensitive()
    {
        foreach (char c in KnownProteinSequenceDifferentiators)
            Assert.IsTrue(SequenceHelpers.IsKnownProteinDifferentiator(char.ToLowerInvariant(c)));
    }

    [TestMethod]
    public void IsProteinSequenceDifferentiatorAmbiguousCharactersReturnsFalse()
    {
        Assert.IsFalse(SequenceHelpers.IsKnownProteinDifferentiator('u'));
    }

    [TestMethod]
    public void InvalidCodonLength()
    {
        Assert.ThrowsExactly<InvalidDataException>(() => SequenceHelpers.RNAToProteinConverter("A"));
    }

    [TestMethod]
    public void BadCodonLowercase()
    {
        Assert.ThrowsExactly<InvalidDataException>(() => SequenceHelpers.RNAToProteinConverter("aaa"));
    }

    [TestMethod]
    public void BadCodonEmptyString()
    {
        Assert.ThrowsExactly<InvalidDataException>(() => SequenceHelpers.RNAToProteinConverter(""));
    }

    [TestMethod]
    public void ValidCodes()
    {
        Assert.AreEqual("F", SequenceHelpers.RNAToProteinConverter("UUU"));
        Assert.AreEqual("L", SequenceHelpers.RNAToProteinConverter("CUU"));
        Assert.AreEqual("I", SequenceHelpers.RNAToProteinConverter("AUU"));
        Assert.AreEqual("V", SequenceHelpers.RNAToProteinConverter("GUU"));
        Assert.AreEqual("F", SequenceHelpers.RNAToProteinConverter("UUC"));
        Assert.AreEqual("L", SequenceHelpers.RNAToProteinConverter("CUC"));
        Assert.AreEqual("I", SequenceHelpers.RNAToProteinConverter("AUC"));
        Assert.AreEqual("V", SequenceHelpers.RNAToProteinConverter("GUC"));
        Assert.AreEqual("L", SequenceHelpers.RNAToProteinConverter("UUA"));
        Assert.AreEqual("L", SequenceHelpers.RNAToProteinConverter("CUA"));
        Assert.AreEqual("I", SequenceHelpers.RNAToProteinConverter("AUA"));
        Assert.AreEqual("V", SequenceHelpers.RNAToProteinConverter("GUA"));
        Assert.AreEqual("L", SequenceHelpers.RNAToProteinConverter("UUG"));
        Assert.AreEqual("L", SequenceHelpers.RNAToProteinConverter("CUG"));
        Assert.AreEqual("M", SequenceHelpers.RNAToProteinConverter("AUG"));
        Assert.AreEqual("V", SequenceHelpers.RNAToProteinConverter("GUG"));
        Assert.AreEqual("S", SequenceHelpers.RNAToProteinConverter("UCU"));
        Assert.AreEqual("P", SequenceHelpers.RNAToProteinConverter("CCU"));
        Assert.AreEqual("T", SequenceHelpers.RNAToProteinConverter("ACU"));
        Assert.AreEqual("A", SequenceHelpers.RNAToProteinConverter("GCU"));
        Assert.AreEqual("S", SequenceHelpers.RNAToProteinConverter("UCC"));
        Assert.AreEqual("P", SequenceHelpers.RNAToProteinConverter("CCC"));
        Assert.AreEqual("T", SequenceHelpers.RNAToProteinConverter("ACC"));
        Assert.AreEqual("A", SequenceHelpers.RNAToProteinConverter("GCC"));
        Assert.AreEqual("S", SequenceHelpers.RNAToProteinConverter("UCA"));
        Assert.AreEqual("P", SequenceHelpers.RNAToProteinConverter("CCA"));
        Assert.AreEqual("T", SequenceHelpers.RNAToProteinConverter("ACA"));
        Assert.AreEqual("A", SequenceHelpers.RNAToProteinConverter("GCA"));
        Assert.AreEqual("S", SequenceHelpers.RNAToProteinConverter("UCG"));
        Assert.AreEqual("P", SequenceHelpers.RNAToProteinConverter("CCG"));
        Assert.AreEqual("T", SequenceHelpers.RNAToProteinConverter("ACG"));
        Assert.AreEqual("A", SequenceHelpers.RNAToProteinConverter("GCG"));
        Assert.AreEqual("Y", SequenceHelpers.RNAToProteinConverter("UAU"));
        Assert.AreEqual("H", SequenceHelpers.RNAToProteinConverter("CAU"));
        Assert.AreEqual("N", SequenceHelpers.RNAToProteinConverter("AAU"));
        Assert.AreEqual("D", SequenceHelpers.RNAToProteinConverter("GAU"));
        Assert.AreEqual("Y", SequenceHelpers.RNAToProteinConverter("UAC"));
        Assert.AreEqual("H", SequenceHelpers.RNAToProteinConverter("CAC"));
        Assert.AreEqual("N", SequenceHelpers.RNAToProteinConverter("AAC"));
        Assert.AreEqual("D", SequenceHelpers.RNAToProteinConverter("GAC"));
        Assert.AreEqual("Stop", SequenceHelpers.RNAToProteinConverter("UAA"));
        Assert.AreEqual("Q", SequenceHelpers.RNAToProteinConverter("CAA"));
        Assert.AreEqual("K", SequenceHelpers.RNAToProteinConverter("AAA"));
        Assert.AreEqual("E", SequenceHelpers.RNAToProteinConverter("GAA"));
        Assert.AreEqual("Stop", SequenceHelpers.RNAToProteinConverter("UAG"));
        Assert.AreEqual("Q", SequenceHelpers.RNAToProteinConverter("CAG"));
        Assert.AreEqual("K", SequenceHelpers.RNAToProteinConverter("AAG"));
        Assert.AreEqual("E", SequenceHelpers.RNAToProteinConverter("GAG"));
        Assert.AreEqual("C", SequenceHelpers.RNAToProteinConverter("UGU"));
        Assert.AreEqual("R", SequenceHelpers.RNAToProteinConverter("CGU"));
        Assert.AreEqual("S", SequenceHelpers.RNAToProteinConverter("AGU"));
        Assert.AreEqual("G", SequenceHelpers.RNAToProteinConverter("GGU"));
        Assert.AreEqual("C", SequenceHelpers.RNAToProteinConverter("UGC"));
        Assert.AreEqual("R", SequenceHelpers.RNAToProteinConverter("CGC"));
        Assert.AreEqual("S", SequenceHelpers.RNAToProteinConverter("AGC"));
        Assert.AreEqual("G", SequenceHelpers.RNAToProteinConverter("GGC"));
        Assert.AreEqual("Stop", SequenceHelpers.RNAToProteinConverter("UGA"));
        Assert.AreEqual("R", SequenceHelpers.RNAToProteinConverter("CGA"));
        Assert.AreEqual("R", SequenceHelpers.RNAToProteinConverter("AGA"));
        Assert.AreEqual("G", SequenceHelpers.RNAToProteinConverter("GGA"));
        Assert.AreEqual("W", SequenceHelpers.RNAToProteinConverter("UGG"));
        Assert.AreEqual("R", SequenceHelpers.RNAToProteinConverter("CGG"));
        Assert.AreEqual("R", SequenceHelpers.RNAToProteinConverter("AGG"));
        Assert.AreEqual("G", SequenceHelpers.RNAToProteinConverter("GGG"));
    }


    [TestMethod]
    public void NumberOfProteins()
    {
        Assert.AreEqual(3, SequenceHelpers.NumberOfPossibleProteins("Stop"));
        Assert.AreEqual(1, SequenceHelpers.NumberOfPossibleProteins("W"));
        Assert.AreEqual(1, SequenceHelpers.NumberOfPossibleProteins("M"));
        Assert.AreEqual(2, SequenceHelpers.NumberOfPossibleProteins("F"));
        Assert.AreEqual(2, SequenceHelpers.NumberOfPossibleProteins("Y"));
        Assert.AreEqual(2, SequenceHelpers.NumberOfPossibleProteins("H"));
        Assert.AreEqual(2, SequenceHelpers.NumberOfPossibleProteins("N"));
        Assert.AreEqual(2, SequenceHelpers.NumberOfPossibleProteins("D"));
        Assert.AreEqual(2, SequenceHelpers.NumberOfPossibleProteins("Q"));
        Assert.AreEqual(2, SequenceHelpers.NumberOfPossibleProteins("K"));
        Assert.AreEqual(2, SequenceHelpers.NumberOfPossibleProteins("E"));
        Assert.AreEqual(2, SequenceHelpers.NumberOfPossibleProteins("C"));
        Assert.AreEqual(3, SequenceHelpers.NumberOfPossibleProteins("I"));
        Assert.AreEqual(4, SequenceHelpers.NumberOfPossibleProteins("V"));
        Assert.AreEqual(4, SequenceHelpers.NumberOfPossibleProteins("P"));
        Assert.AreEqual(4, SequenceHelpers.NumberOfPossibleProteins("T"));
        Assert.AreEqual(4, SequenceHelpers.NumberOfPossibleProteins("A"));
        Assert.AreEqual(4, SequenceHelpers.NumberOfPossibleProteins("G"));
        Assert.AreEqual(6, SequenceHelpers.NumberOfPossibleProteins("R"));
        Assert.AreEqual(6, SequenceHelpers.NumberOfPossibleProteins("S"));
        Assert.AreEqual(6, SequenceHelpers.NumberOfPossibleProteins("L"));
    }

    [TestMethod]
    public void NumberOfProteinsTotal()
    {
        Assert.AreEqual(64, SequenceHelpers.ProteinCodesToRNA.Sum(kv => kv.Value.Count));
    }

    [TestMethod]
    public void PossibleKmersListTest()
    {
        var output = SequenceHelpers.AllPossibleKmersList("ACGT");
        var expected = new List<string>
        {
            "ACGT",
            "CAGT",
            "GCAT",
            "CGAT",
            "ACGT",
            "CAGT",
            "TCGA",
            "CTGA",
            "GCTA",
            "CGTA",
            "TCGA",
            "CTGA",
            "TAGC",
            "ATGC",
            "GATC",
            "AGTC",
            "TAGC",
            "ATGC",
            "TACG",
            "ATCG",
            "CATG",
            "ACTG",
            "TACG",
            "ATCG"
        };

        for (var i = 0; i < expected.Count; i++) Assert.AreEqual(expected[i], output[i]);
    }
}