using Bio.Analysis.Types;
using Bio.Sequence.Types;

namespace BioTests.Analysis.Types;

[TestClass]
public class KmerCounterTests
{
    // TODO: what does eee get counted 2 or 3x -- > is there a DNA related
    // example here?

    [TestMethod]
    public void KmerCounterTest()
    {
        var sequence = new AnySequence("aabbccddeeffeeggee");

        var kmerCounter = new KmerCounter(sequence, 2);
        Assert.AreEqual(2, kmerCounter.KmerLength);
        Assert.AreEqual("ee", kmerCounter.HighestFrequencyKmers.ToList()[0]);
        Assert.AreEqual(3, kmerCounter.CurrentHighestFrequency);
    }

    [TestMethod]
    public void KmerCounterTestMultipleMostFrequent()
    {
        var sequence = new AnySequence("ACGTTGCATGTCGCATGATGCATGAGAGCT");
        var kmerCounter = new KmerCounter(sequence, 4);
        // Assert.AreEqual(2, kmerCounter.KmerLength);
        // Assert.AreEqual("ee", kmerCounter.HighestFrequencyKmers[0]);
        Assert.AreEqual(3, kmerCounter.CurrentHighestFrequency);
        Assert.IsTrue(kmerCounter.HighestFrequencyKmers.Contains("GCAT"));
        Assert.IsTrue(kmerCounter.HighestFrequencyKmers.Contains("CATG"));
        Assert.AreEqual(2, kmerCounter.HighestFrequencyKmers.ToList().Count);
    }
}