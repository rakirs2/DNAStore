using Bio.Analysis.Types;
using Bio.Sequence.Types;
using BioTests.Analysis.Types;
using JetBrains.Annotations;

namespace BioTests.Analysis.Types;

[TestClass]
public class MotifProfileTest
{

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MotifProfileInputJaggedMotifs()
    {
        var input = new List<Motif>();
        input.Add(new Motif("ACGT"));
        input.Add(new Motif("ACGTA"));
        _ = new MotifProfile<DnaSequence>(input);
    }
    
    [TestMethod]
    public void MotifProfileGiven()
    {
        var inputs = new List<Motif>
        {
            new ("GGCGTTCAGGCA"),
            new ("AAGAATCAGTCA"),
            new ("CAAGGAGTTCGC"),
            new ("CACGTCAATCAC"),
            new("CAATAATATTCG")
        };
     
        var output = new MotifProfile<DnaSequence>(inputs);
        Assert.AreEqual("CACGTTCATTCC", output.Consensus);
    }
    
}