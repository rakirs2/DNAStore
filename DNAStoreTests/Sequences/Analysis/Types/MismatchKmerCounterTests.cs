using DnaStore.Sequence.Analysis.Types;

namespace BaseTests.Sequence.Analysis.Types;

[TestClass]
public class MismatchKmerCounterTests
{
    [TestMethod]
    public void MismatchKmerCounterSimple()
    {
        var sequence = new DnaStore.Sequence.Types.Sequence("ACGTTGCATGTCGCATGATGCATGAGAGCT");
        var counter = new MismatchKmerCounter(4, sequence, 1);
        var output = counter.GetKmers("ACGT");
        Assert.IsTrue(output.SetEquals(new HashSet<string> { "GATG", "ATGC", "ATGT" }));
    }

    [TestMethod]
    public void MismatchKmerCounterShort()
    {
        var sequence = new DnaStore.Sequence.Types.Sequence("AGGT");
        var counter = new MismatchKmerCounter(2, sequence, 1);
        var output = counter.GetKmers("ACGT");
        Assert.IsTrue(output.SetEquals(new HashSet<string> { "GG" }));
    }

    [TestMethod]
    public void MismatchKmerCounterComplement()
    {
        var sequence = new DnaStore.Sequence.Types.Sequence("ACGTTGCATGTCGCATGATGCATGAGAGCT");
        var counter = new MismatchKmerCounter(4, sequence, 1);
        var output = counter.GetKmers("ACGT", true);
        Assert.IsTrue(counter.HighestFrequencyKmers.SetEquals(new HashSet<string> { "ATGT", "ACAT" }));
    }
}