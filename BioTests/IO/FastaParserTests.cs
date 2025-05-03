using Bio.IO;

namespace BioTests.IO;

[TestClass]
public class FastaParserTests
{
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../BioTests/Sequence/TestData/MultipleFasta.fasta");

    private readonly string _name1 = "Rosalind_6404";
    private readonly string _name2 = "Rosalind_5959";
    private readonly string _name3 = "Rosalind_0808";

    private readonly string _expectedSequence1 =
        "CCTGCGGAAGATCGGCACTAGAATAGCCAGAACCGTTTCTCTGAGGCTTCCGGCCTTCCCTCCCACTAATAATTCTGAGG";

    private readonly string _expectedSequence2 =
        "CCATCGGTAGCGCATCCTTAGTCCAATTAAGTCCCTATCCAGGCGCTCCGCCGAAGGTCTATATCCATTTGTCAGCAGACACGC";

    private readonly string _expectedSequence3 =
        "CCACCCTCGTGGTATGGCTAGGCATTCAGGAACCGGAGAACGCTTCAGACCAGCCCGGACTGGGAACCTGCGGGCAGTAGGTGGAAT";

    [TestMethod]
    public void ReadMultipleTest()
    {
        var result = FastaParser.Read(_filePath);
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Count);
    }

    [TestMethod]
    public void VerifyMultipleContents()
    {
        var result = FastaParser.Read(_filePath);
        IList<ExpectedFasta> expected = new List<ExpectedFasta>
        {
            new(_name1, _expectedSequence1),
            new(_name2, _expectedSequence2),
            new(_name3, _expectedSequence3)
        };

        Verify(result, expected);
    }

    private class ExpectedFasta
    {
        public ExpectedFasta(string name, string expectedSequence)
        {
            _name = name;
            _expectedSequence = expectedSequence;
        }

        public void Verify(IFasta input)
        {
            Assert.AreEqual(input.Name, _name);
            Assert.AreEqual(input.RawSequence, _expectedSequence);
        }

        private readonly string _name;
        private readonly string _expectedSequence;
    }

    private void Verify(IList<IFasta> input, IList<ExpectedFasta> expected)
    {
        for (var i = 0; i < input.Count; i++) expected[i].Verify(input[i]);
    }
}