namespace Bio.Sequence.Tests;

[TestClass]
public class FastaTests
{
    private const string SomeName = "some Name";
    private const string SomeIllegitimateSequence = "aaccttg";
    private const string JsonValue =
        "{\"Name\":\"some Name\",\"RawSequence\":\"aaccttg\",\"Frequencies\":{\"a\":2,\"c\":2,\"t\":2,\"g\":1},\"XorHash\":103}";
    private readonly Dictionary<char, int> _expectedSequenceCounts =
        new() { { 'a', 2 }, { 'c', 2 }, { 't', 2 }, { 'g', 1 } };
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
        "../../../../BioTests/Sequence/TestData/crab1.fasta");

    [TestMethod]
    public void FastaConstructor()
    {
        var someFasta = new Fasta(SomeName, SomeIllegitimateSequence);
        Assert.IsNotNull(someFasta);
        Assert.AreEqual(SomeName, someFasta.Name);
        Assert.AreEqual(SomeIllegitimateSequence, someFasta.RawSequence);
        Assert.AreEqual(103, someFasta.XorHash);
        Assert.IsTrue(someFasta.Frequencies.Count == _expectedSequenceCounts.Count &&
                      !someFasta.Frequencies.Except(_expectedSequenceCounts).Any());
    }

    [TestMethod]
    public void JsonOutput()
    {
        var someFasta = new Fasta(SomeName, SomeIllegitimateSequence);
        Assert.AreEqual(JsonValue, someFasta.ToJson());
    }

    [TestMethod]
    public void SaveAndRetrieveLocally()
    {
        var someFasta = new Fasta(SomeName, SomeIllegitimateSequence);
        someFasta.Save(_filePath);
        var newFasta = Fasta.GetFromFile(_filePath);
        Assert.AreEqual(someFasta, newFasta);
    }
    
    
}