using DNAStore.Sequences.Analysis.Types;
using DNAStore.Sequences.IO;

namespace BaseTests.Sequences.Analysis.Types;

[TestClass]
public class SimpleProfileMatrixTests
{
    private readonly string _expectedFrequencyMatrix =
        "A: 5 1 0 0 5 5 0 0\nC: 0 0 1 4 2 0 6 1\nG: 1 1 6 3 0 1 0 0\nT: 1 5 0 0 0 1 1 6\n";

    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../DNAStoreTests/Sequences/TestData/ProfileMatrixData.fasta");

    [TestMethod]
    public void ProfileMatrixTest()
    {
        var result = new SimpleProfileMatrix(FastaParser.Read(_filePath));
        Assert.AreEqual(7, result.QuantityAnalyzed);
        Assert.AreEqual(8, result.LengthOfSequences);
        Assert.AreEqual(_expectedFrequencyMatrix, result.FrequencyMatrix());
    }

    [TestMethod]
    public void SimpleProfileMatrixNullInput()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => new SimpleProfileMatrix(null));
    }

    [TestMethod]
    public void SimpleProfileMatrixNoInput()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new SimpleProfileMatrix(new List<Fasta>()));
    }
}