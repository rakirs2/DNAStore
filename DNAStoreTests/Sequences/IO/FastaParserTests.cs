using DnaStore.Sequence.IO;

namespace BaseTests.Sequence.IO;

[TestClass]
public class FastaParserTests
{
    private readonly string _exampleMixedStringForParsing =
        ">sp|A2Z669|CSPLT_ORYSI CASP-like protein 5A2 OS=Oryza sativa subsp. indica OX=39946 GN=OsI_33147 PE=3 SV=1\nMRASRPVVHPVEAPPPAALAVAAAAVAVEAGVGAGGGAAAHGGENAQPRGVRMKDPPGAP\nGTPGGLGLRLVQAFFAAAALAVMASTDDFPSVSAFCYLVAAAILQCLWSLSLAVVDIYAL\nLVKRSLRNPQAVCIFTIGDGITGTLTLGAACASAGITVLIGNDLNICANNHCASFETATA\nMAFISWFALAPSCVLNFWSMASR\n";

    private readonly string _expectedSequence1 =
        "CCTGCGGAAGATCGGCACTAGAATAGCCAGAACCGTTTCTCTGAGGCTTCCGGCCTTCCCTCCCACTAATAATTCTGAGG";

    private readonly string _expectedSequence2 =
        "CCATCGGTAGCGCATCCTTAGTCCAATTAAGTCCCTATCCAGGCGCTCCGCCGAAGGTCTATATCCATTTGTCAGCAGACACGC";

    private readonly string _expectedSequence3 =
        "CCACCCTCGTGGTATGGCTAGGCATTCAGGAACCGGAGAACGCTTCAGACCAGCCCGGACTGGGAACCTGCGGGCAGTAGGTGGAAT";

    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../DNAStoreTests/Sequence/Sequences/TestData/MultipleFasta.fasta");

    private readonly string _mixedStringName =
        "sp|A2Z669|CSPLT_ORYSI CASP-like protein 5A2 OS=Oryza sativa subsp. indica OX=39946 GN=OsI_33147 PE=3 SV=1";

    private readonly string _mixedStringSequence =
        "MRASRPVVHPVEAPPPAALAVAAAAVAVEAGVGAGGGAAAHGGENAQPRGVRMKDPPGAPGTPGGLGLRLVQAFFAAAALAVMASTDDFPSVSAFCYLVAAAILQCLWSLSLAVVDIYALLVKRSLRNPQAVCIFTIGDGITGTLTLGAACASAGITVLIGNDLNICANNHCASFETATAMAFISWFALAPSCVLNFWSMASR";

    private readonly string _name1 = "Rosalind_6404";
    private readonly string _name2 = "Rosalind_5959";
    private readonly string _name3 = "Rosalind_0808";

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

    [TestMethod]
    public void DeserializeRawStringTest()
    {
        var result = FastaParser.DeserializeRawString(_exampleMixedStringForParsing);
        var expectedFasta = new ExpectedFasta(_mixedStringName, _mixedStringSequence);
        expectedFasta.Verify(result);
    }

    private void Verify(IList<Fasta> input, IList<ExpectedFasta> expected)
    {
        for (var i = 0; i < input.Count; i++) expected[i].Verify(input[i]);
    }

    private class ExpectedFasta
    {
        private readonly string _expectedSequence;

        private readonly string _name;

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
    }
}