using Bio.Analysis.Types;
using Bio.IO;

namespace BioTests.Analysis.Types;

[TestClass]
public class LongestCommonSubsequenceTests
{
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../SequenceTests/TestData/LongestSubsequence.fasta");

    [TestMethod]
    public void LongestCommonSubsequenceTest()
    {
        var result = new LongestCommonSubsequence(FastaParser.Read(_filePath));
        Assert.AreEqual("AC", result.GetAnyLongest().ToString());
    }
}