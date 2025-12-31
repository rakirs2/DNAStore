using DNAStore.Sequences.Types;

namespace DNAStoreTests.Sequences.Sequences.Types;

[TestClass]
public class RnaSequenceTest
{
    [TestMethod]
    public void GetExpectedProteinStringTest()
    {
        var rnaSequence = new RnaSequence("AUGGCCAUGGCGCCCAGAACUGAGAUCAAUAGUACCCGUAUUAACGGGUGA");
        Assert.AreEqual("MAMAPRTEINSTRING*", rnaSequence.GetExpectedProteinString());
    }

    [TestMethod]
    public void PerfectMatchings()
    {
        var rnaSequence = new RnaSequence("AGCUAGUCAU");
        Assert.AreEqual(12, rnaSequence.NumberOfPerfectMatchings());
    }

    [TestMethod]
    public void PerfectMatchingsInvalidAU()
    {
        var rnaSequence = new RnaSequence("AGCUAGUCA");
        Assert.ThrowsExactly<ArgumentException>(() => rnaSequence.NumberOfPerfectMatchings());
    }

    [TestMethod]
    public void PerfectMatchingsInvalidGC()
    {
        var rnaSequence = new RnaSequence("AGCUAGUAC");
        Assert.ThrowsExactly<ArgumentException>(() => rnaSequence.NumberOfPerfectMatchings());
    }
    
    [TestMethod]
    public void PerfectMatchingsModular()
    {
        var rnaSequence = new RnaSequence("AUAU");
        Assert.AreEqual(2, rnaSequence.NumberOfPerfectMatchingsDynamic());
    }
    
    [TestMethod]
    public void PerfectMatchingsEmpty()
    {
        var rnaSequence = new RnaSequence("");
        Assert.AreEqual(1, rnaSequence.NumberOfPerfectMatchingsDynamic());
    }
    
    [TestMethod]
    public void PerfectMatchingsLong()
    {
        var rnaSequence = new RnaSequence("ACGUACGU");
        Assert.AreEqual(3, rnaSequence.NumberOfPerfectMatchingsDynamic());
    }
    
    [TestMethod]
    public void PerfectMatchingsDynamnic()
    {
        var rnaSequence = new RnaSequence("UAGCGUGAUCAC");
        Assert.AreEqual(2, rnaSequence.NumberOfPerfectMatchingsDynamic());
    }
    [TestMethod]
    public void PerfectMatchingsDynamnicDouble()
    {
        var rnaSequence = new RnaSequence("UAGCGUGAUCACUAGCGUGAUCAC");
        Assert.AreEqual(30, rnaSequence.NumberOfPerfectMatchingsDynamic());
    }
    [TestMethod]
    public void MaximalMatchings()
    {
        var rnaSequence = new RnaSequence("AUGCUUC");
        Assert.AreEqual(6, rnaSequence.MaximumNumberOfMatchings());
    }
     
    [TestMethod]
    public void MaximalMatchings2()
    {
        var rnaSequence = new RnaSequence("AAUUUGAAAGCGAAGAUACCGCAGGCAACAAGUCCGUAAGUAACUGAUGCUACUGUUGACAAGUGGCUCUGGCUAAACCAU");
        Assert.AreEqual(6, rnaSequence.MaximumNumberOfMatchings());
    }
}