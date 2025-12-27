using DNAStore.Base.DataStructures;

namespace BaseTests.Base.DataStructures;

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


    [TestMethod]
    public void HighestFrequencyTest()
    {
        var basePairDictionary = new BasePairDictionary();
        basePairDictionary.Add('c');
        basePairDictionary.Add('c');
        basePairDictionary.Add('d');
        Assert.AreEqual('c', basePairDictionary.HighestFrequencyBasePair);
        Assert.AreEqual(2, basePairDictionary.HighestFrequencyBasePairCount);
        basePairDictionary.Add('d');
        basePairDictionary.Add('d');
        Assert.AreEqual('d', basePairDictionary.HighestFrequencyBasePair);
        Assert.AreEqual(3, basePairDictionary.HighestFrequencyBasePairCount);
    }

    [TestMethod]
    public void OverrideOfToString()
    {
        var basePairDictionary = new BasePairDictionary();
        basePairDictionary.Add('c');
        basePairDictionary.Add('c');
        basePairDictionary.Add('d');
        Assert.AreEqual("{\"c\":2,\"d\":1}", basePairDictionary.ToString());
    }
}