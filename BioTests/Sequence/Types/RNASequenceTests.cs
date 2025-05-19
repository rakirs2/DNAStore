using Bio.Sequence.Types;

namespace BioTests.Sequence.Types;


[TestClass]
public class RNASequenceTests
{
    [TestMethod]
    public void GetExpectedProteinStringTest()
    {
        var rnaSequence = new RNASequence("AUGGCCAUGGCGCCCAGAACUGAGAUCAAUAGUACCCGUAUUAACGGGUGA");
        Assert.AreEqual("MAMAPRTEINSTRING", rnaSequence.GetExpectedProteinString());
    }
}