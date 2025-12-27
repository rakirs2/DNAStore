using DnaStore.Sequence.Analysis.Types;
using DnaStore.Sequence.IO;

namespace BaseTests.Sequence.Analysis.Types;

[TestClass]
public class OverlapGraphTests
{
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../DNAStoreTests/Sequence/TestData/OverlapFastas.fasta");

    [TestMethod]
    public void OverlapGraphTest()
    {
        var result = new OverlapGraph(FastaParser.Read(_filePath), 3);
        Assert.AreEqual(3, result.GetOverlaps().Count());
    }
}