

using Bio.Analysis.Types;
using Bio.Sequence.Types;

namespace BioTests.Analysis.Types;

[TestClass()]
public class MismatchKmerCounterTests
{
    [TestMethod()]
    public void MismatchKmerCounterTest()
    {
        var sequence = new AnySequence("ACGTTGCATGTCGCATGATGCATGAGAGCT");
        var counter = new MismatchKmerCounter(4, sequence, 1);
        var output = counter.GetKmers("ACGT");
        Assert.IsTrue(output.SetEquals(new HashSet<string>() { "GATG", "ATGC", "ATGT" }));
    }
}