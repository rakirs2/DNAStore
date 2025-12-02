using Bio.Sequence.Types;
using BioTests.Analysis.Types;
using JetBrains.Annotations;

namespace BioTests.Analysis.Types;

[TestClass]
[TestSubject(typeof(MotifProfile<>))]
public class MotifProfileTest
{

    [TestMethod]
    public void METHOD()
    {
        var temp = new MotifProfile<DnaSequence>(null);
    }
}