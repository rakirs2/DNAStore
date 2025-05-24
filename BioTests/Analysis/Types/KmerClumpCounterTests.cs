using Bio.Sequence.Types;

namespace Bio.Analysis.Types.Tests;

[TestClass()]
public class KmerClumpCounterTests
{
    [TestMethod()]
    public void KmerClumpCounterTest()
    {
        var seq = new AnySequence(
            "CGGACTCGACAGATGTGAAGAAATGTGAAGACTGAGTGAAGAGAAGAGGAAACACGACACGACATTGCGACATAATGTACGAATGTAATGTGCCTATGGC");
        var counter = new KmerClumpCounter(seq, 20, 5, 3);
        Assert.AreEqual(20, counter.ScanLength);
        Assert.AreEqual(5, counter.KmerLength);
        Assert.AreEqual(3, counter.MinCount);
    }

    [TestMethod()]
    public void KmerClumpCounterValidKmers()
    {
        var seq = new AnySequence(
            "CGGACTCGACAGATGTGAAGAAATGTGAAGACTGAGTGAAGAGAAGAGGAAACACGACACGACATTGCGACATAATGTACGAATGTAATGTGCCTATGGC");
        var counter = new KmerClumpCounter(seq, 75, 5, 4);
        var expected = new HashSet<string> { "CGACA", "GAAGA", "AATGT" };
        Assert.IsTrue(counter.ValidKmers.SetEquals(expected));
    }
}