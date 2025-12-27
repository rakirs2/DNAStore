using Bio.Analysis.Types;
using Bio.Sequences.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class SequenceTests
{
    [TestMethod]
    public void BasicConstruction()
    {
        var anySequence = new Bio.Sequences.Types.Sequence("Somaf321434");
        Assert.IsNotNull(anySequence, "Object should construct");
        Assert.AreEqual(11, anySequence.Length);
    }

    [TestMethod]
    public void HammingMismatchedLengths()
    {
        var a = new Bio.Sequences.Types.Sequence("a");
        var b = new Bio.Sequences.Types.Sequence("ab");
        Assert.ThrowsExactly<InvalidDataException>(() => Bio.Sequences.Types.Sequence.HammingDistance(a, b));
    }

    [TestMethod]
    public void HammingDistanceCaseSensitive()
    {
        var a = new Bio.Sequences.Types.Sequence("ac");
        var b = new Bio.Sequences.Types.Sequence("ab");
        var result = Bio.Sequences.Types.Sequence.HammingDistance(a, b);
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void HammingDistanceRealistic()
    {
        var a = new Bio.Sequences.Types.Sequence("GAGCCTACTAACGGGAT");
        var b = new Bio.Sequences.Types.Sequence("CATCGTAATGACGGCCT");
        var result = Bio.Sequences.Types.Sequence.HammingDistance(a, b);
        Assert.AreEqual(7, result);
    }

    [TestMethod]
    public void OneIndexSimpleTest()
    {
        var a = new Bio.Sequences.Types.Sequence("GAGCCTACTAACGGGAT");
        var b = new Motif("GAG", 3);
        var result = a.MotifLocations(b);
        var expected = new long[] { 1 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void ZeroIndexSimpleTest()
    {
        var a = new Bio.Sequences.Types.Sequence("GAGCCTACTAACGGGAT");
        var b = new Motif("GAG", 3);
        var result = a.MotifLocations(b, true);
        var expected = new long[] { 0 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void OneIndexExample()
    {
        var a = new Bio.Sequences.Types.Sequence("GATATATGCATATACTT");
        var b = new Motif("ATAT", 4);
        var result = a.MotifLocations(b);
        var expected = new long[] { 2, 4, 10 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void AreSequenceEqualTest()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("abcde");
        var seq2 = new Bio.Sequences.Types.Sequence("abcde");
        Assert.IsTrue(Bio.Sequences.Types.Sequence.AreSequenceEqual(seq1, seq2));
    }

    [TestMethod]
    public void AreSequenceEqualTestDifferentSequence()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("abcde");
        var seq2 = new Bio.Sequences.Types.Sequence("abcdf");
        Assert.IsFalse(Bio.Sequences.Types.Sequence.AreSequenceEqual(seq1, seq2));
    }

    [TestMethod]
    public void AreSequenceEqualTestDifferentLength()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("abcde");
        var seq2 = new Bio.Sequences.Types.Sequence("abcdeg");
        Assert.IsFalse(Bio.Sequences.Types.Sequence.AreSequenceEqual(seq1, seq2));
    }

    [TestMethod]
    public void RemoveIntronsTestBeginning()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("abcde");
        var output = seq1.RemoveIntrons(new List<Bio.Sequences.Types.Sequence> { new("a") });
        Assert.AreEqual("bcde", output.ToString());
    }

    [TestMethod]
    public void RemoveIntronsTestEnd()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("abcde");
        var output = seq1.RemoveIntrons(new List<Bio.Sequences.Types.Sequence> { new("e") });
        Assert.AreEqual("abcd", output.ToString());
    }

    [TestMethod]
    public void RemoveIntronsTestNull()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("abcde");
        Assert.ThrowsExactly<ArgumentNullException>(() => seq1.RemoveIntrons(null));
    }

    [TestMethod]
    public void FindFirstPossibleSubSequenceTest()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("ACGTACGTGACG");
        var subseq = new Bio.Sequences.Types.Sequence("GTA");

        var result = seq1.FindFirstPossibleSubSequence(subseq);
        var expected = new List<int> { 3, 4, 5 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void OverlapTest()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("ACGTACGTGACG");
        var seq2 = new Bio.Sequences.Types.Sequence("GTGACGCATTTG");
        Assert.AreEqual(6, Bio.Sequences.Types.Sequence.CalculateOverlap(seq1, seq2));
        Assert.AreEqual(0, Bio.Sequences.Types.Sequence.CalculateOverlap(seq2, seq1));
    }

    [TestMethod]
    public void InvalidTypeThrows()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("ACGTACGTGACG");
        Assert.ThrowsExactly<ArgumentException>(() => seq1.CompareTo("something"));
    }

    [TestMethod]
    public void CompareToEqual()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("ABCD");
        var seq2 = new Bio.Sequences.Types.Sequence("ABCD");
        var seq3 = new Bio.Sequences.Types.Sequence("ABC");
        var seq4 = new Bio.Sequences.Types.Sequence("ABCE");
        Assert.AreEqual(0, seq1.CompareTo(seq2));
        Assert.AreEqual(1, seq1.CompareTo(seq3));
        Assert.AreEqual(-1, seq1.CompareTo(seq4));
    }

    [TestMethod]
    public void EnumerableChar()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("ABCD");
        var expected = "ABCD";
        using (var enumerator = seq1.GetEnumerator())
        {
            for (var i = 0; i < expected.Length; i++)
            {
                enumerator.MoveNext();
                Assert.AreEqual(expected[i], enumerator.Current);
            }

            Assert.IsFalse(enumerator.MoveNext());
        }
    }

    [TestMethod]
    public void ContainsStringWithHammingDistance()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("AAAAAAAAT");
        Assert.IsTrue(seq1.ContainsString("TT", 1));
    }

    [TestMethod]
    public void KmerEnumerator()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("AAAAAAAA");
        var counter = 0;
        foreach (var something in seq1.GetKmerEnumerator(2)) counter++;

        Assert.AreEqual(7, counter);
    }

    [TestMethod]
    public void RandomKmerGenerator()
    {
        var seq1 = new Bio.Sequences.Types.Sequence("FEZLWBDYZGJQFSMZAJTADAYAXTNXODMV");
        var s1 = seq1.GetRandomKmer(5);
        var s2 = seq1.GetRandomKmer(5);
        var s3 = seq1.GetRandomKmer(5);

        // assert 2/3 work. This should be sufficient for a random setup.
        var counter = 0;
        if (s1.Equals(s2))
            counter++;
        if (s1.Equals(s3))
            counter++;
        if (s2.Equals(s3))
            counter++;
        Assert.IsTrue(counter <= 1);
    }

    [TestMethod]
    public void DistanceBetweenPattern()
    {
        List<Bio.Sequences.Types.Sequence> sequences = new()
        {
            new Bio.Sequences.Types.Sequence("TTACCTTAAC"),
            new Bio.Sequences.Types.Sequence("GATATCTGTC"),
            new Bio.Sequences.Types.Sequence("ACGGCGTTCG"),
            new Bio.Sequences.Types.Sequence("CCCTAAAGAG"),
            new Bio.Sequences.Types.Sequence("CGTCAGAGGT")
        };
        Assert.AreEqual(5, Bio.Sequences.Types.Sequence.DistancePatternAndString("AAA", sequences));
    }

    [TestMethod]
    public void InvalidEditDistance()
    {
        var seq1 = new DnaSequence("AAA");
        var seq2 = new RnaSequence("UUU");
        Assert.ThrowsExactly<ArgumentException>(() => seq1.EditDistance(seq2));
    }
}