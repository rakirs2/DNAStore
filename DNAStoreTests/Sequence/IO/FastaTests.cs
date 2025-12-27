using DnaStore.Math;
using DnaStore.Sequence.IO;
using DnaStore.Sequence.Types;

namespace BaseTests.Sequence.IO;

[TestClass]
public class FastaTests
{
    private const string SomeName = "some Name";

    private const string SomeIllegitimateDNASequence = "aaccttg";
    private const string SomeIllegitimateRNASequence = "aaccuug";

    private const string SomeIllegitimateProteinSequence =
        "mhntvwcstv hlklpdmyss nsslngnnlt fssnspfcsf esnstsskdd hnihssfplt";

    // TODO: figure out something more robust with this
    private const string JsonValue =
        "{\"Name\":\"some Name\",\"RawSequence\":\"aaccttg\",\"BasePairDictionary\":{\"Count\":7,\"HighestFrequencyBasePair\":\"a\",\"HighestFrequencyBasePairCount\":2},\"Length\":7,\"GCContent\":0.42857142857142855,\"ContentType\":1}";

    // TODO: we should update this to be a guid
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../DNAStoreTests/Sequence/Sequences/TestData/crab1.fasta");

    private readonly string _multipleFastaPath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../DNAStoreTests/Sequence/Sequences/TestData/MultipleFasta.fasta");

    [TestMethod]
    public void FastaConstructor()
    {
        var someFasta = new Fasta(SomeName, SomeIllegitimateDNASequence);
        Assert.IsNotNull(someFasta);
        Assert.AreEqual(SomeName, someFasta.Name);
        Assert.AreEqual(SomeIllegitimateDNASequence, someFasta.RawSequence);
    }

    [TestMethod]
    [Ignore]
    public void JsonOutput()
    {
        var someFasta = new Fasta(SomeName, SomeIllegitimateDNASequence);
        Assert.AreEqual(JsonValue, someFasta.ToJson());
    }

    [TestMethod]
    public void SaveAndRetrieveLocally()
    {
        var someFasta = new Fasta(SomeName, SomeIllegitimateDNASequence);
        someFasta.Save(_filePath);
        var newFasta = Fasta.GetFromFile(_filePath);
        Assert.AreEqual(someFasta, newFasta);
    }

    [TestMethod]
    public void OpenDNAFasta()
    {
        var someFasta = new Fasta(SomeName, SomeIllegitimateDNASequence);
        Assert.AreEqual(ContentType.DNA, someFasta.ContentType);
    }

    [TestMethod]
    public void OpenRNAFasta()
    {
        var someFasta = new Fasta(SomeName, SomeIllegitimateRNASequence);
        Assert.AreEqual(ContentType.RNA, someFasta.ContentType);
    }

    [TestMethod]
    public void OpenProteinFasta()
    {
        var someFasta = new Fasta(SomeName, SomeIllegitimateProteinSequence);
        Assert.AreEqual(ContentType.Protein, someFasta.ContentType);
    }

    [TestMethod]
    public void FastaGCContent()
    {
        var someFasta = new Fasta(SomeName, SomeIllegitimateDNASequence);
        Assert.IsTrue(Helpers.DoublesEqualWithinRange(new DnaSequence(someFasta.RawSequence).GCRatio(), 0.4285));
    }

    [TestMethod]
    public void GetMaxGCContentTest()
    {
        var fastas = FastaParser.Read(_multipleFastaPath);
        var highest = Fasta.GetMaxGCContent(fastas);
        Assert.IsTrue(Helpers.DoublesEqualWithinRange(60.919540, new DnaSequence(highest.RawSequence).GCRatio() * 100));
    }
}