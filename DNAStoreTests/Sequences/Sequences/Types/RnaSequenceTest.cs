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
        var rnaSequence = new RnaSequence("GGUGGCCACGCAGUGGCCCGUAGGCGCGCCGCCGGCAUGCAAAUUGCUGCGCUCCGCGGCGUAAUCGAAGGGCGCCGAUCAUCGCAGCUCUACGUCGAUAGCACGAGCUAUUUACGUGGCGCCUAAAUAUAUAUGCUUUAUAAUAGGAGCUCACGUUACGCGUGGCGCCCUAGGCCGAACGAUCCGGCGUAUCUAUAUUAAUAAUAGCUCGUGGACGUCCGCACGUGCGGCCACGGCCUAUAGCUCGUAAUA");
        Assert.AreEqual(1, rnaSequence.NumberOfPerfectMatchingsDynamic());
    }
}