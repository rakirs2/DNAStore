using DNAStore.Base.Utils;
using DNAStore.BioMath;

namespace DNAStoreTests.Sequences.Math;

[TestClass]
public class ProbabilityTests
{
    [TestMethod]
    public void PercentDominantTest()
    {
        Assert.AreEqual(.783333, Probability.PercentDominant(2, 2, 2), 1e-5);
    }

    [TestMethod]
    public void GetPermutationsOfASet()
    {
        var allPerms = Probability.GetPermutations(Enumerable.Range(1, 3), 3);
        Assert.AreEqual(6, allPerms.Count());
    }

    [TestMethod]
    public void GetPermutationsOfASetDNA()
    {
        var allPerms = Probability.GetPermutations(['a', 'c', 'g', 't'], 4);
        Assert.AreEqual(24, allPerms.Count());
    }

    [TestMethod]
    public void GetPermutationsOfASetRNA()
    {
        var allPerms = Probability.GetPermutations(['a', 'c', 'g', 'u'], 4);
        Assert.AreEqual(24, allPerms.Count());
    }

    [TestMethod]
    public void ExpectedDominantOffspringTest()
    {
        var total = Probability.ExpectedDominantOffspring(1, 0, 0, 1, 0, 1, 2);
        Assert.AreEqual(3.5, total);
    }

    [TestMethod]
    public void GenerateAllKmersTest()
    {
        var output = Probability.GenerateAllKmers("acgt", 1);
        Assert.IsTrue(output.SequenceEqual(["a", "c", "g", "t"]));
    }

    [TestMethod]
    public void GenerateAllKmersTest_Length2()
    {
        var output = Probability.GenerateAllKmers("acgt", 2);
        Assert.IsTrue(output.SequenceEqual([
            "aa", "ac", "ag", "at", "ca", "cc", "cg", "ct", "ga", "gc", "gg", "gt", "ta", "tc", "tg", "tt"
        ]));
    }

    [TestMethod]
    public void GenerateAllKmersAndSubKmersTest()
    {
        var output = Probability.GenerateAllKmers("acgt", 1);
        Assert.IsTrue(output.SequenceEqual([
            "a", "c", "g", "t"
        ]));
    }

    [TestMethod]
    public void GenerateAllKmersAndSubKmersTwoTest()
    {
        var output = Probability.GenerateAllKmersAndSubKmers("acgt", 2);
        Assert.IsTrue(output.SequenceEqual([
            "a", "aa", "ac", "ag", "at", "c", "ca", "cc", "cg", "ct", "g", "ga", "gc", "gg", "gt", "t", "ta", "tc",
            "tg", "tt"
        ]));
    }

    [TestMethod]
    public void NumberOfSubSets()
    {
        Assert.AreEqual(4, Probability.NumberOfSets(2));
        Assert.AreEqual(8, Probability.NumberOfSets(3));
    }

    [TestMethod]
    public void NumberOfSubSetsLarge()
    {
        Assert.AreEqual(966784, Probability.NumberOfSetsLarge(874));
    }

    [TestMethod]
    public void Bernoulli()
    {
        Assert.AreEqual(0.8, Probability.SimpleBernoulli(.2, 0));
    }

    [TestMethod]
    public void PartialPermutations()
    {
        Assert.AreEqual(51200, Probability.PartialPermutations(21, 7));
    }

    [TestMethod]
    public void SignedPermutations()
    {
        var results = new HashSet<int[]>(new IntArrayComparer());
        Probability.GenerateSignedPermutations(new[] { 1, 2 }, 0, results);
    }

    [TestMethod]
    public void GenerateSignedPermutations()
    {
        Assert.AreEqual(8, Probability.GenerateSignedPermutations(2).ToArray().Length);
    }
}