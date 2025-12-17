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
    public void HammingMismatchedLengths()
    {
        var a = new AnySequence("a");
        var b = new AnySequence("ab");
        Assert.ThrowsExactly<InvalidDataException>(() => AnySequence.HammingDistance(a, b));
    }

    [TestMethod]
    public void HammingDistanceCaseSensitive()
    {
        var a = new AnySequence("ac");
        var b = new AnySequence("ab");
        int result = AnySequence.HammingDistance(a, b);
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void HammingDistanceRealistic()
    {
        var a = new AnySequence("GAGCCTACTAACGGGAT");
        var b = new AnySequence("CATCGTAATGACGGCCT");
        int result = AnySequence.HammingDistance(a, b);
        Assert.AreEqual(7, result);
    }

    [TestMethod]
    public void OneIndexSimpleTest()
    {
        var a = new AnySequence("GAGCCTACTAACGGGAT");
        var b = new Motif("GAG", 3);
        long[] result = a.MotifLocations(b);
        var expected = new long[] { 1 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void ZeroIndexSimpleTest()
    {
        var a = new AnySequence("GAGCCTACTAACGGGAT");
        var b = new Motif("GAG", 3);
        long[] result = a.MotifLocations(b, true);
        var expected = new long[] { 0 };
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [TestMethod]
    public void OneIndexExample()
    {
        var a = new AnySequence("GATATATGCATATACTT");
        var b = new Motif("ATAT", 4);
        long[] result = a.MotifLocations(b);
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

    [TestMethod]
    public void OverlapTest()
    {
        var seq1 = new AnySequence("ACGTACGTGACG");
        var seq2 = new AnySequence("GTGACGCATTTG");
        Assert.AreEqual(6, AnySequence.CalculateOverlap(seq1, seq2));
        Assert.AreEqual(0, AnySequence.CalculateOverlap(seq2, seq1));
    }

    [TestMethod]
    public void InvalidTypeThrows()
    {
        var seq1 = new AnySequence("ACGTACGTGACG");
        Assert.ThrowsExactly<ArgumentException>(() => seq1.CompareTo("something"));
    }

    [TestMethod]
    public void CompareToEqual()
    {
        var seq1 = new AnySequence("ABCD");
        var seq2 = new AnySequence("ABCD");
        var seq3 = new AnySequence("ABC");
        var seq4 = new AnySequence("ABCE");
        Assert.AreEqual(0, seq1.CompareTo(seq2));
        Assert.AreEqual(1, seq1.CompareTo(seq3));
        Assert.AreEqual(-1, seq1.CompareTo(seq4));
    }

    [TestMethod]
    public void EnumerableChar()
    {
        var seq1 = new AnySequence("ABCD");
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
        var seq1 = new AnySequence("AAAAAAAAT");
        Assert.IsTrue(seq1.ContainsString("TT", 1));
    }

    [TestMethod]
    public void KmerEnumerator()
    {
        var seq1 = new AnySequence("AAAAAAAA");
        var counter = 0;
        foreach (string something in seq1.GetKmerEnumerator(2)) counter++;

        Assert.AreEqual(7, counter);
    }

    [TestMethod]
    public void RandomKmerGenerator()
    {
        var seq1 = new AnySequence("FEZLWBDYZGJQFSMZAJTADAYAXTNXODMV");
        string s1 = seq1.GetRandomKmer(5);
        string s2 = seq1.GetRandomKmer(5);
        string s3 = seq1.GetRandomKmer(5);

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
        List<AnySequence> sequences = new()
        {
            new("TTACCTTAAC"),
            new("GATATCTGTC"),
            new("ACGGCGTTCG"),
            new("CCCTAAAGAG"),
            new("CGTCAGAGGT")
        };
        Assert.AreEqual(5, AnySequence.DistancePatternAndString("AAA", sequences));
    }

    [TestMethod]
    public void InvalidEditDistance()
    {
        DnaSequence seq1 = new DnaSequence("AAA");
        RnaSequence seq2 = new RnaSequence("UUU");
        Assert.ThrowsExactly<ArgumentException>(() =>seq1.EditDistance(seq2));
    }
}