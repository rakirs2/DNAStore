using Bio.Analysis.Types;
using Bio.Sequence.Types;


namespace BioTests.Sequence.Types;

[TestClass]
public class SequenceTests
{
    [TestMethod]
    public void BasicConstruction()
    {
        var anySequence = new Bio.Sequence.Types.Sequence("Somaf321434");
        Assert.IsNotNull(anySequence, "Object should construct");
        Assert.AreEqual(11, anySequence.Length);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void HammingMismatchedLengths()
    {
        var a = new Bio.Sequence.Types.Sequence("a");
        var b = new Bio.Sequence.Types.Sequence("ab");
        var result = Bio.Sequence.Types.Sequence.HammingDistance(a, b);
    }

    [TestMethod]
    public void HammingDistanceCaseSensitive()
    {
        var a = new Bio.Sequence.Types.Sequence("ac");
        var b = new Bio.Sequence.Types.Sequence("ab");
        var result = Bio.Sequence.Types.Sequence.HammingDistance(a, b);
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void HammingDistanceRealistic()
    {
        var a = new Bio.Sequence.Types.Sequence("GAGCCTACTAACGGGAT");
        var b = new Bio.Sequence.Types.Sequence("CATCGTAATGACGGCCT");
        var result = Bio.Sequence.Types.Sequence.HammingDistance(a, b);
        Assert.AreEqual(7, result);
    }

    [TestMethod]
    public void OneIndexSimpleTest()
    {
        var a = new Bio.Sequence.Types.Sequence("GAGCCTACTAACGGGAT");
        var b = new Motif("GAG", 3);
        var result = a.MotifLocations(b);
        var expected = new long[] { 1 };
        Assert.IsTrue(Enumerable.SequenceEqual(expected, result));
    }

    [TestMethod]
    public void ZeroIndexSimpleTest()
    {
        var a = new Bio.Sequence.Types.Sequence("GAGCCTACTAACGGGAT");
        var b = new Motif("GAG", 3);
        var result = a.MotifLocations(b, true);
        var expected = new long[] { 0 };
        Assert.IsTrue(Enumerable.SequenceEqual(expected, result));
    }

    [TestMethod]
    public void OneIndexExample()
    {
        var a = new Bio.Sequence.Types.Sequence("GATATATGCATATACTT");
        var b = new Motif("ATAT", 4);
        var result = a.MotifLocations(b);
        var expected = new long[] { 2, 4, 10 };
        Assert.IsTrue(Enumerable.SequenceEqual(expected, result));
    }

    [TestMethod()]
    public void AreSequenceEqualTest()
    {
        var seq1 = new Bio.Sequence.Types.Sequence("abcde");
        var seq2 = new Bio.Sequence.Types.Sequence("abcde");
        Assert.IsTrue(Bio.Sequence.Types.Sequence.AreSequenceEqual(seq1, seq2));
    }

    [TestMethod()]
    public void AreSequenceEqualTestDifferentSequence()
    {
        var seq1 = new Bio.Sequence.Types.Sequence("abcde");
        var seq2 = new Bio.Sequence.Types.Sequence("abcdf");
        Assert.IsFalse(Bio.Sequence.Types.Sequence.AreSequenceEqual(seq1, seq2));
    }

    [TestMethod()]
    public void AreSequenceEqualTestDifferentLength()
    {
        var seq1 = new Bio.Sequence.Types.Sequence("abcde");
        var seq2 = new Bio.Sequence.Types.Sequence("abcdeg");
        Assert.IsFalse(Bio.Sequence.Types.Sequence.AreSequenceEqual(seq1, seq2));
    }
}