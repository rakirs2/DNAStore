using Bio.IO;

namespace BioTests.Analysis.Types
{
    [TestClass()]
    public class ProfileMatrixTests
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "../../../../BioTests/TestData/ProfileMatrixData.fasta");

        [TestMethod()]
        public void ProfileMatrixTest()
        {
            var result = FastaParser.Read(_filePath);
        }

        [TestMethod()]
        public void GetProfileStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCleanOutputTest()
        {
            Assert.Fail();
        }
    }
}