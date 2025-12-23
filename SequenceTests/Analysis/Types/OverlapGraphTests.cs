using Bio.Analysis.Types;
using Bio.IO;

namespace BioTests.Analysis.Types;

[TestClass]
public class OverlapGraphTests
{
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../SequenceTests/TestData/OverlapFastas.fasta");

    [TestMethod]
    public void OverlapGraphTest()
    {
        var result = new OverlapGraph(FastaParser.Read(_filePath), 3);
        Assert.AreEqual(3, result.GetOverlaps().Count());
    }
}