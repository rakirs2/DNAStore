using DnaStore.Sequence.Analysis.Types;
using DnaStore.Sequence.Types;

namespace BaseTests.Sequence.Sequences.Types;

[TestClass]
public class SequenceTests
{
    [TestMethod]
    public void BasicConstruction()
    {
        var anySequence = new DnaStore.Sequence.Types.Sequence("Somaf321434");
        Assert.IsNotNull(anySequence, "Object should construct");
        Assert.AreEqual(11, anySequence.Length);
    }

    [TestMethod]
    public void HammingMismatchedLengths()
    {
        var a = new DnaStore.Sequence.Types.Sequence("a");
        var b = new DnaStore.Sequence.Types.Sequence("ab");
        Assert.ThrowsExactly<InvalidDataException>(() => DnaStore.Sequence.Types.Sequence.HammingDistance(a, b));
    }

    [TestMethod]
    public void HammingDistanceCaseSensitive()
    {
        var a = new DnaStore.Sequence.Types.Sequence("ac");
        var b = new DnaStore.Sequence.Types.Sequence("ab");
        var result = DnaStore.Sequence.Types.Sequence.HammingDistance(a, b);
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void HammingDistanceRealistic()
    {
        var a = new DnaStore.Sequence.Types.Sequence("GAGCCTACTAACGGGAT");
        var b = new DnaStore.Sequence.Types.Sequence("CATCGTAATGACGGCCT");
        var result = DnaStore.Sequence.Types.Sequence.HammingDistance(a, b);
        Assert.AreEqual(7, result);
    }

    [TestMethod]
    public void OneIndexSimpleTest()
    {
        var a = new DnaStore.Sequence.Types.Sequence("GAGCCTACTAACGGGAT");
        var b = new Motif("GAG", 3);
        var result = a.MotifLocations(b);
        var expected = new long[] { 1 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void ZeroIndexSimpleTest()
    {
        var a = new DnaStore.Sequence.Types.Sequence("GAGCCTACTAACGGGAT");
        var b = new Motif("GAG", 3);
        var result = a.MotifLocations(b, true);
        var expected = new long[] { 0 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void OneIndexExample()
    {
        var a = new DnaStore.Sequence.Types.Sequence("GATATATGCATATACTT");
        var b = new Motif("ATAT", 4);
        var result = a.MotifLocations(b);
        var expected = new long[] { 2, 4, 10 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void AreSequenceEqualTest()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("abcde");
        var seq2 = new DnaStore.Sequence.Types.Sequence("abcde");
        Assert.IsTrue(DnaStore.Sequence.Types.Sequence.AreSequenceEqual(seq1, seq2));
    }

    [TestMethod]
    public void AreSequenceEqualTestDifferentSequence()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("abcde");
        var seq2 = new DnaStore.Sequence.Types.Sequence("abcdf");
        Assert.IsFalse(DnaStore.Sequence.Types.Sequence.AreSequenceEqual(seq1, seq2));
    }

    [TestMethod]
    public void AreSequenceEqualTestDifferentLength()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("abcde");
        var seq2 = new DnaStore.Sequence.Types.Sequence("abcdeg");
        Assert.IsFalse(DnaStore.Sequence.Types.Sequence.AreSequenceEqual(seq1, seq2));
    }

    [TestMethod]
    public void RemoveIntronsTestBeginning()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("abcde");
        var output = seq1.RemoveIntrons(new List<DnaStore.Sequence.Types.Sequence> { new("a") });
        Assert.AreEqual("bcde", output.ToString());
    }

    [TestMethod]
    public void RemoveIntronsTestEnd()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("abcde");
        var output = seq1.RemoveIntrons(new List<DnaStore.Sequence.Types.Sequence> { new("e") });
        Assert.AreEqual("abcd", output.ToString());
    }

    [TestMethod]
    public void RemoveIntronsTestNull()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("abcde");
        Assert.ThrowsExactly<ArgumentNullException>(() => seq1.RemoveIntrons(null));
    }

    [TestMethod]
    public void FindFirstPossibleSubSequenceTest()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("ACGTACGTGACG");
        var subseq = new DnaStore.Sequence.Types.Sequence("GTA");

        var result = seq1.FindFirstPossibleSubSequence(subseq);
        var expected = new List<int> { 3, 4, 5 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void OverlapTest()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("ACGTACGTGACG");
        var seq2 = new DnaStore.Sequence.Types.Sequence("GTGACGCATTTG");
        Assert.AreEqual(6, DnaStore.Sequence.Types.Sequence.CalculateOverlap(seq1, seq2));
        Assert.AreEqual(0, DnaStore.Sequence.Types.Sequence.CalculateOverlap(seq2, seq1));
    }

    [TestMethod]
    public void InvalidTypeThrows()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("ACGTACGTGACG");
        Assert.ThrowsExactly<ArgumentException>(() => seq1.CompareTo("something"));
    }

    [TestMethod]
    public void CompareToEqual()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("ABCD");
        var seq2 = new DnaStore.Sequence.Types.Sequence("ABCD");
        var seq3 = new DnaStore.Sequence.Types.Sequence("ABC");
        var seq4 = new DnaStore.Sequence.Types.Sequence("ABCE");
        Assert.AreEqual(0, seq1.CompareTo(seq2));
        Assert.AreEqual(1, seq1.CompareTo(seq3));
        Assert.AreEqual(-1, seq1.CompareTo(seq4));
    }

    [TestMethod]
    public void EnumerableChar()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("ABCD");
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
        var seq1 = new DnaStore.Sequence.Types.Sequence("AAAAAAAAT");
        Assert.IsTrue(seq1.ContainsString("TT", 1));
    }

    [TestMethod]
    public void KmerEnumerator()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("AAAAAAAA");
        var counter = 0;
        foreach (var something in seq1.GetKmerEnumerator(2)) counter++;

        Assert.AreEqual(7, counter);
    }

    [TestMethod]
    public void RandomKmerGenerator()
    {
        var seq1 = new DnaStore.Sequence.Types.Sequence("FEZLWBDYZGJQFSMZAJTADAYAXTNXODMV");
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
        List<DnaStore.Sequence.Types.Sequence> sequences = new()
        {
            new DnaStore.Sequence.Types.Sequence("TTACCTTAAC"),
            new DnaStore.Sequence.Types.Sequence("GATATCTGTC"),
            new DnaStore.Sequence.Types.Sequence("ACGGCGTTCG"),
            new DnaStore.Sequence.Types.Sequence("CCCTAAAGAG"),
            new DnaStore.Sequence.Types.Sequence("CGTCAGAGGT")
        };
        Assert.AreEqual(5, DnaStore.Sequence.Types.Sequence.DistancePatternAndString("AAA", sequences));
    }

    [TestMethod]
    public void InvalidEditDistance()
    {
        var seq1 = new DnaSequence("AAA");
        var seq2 = new RnaSequence("UUU");
        Assert.ThrowsExactly<ArgumentException>(() => seq1.EditDistance(seq2));
    }
}