using System.Diagnostics.Metrics;
using DNAStore.Base.DataStructures;

namespace DNAStoreTests.Base.DataStructures;

[TestClass]
public class AddOnlyCounterTest
{

    [TestMethod]
    public void SimpleAddition()
    {
        var counter = new AddOnlyCounter<string, int>();
        counter.Add("a");
        counter.Add("b");
        counter.Add("c");
        Assert.AreEqual( "a", counter.HighestFrequency);
        Assert.AreEqual( 3, counter.Count);
        counter.Add("c");
        Assert.AreEqual( "c", counter.HighestFrequency);
        Assert.AreEqual( 4, counter.Count);
    }
}