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
        Assert.AreEqual(2, rnaSequence.NumberOfPerfectMatchingsCached());
    }
    
    [TestMethod]
    public void PerfectMatchingsEmpty()
    {
        var rnaSequence = new RnaSequence("");
        Assert.AreEqual(1, rnaSequence.NumberOfPerfectMatchingsCached());
    }
    
    [TestMethod]
    public void PerfectMatchingsLong()
    {
        var rnaSequence = new RnaSequence("ACGUACGU");
        Assert.AreEqual(3, rnaSequence.NumberOfPerfectMatchingsCached());
    }
    
    [TestMethod]
    public void PerfectMatchingsDynamnic()
    {
        var rnaSequence = new RnaSequence("UAGCGUGAUCAC");
        Assert.AreEqual(2, rnaSequence.NumberOfPerfectMatchingsCached());
    }
    [TestMethod]
    public void PerfectMatchingsDynamnicDouble()
    {
        var rnaSequence = new RnaSequence("UAGCGUGAUCACUAGCGUGAUCAC");
        Assert.AreEqual(30, rnaSequence.NumberOfPerfectMatchingsCached());
    }
    
    [TestMethod]
    public void MaximalMatchings()
    {
        var rnaSequence = new RnaSequence("AUGCUUC");
        Assert.AreEqual(6, rnaSequence.MaximumNumberOfMatchings());
    }

    [TestMethod]
    public void MotzkinNumberGivenCase()
    {
        var rnaSequence = new RnaSequence("AUAU");
        Assert.AreEqual(7, rnaSequence.MotzkinNumber());
    }
    
    [TestMethod]
    public void MotzkinNumberFirstLetterExempt()
    {
        var rnaSequence = new RnaSequence("UAU");
        Assert.AreEqual(3, rnaSequence.MotzkinNumber());
    }
    
    [TestMethod]
    public void MotzkinNumberEmptyBaseCase()
    {
        var rnaSequence = new RnaSequence("");
        Assert.AreEqual(1, rnaSequence.MotzkinNumber());
    }
    
    [TestMethod]
    public void MotzkinNumberLength1BaseCase()
    {
        var rnaSequence = new RnaSequence("U");
        Assert.AreEqual(1, rnaSequence.MotzkinNumber());
    }
    
    [TestMethod]
    public void IsComplementTest()
    {
       Assert.IsFalse(RnaSequence.IsComplement('A', 'T'));
       Assert.IsTrue(RnaSequence.IsComplement('A', 'U'));
       Assert.IsTrue(RnaSequence.IsComplement('C', 'G'));
       Assert.IsFalse(RnaSequence.IsComplement(' ', '"'));
    }

    [TestMethod]
    public void WobbleDistanceLessThanSafe()
    {
        var seq = new RnaSequence("AUAU");
        Assert.AreEqual(1, seq.NonCrossingsWithWobbleDistance());
    }
    
    [TestMethod]
    public void WobbleBaseCase()
    {
        var seq = new RnaSequence("AUAUU");
        Assert.AreEqual(2, seq.NonCrossingsWithWobbleDistance());
    }
    
    [TestMethod]
    public void WobbleBaseCase2()
    {
        var seq = new RnaSequence("AUAUAU");
        Assert.AreEqual(2, seq.NonCrossingsWithWobbleDistance());
    }
    
    [TestMethod]
    public void WobbleBaseCase3()
    {
        var seq = new RnaSequence("AUUUUU");
        Assert.AreEqual(3, seq.NonCrossingsWithWobbleDistance());
    }
    
    [TestMethod]
    public void WobbleBaseCase4()
    {
        var seq = new RnaSequence("AUUUUUU");
        Assert.AreEqual(4, seq.NonCrossingsWithWobbleDistance());
    }
    
    [TestMethod]
    public void WobbleBaseCase5()
    {
        var seq = new RnaSequence("AAAAAAAAA");
        Assert.AreEqual(1, seq.NonCrossingsWithWobbleDistance());
    }
    
    [TestMethod]
    public void WobbleBaseCase6()
    {
        var seq = new RnaSequence("AUCGAUCG");
        Assert.AreEqual(4, seq.NonCrossingsWithWobbleDistance());
    }

    
    [TestMethod]
    public void WobbleGiven()
    {
        var seq = new RnaSequence("AUGCUAGUACGGAGCGAGUCUAGCGAGCGAUGUCGUGAGUACUAUAUAUGCGCAUAAGCCACGU");
        Assert.AreEqual(284850219977421, seq.NonCrossingsWithWobbleDistance());
    }
}