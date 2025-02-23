namespace Bio.Sequence.Tests
{
    [TestClass]
    public class FastaTests
    {
        private const string SomeName = "some Name";
        private const string SomeIllegitimateSequence = "aaccttg";

        // TODO: I need a more robust test on this. Copying an pasting the string is not the solution
        private const string JsonValue =
            "{\"Name\":\"some Name\",\"RawSequence\":\"aaccttg\",\"Frequencies\":{\"a\":2,\"c\":2,\"t\":2,\"g\":1},\"Type\":0,\"XorHash\":103}";
        private readonly Dictionary<char, int> _expectedSequenceCounts = new Dictionary<char, int> { { 'a', 2 }, { 'c', 2 }, { 't', 2 }, { 'g', 1 } };

        private readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(),
            "../../../../BioTests/Sequence/TestData/crab1.fasta");

        [TestMethod]
        public void FastaConstructor()
        {
            var someFasta = new Fasta(SomeName, SomeIllegitimateSequence);
            Assert.IsNotNull(someFasta);
            Assert.AreEqual(someFasta.Name, SomeName);
            Assert.AreEqual(someFasta.RawSequence, SomeIllegitimateSequence);
            Assert.AreEqual(someFasta.XorHash, 103);
            Assert.IsTrue(someFasta.Frequencies.Count == _expectedSequenceCounts.Count && !someFasta.Frequencies.Except(_expectedSequenceCounts).Any());
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
            someFasta.Save(FilePath);
            var newFasta = Fasta.GetFromFile(FilePath);
            Assert.AreEqual(someFasta, newFasta);
        }

        [TestMethod]
        public void SplitAndVerify()
        {
            var someFasta = new Fasta(SomeName, SomeIllegitimateSequence);
            var output = someFasta.SplitInHalf();
            // TODO: might be worth adding a utility for this
            var rejoinedSequence = String.Join("", output);
            Assert.AreEqual(someFasta.RawSequence, rejoinedSequence);
        }
    }
}