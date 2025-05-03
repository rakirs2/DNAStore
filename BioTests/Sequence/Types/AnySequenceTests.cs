using Bio.Sequence.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class AnySequenceTests
{
    [TestMethod]
    public void BasicConstruction()
    {
        var anySequence = new AnySequence("Somaf321434");
        Assert.IsNotNull(anySequence, "Object should construct");
        Assert.AreEqual(11, anySequence.Length);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void HammingMismatchedLengths()
    {
        var a = new AnySequence("a");
        var b = new AnySequence("ab");
        var result = AnySequence.HammingDistance(a, b);
    }

    [TestMethod]
    public void HammingDistanceCaseSensitive()
    {
        var a = new AnySequence("ac");
        var b = new AnySequence("ab");
        var result = AnySequence.HammingDistance(a, b);
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void HammingDistanceRealistic()
    {
        var a = new AnySequence("GAGCCTACTAACGGGAT");
        var b = new AnySequence("CATCGTAATGACGGCCT");
        var result = AnySequence.HammingDistance(a, b);
        Assert.AreEqual(7, result);
    }
}