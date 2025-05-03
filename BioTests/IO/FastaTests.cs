using Bio.IO;
using Bio.Sequence;

namespace BioTests.IO;

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
        "{\"Name\":\"some Name\",\"RawSequence\":\"aaccttg\",\"BasePairDictionary\":{\"Count\":7},\"Length\":0,\"GCContent\":0.42857142857142855,\"ContentType\":1}";

    // TODO: we should update this to be a guid
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../BioTests/Sequence/TestData/crab1.fasta");

    [TestMethod]
    public void FastaConstructor()
    {
        var someFasta = new Fasta(SomeName, SomeIllegitimateDNASequence);
        Assert.IsNotNull(someFasta);
        Assert.AreEqual(SomeName, someFasta.Name);
        Assert.AreEqual(SomeIllegitimateDNASequence, someFasta.RawSequence);
    }

    [TestMethod]
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
        Assert.IsTrue(Bio.Math.Helpers.DoublesEqualWithinRange(someFasta.GCContent, 0.4285));
    }
}