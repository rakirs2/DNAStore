using Bio.IO;

namespace BioTests.IO;

[TestClass]
public class FastaListExtensionsTests
{
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../DNAStore.SequenceTests/TestData/DistanceMatrix.fasta");

    [TestMethod]
    public void DistanceMatrixTest()
    {
        var fastas = FastaParser.Read(_filePath);
        var output = fastas.GenerateDistanceMatrix();
        var expected = new List<List<double>>
        {
            new()
            {
                0.00000, 0.40000, 0.10000, 0.10000
            },
            new()
            {
                0.40000, 0.00000, 0.40000, 0.30000
            },
            new()
            {
                0.10000, 0.40000, 0.00000, 0.20000
            },
            new()
            {
                0.10000, 0.30000, 0.20000, 0.00000
            }
        };

        for (var i = 0; i < expected.Count; i++)
        for (var j = 0; j < expected.Count; j++)
            Assert.AreEqual(expected[i][j], output[i][j]);
    }
}