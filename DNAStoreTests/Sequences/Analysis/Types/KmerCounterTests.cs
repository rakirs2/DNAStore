using DNAStore.Sequences.Analysis.Types;
using DNAStore.Sequences.Types;

namespace BaseTests.Sequences.Analysis.Types;

[TestClass]
public class KmerCounterTests
{
    [TestMethod]
    public void KmerCounterTest()
    {
        var sequence = new Sequence("aabbccddeeffeeggee");

        var kmerCounter = new KmerCounter(sequence, 2);
        Assert.AreEqual(2, kmerCounter.KmerLength);
        Assert.AreEqual("ee", kmerCounter.HighestFrequencyKmers.ToList()[0]);
        Assert.AreEqual(3, kmerCounter.CurrentHighestFrequency);
    }

    [TestMethod]
    public void KmerCounterTestMultipleMostFrequent()
    {
        var sequence = new Sequence("ACGTTGCATGTCGCATGATGCATGAGAGCT");
        var kmerCounter = new KmerCounter(sequence, 4);
        // Assert.AreEqual(2, kmerCounter.KmerLength);
        // Assert.AreEqual("ee", kmerCounter.HighestFrequencyKmers[0]);
        Assert.AreEqual(3, kmerCounter.CurrentHighestFrequency);
        Assert.IsTrue(kmerCounter.HighestFrequencyKmers.Contains("GCAT"));
        Assert.IsTrue(kmerCounter.HighestFrequencyKmers.Contains("CATG"));
        Assert.AreEqual(2, kmerCounter.HighestFrequencyKmers.ToList().Count);
    }
}