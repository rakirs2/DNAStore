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
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void ZeroIndexSimpleTest()
    {
        var a = new AnySequence("GAGCCTACTAACGGGAT");
        var b = new Motif("GAG", 3);
        var result = a.MotifLocations(b, true);
        var expected = new long[] { 0 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void OneIndexExample()
    {
        var a = new AnySequence("GATATATGCATATACTT");
        var b = new Motif("ATAT", 4);
        var result = a.MotifLocations(b);
        var expected = new long[] { 2, 4, 10 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void AreSequenceEqualTest()
    {
        var seq1 = new AnySequence("abcde");
        var seq2 = new AnySequence("abcde");
        Assert.IsTrue(AnySequence.AreSequenceEqual(seq1, seq2));
    }

    [TestMethod]
    public void AreSequenceEqualTestDifferentSequence()
    {
        var seq1 = new AnySequence("abcde");
        var seq2 = new AnySequence("abcdf");
        Assert.IsFalse(AnySequence.AreSequenceEqual(seq1, seq2));
    }

    [TestMethod]
    public void AreSequenceEqualTestDifferentLength()
    {
        var seq1 = new AnySequence("abcde");
        var seq2 = new AnySequence("abcdeg");
        Assert.IsFalse(AnySequence.AreSequenceEqual(seq1, seq2));
    }

    [TestMethod]
    public void RemoveIntronsTestBeginning()
    {
        var seq1 = new AnySequence("abcde");
        var output = seq1.RemoveIntrons(new List<AnySequence> { new("a") });
        Assert.AreEqual("bcde", output.ToString());
    }

    [TestMethod]
    public void RemoveIntronsTestEnd()
    {
        var seq1 = new AnySequence("abcde");
        var output = seq1.RemoveIntrons(new List<AnySequence> { new("e") });
        Assert.AreEqual("abcd", output.ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RemoveIntronsTestNull()
    {
        var seq1 = new AnySequence("abcde");
        var output = seq1.RemoveIntrons(null);
        Assert.AreEqual("abcde", output.ToString());
    }

    [TestMethod]
    public void FindFirstPossibleSubSequenceTest()
    {
        var seq1 = new AnySequence("ACGTACGTGACG");
        var subseq = new AnySequence("GTA");

        var result = seq1.FindFirstPossibleSubSequence(subseq);
        var expected = new List<int> { 3, 4, 5 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }
}