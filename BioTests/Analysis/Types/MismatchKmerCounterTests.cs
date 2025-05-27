

using Bio.Analysis.Types;
using Bio.Sequence.Types;

namespace BioTests.Analysis.Types;

[TestClass()]
public class MismatchKmerCounterTests
{
    [TestMethod()]
    public void MismatchKmerCounterSimple()
    {
        var sequence = new AnySequence("ACGTTGCATGTCGCATGATGCATGAGAGCT");
        var counter = new MismatchKmerCounter(4, sequence, 1);
        var output = counter.GetKmers("ACGT");
        Assert.IsTrue(output.SetEquals(new HashSet<string>() { "GATG", "ATGC", "ATGT" }));
    }

    [TestMethod()]
    public void MismatchKmerCounterShort()
    {
        var sequence = new AnySequence("AGGT");
        var counter = new MismatchKmerCounter(2, sequence, 1);
        var output = counter.GetKmers("ACGT");
        Assert.IsTrue(output.SetEquals(new HashSet<string>() { "GG" }));
    }
}