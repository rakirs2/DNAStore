using Bio.Sequences.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class ReverseComplementDictionaryTest
{
    [TestMethod]
    public void EmptyAccess()
    {
        var dict = new ReverseComplementDictionary();
        Assert.AreEqual(0, dict[new DnaSequence("")]);
    }

    [TestMethod]
    public void EmptySequencesAreNotTracked()
    {
        var dict = new ReverseComplementDictionary();
        dict.Add(new DnaSequence(""));
        Assert.AreEqual(0, dict[new DnaSequence("")]);
    }

    [TestMethod]
    public void SimpleInsertion()
    {
        var dict = new ReverseComplementDictionary();
        dict.Add(new DnaSequence("A"));
        Assert.AreEqual(1, dict[new DnaSequence("A")]);
        Assert.AreEqual(1, dict[new DnaSequence("T")]);
    }
}