using Bio.Math;
using Bio.Sequence.Types;

namespace BioTests.Sequence.Types;

[TestClass()]
public class ProteinSequenceTests
{
    [TestMethod()]
    public void ProteinSequenceTest()
    {
        var protein = new ProteinSequence("SKADYEK");
        Assert.IsTrue(Helpers.DoublesEqualWithinRange(821.392, protein.MolecularWeight));
    }
}