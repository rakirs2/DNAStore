using Bio.Analysis.Types;
using Bio.IO;

namespace BioTests.Analysis.Types
{
    [TestClass()]
    public class ProfileMatrixTests
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "../../../../BioTests/TestData/ProfileMatrixData.fasta");

        [TestMethod]
        public void ProfileMatrixTest()
        {
            var result = new ProfileMatrix(FastaParser.Read(_filePath));
            Assert.AreEqual(7, result.QuantityAnalyzed);
            Assert.AreEqual(8, result.LengthOfSequences);
        }
    }
}