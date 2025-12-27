using DnaStore.Sequence.Analysis.Types;
using DnaStore.Sequence.IO;

namespace BaseTests.Sequence.Analysis.Types;

[TestClass]
public class LongestCommonSubsequenceTests
{
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../DNAStoreTests/Sequence/TestData/LongestSubsequence.fasta");

    [TestMethod]
    public void LongestCommonSubsequenceTest()
    {
        var result = new LongestCommonSubsequence(FastaParser.Read(_filePath));
        Assert.AreEqual("AC", result.GetAnyLongest().ToString());
    }
}