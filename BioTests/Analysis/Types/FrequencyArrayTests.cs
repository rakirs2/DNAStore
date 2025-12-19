using Bio.Analysis.Types;

namespace BioTests.Analysis.Types;

[TestClass]
public class FrequencyArrayTests
{
    [TestMethod]
    public void GetFrequencyArrayInLexicographicOrderTest()
    {
        var sequence = new Bio.Sequences.Types.Sequence("ACGCGGCTCTGAAA");
        var frequencyArray = new FrequencyArray(sequence);
        Assert.IsTrue(frequencyArray.GetFrequencyArrayInLexicographicOrder("ACGT", 2)
            .SequenceEqual(new[] { 2, 1, 0, 0, 0, 0, 2, 2, 1, 2, 1, 0, 0, 1, 1, 0 }));
    }
}