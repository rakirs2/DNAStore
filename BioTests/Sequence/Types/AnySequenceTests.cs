using Bio.Analysis.Types;
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

    [TestMethod]
    public void OneIndexSimpleTest()
    {
        var a = new AnySequence("GAGCCTACTAACGGGAT");
        var b = new Motif("GAG", 3);
        var result = a.MotifLocations(b);
        var expected = new long[] { 1 };
        Assert.IsTrue(Enumerable.SequenceEqual(expected, result));
    }

    [TestMethod]
    public void ZeroIndexSimpleTest()
    {
        var a = new AnySequence("GAGCCTACTAACGGGAT");
        var b = new Motif("GAG", 3);
        var result = a.MotifLocations(b, true);
        var expected = new long[] { 0 };
        Assert.IsTrue(Enumerable.SequenceEqual(expected, result));
    }

    [TestMethod]
    public void OneIndexExample()
    {
        var a = new AnySequence("GATATATGCATATACTT");
        var b = new Motif("ATAT", 4);
        var result = a.MotifLocations(b);
        var expected = new long[] { 2, 4, 10 };
        Assert.IsTrue(Enumerable.SequenceEqual(expected, result));
    }
}