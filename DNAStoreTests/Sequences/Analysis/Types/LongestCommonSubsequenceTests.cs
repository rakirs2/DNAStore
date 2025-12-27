using DNAStore.Sequences.Analysis.Types;
using DNAStore.Sequences.IO;

namespace BaseTests.Sequences.Analysis.Types;

[TestClass]
public class LongestCommonSubsequenceTests
{
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../DNAStoreTests/Sequences/TestData/LongestSubsequence.fasta");

    [TestMethod]
    public void LongestCommonSubsequenceTest()
    {
        var result = new LongestCommonSubsequence(FastaParser.Read(_filePath));
        Assert.AreEqual("AC", result.GetAnyLongest().ToString());
    }
}