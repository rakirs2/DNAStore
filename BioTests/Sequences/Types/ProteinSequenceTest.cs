using Bio.Sequences.Types;
using BioMath;

namespace BioTests.Sequence.Types;

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
        var weights = new double[]
        {
            3524.8542,
            3710.9335,
            3841.974,
            3970.0326,
            4057.0646
        };
        
        Assert.AreEqual("WMQS",ProteinSequence.CalculateFromPrefixWeights(weights).RawSequence);
    }
    
    [TestMethod]
    public void GivenShortSeq()
    {
        var weights = new double[]
        {
            3524.8542
        };
        
        Assert.ThrowsExactly<ArgumentException>(()=>ProteinSequence.CalculateFromPrefixWeights(weights).RawSequence);
    }
}