using Bio.Sequence;

namespace BaseTests.DataStructures
{
    [TestClass]
    public class BasePairDictionaryTests
    {
        [TestMethod]
        public void Instantiation()
        {
            var basePairDictionary = new BasePairDictionary();
            Assert.AreEqual(0, basePairDictionary.Count);
        }

        [TestMethod]
        public void Add()
        {
            var basePairDictionary = new BasePairDictionary();
            basePairDictionary.Add('c');
            Assert.AreEqual(1, basePairDictionary.Count);
            Assert.AreEqual(1, basePairDictionary.GetFrequency('c'));
            Assert.AreEqual(0, basePairDictionary.GetFrequency('1'));
        }

        [TestMethod]
        public void AddMultiple()
        {
            var basePairDictionary = new BasePairDictionary();
            basePairDictionary.Add('c');
            basePairDictionary.Add('c');
            basePairDictionary.Add('d');
            Assert.AreEqual(3, basePairDictionary.Count);
            Assert.AreEqual(2, basePairDictionary.GetFrequency('c'));
            Assert.AreEqual(1, basePairDictionary.GetFrequency('d'));
        }
    }
}