using DNAStore.BioMath;
using DNAStore.Sequences.Exceptions;
using DNAStore.Sequences.Types;

namespace BaseTests.Sequences.Sequences.Types;

[TestClass]
public class ProteinSequenceTest
{
    [TestMethod]
    public void ProteinSequenceBasicTest()
    {
        var protein = new ProteinSequence("SKADYEK");
        Assert.IsTrue(Helpers.DoublesEqualWithinRange(821.392, protein.MolecularWeight));
    }

    [TestMethod]
    public void NumberOfPossibleRNATest()
    {
        var protein = new ProteinSequence("MA");
        Assert.AreEqual(12, protein.NumberOfPossibleRNA());
    }

    [TestMethod]
    public void GivenPrefixWeights()
    {
        var weights = new[]
        {
            3524.8542,
            3710.9335,
            3841.974,
            3970.0326,
            4057.0646
        };

        Assert.AreEqual("WMQS", ProteinSequence.CalculateFromPrefixWeights(weights).RawSequence);
    }

    [TestMethod]
    public void GivenShortSeq()
    {
        var weights = new[]
        {
            3524.8542
        };

        Assert.ThrowsExactly<ArgumentException>(() => ProteinSequence.CalculateFromPrefixWeights(weights).RawSequence);
    }

    [TestMethod]
    public void InferredProteinWeight()
    {
        var weights = new[]
        {
            610.391039105,
            738.485999105,
            766.492149105,
            863.544909105,
            867.528589105,
            992.587499105,
            995.623549105,
            1120.6824591,
            1124.6661391,
            1221.7188991,
            1249.7250491,
            1377.8200091
        };

        Assert.AreEqual("KEKEP", ProteinSequence.InferFromPrefixWeights(1988.21104821, weights));
    }

    [TestMethod]
    public void InvalidMassSpecWeight()
    {
        var weights = new[]
        {
            610.391039105,
            738.485999105,
            766.492149105,
            863.544909105,
            867.528589105,
            992.587499105,
            995.623549105,
            1120.6824591,
            1124.6661391,
            1221.7188991,
            1249.7250491,
            1377.8200091
        };

        Assert.ThrowsExactly<MassSpecExceptions.InvalidMassException>(() =>
            ProteinSequence.InferFromPrefixWeights(0.00, weights));
    }
}