using Bio.Sequence;

namespace BioTests.Sequence;

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
        "{\"Name\":\"some Name\",\"RawSequence\":\"aaccttg\",\"Frequencies\":{\"a\":2,\"c\":2,\"t\":2,\"g\":1},\"XorHash\":103,\"ContentType\":1}";

    private readonly Dictionary<char, int> _expectedSequenceCounts =
        new() { { 'a', 2 }, { 'c', 2 }, { 't', 2 }, { 'g', 1 } };

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
        Assert.AreEqual(103, someFasta.XorHash);
        Assert.IsTrue(someFasta.Frequencies.Count == _expectedSequenceCounts.Count &&
                      !someFasta.Frequencies.Except(_expectedSequenceCounts).Any());
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
}