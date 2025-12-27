using DNAStore.Sequences.Analysis.Types;
using DNAStore.Sequences.IO;

namespace DNAStoreTests.Sequences.Analysis.Types;

[TestClass]
public class OverlapGraphTests
{
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../DNAStoreTests/Sequences/TestData/OverlapFastas.fasta");

    [TestMethod]
    public void OverlapGraphTest()
    {
        var result = new OverlapGraph(FastaParser.Read(_filePath), 3);
        Assert.AreEqual(3, result.GetOverlaps().Count());
    }
}