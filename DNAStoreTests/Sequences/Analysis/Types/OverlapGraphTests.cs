using DNAStore.Sequences.Analysis.Types;
using DNAStore.Sequences.IO;

namespace DNAStoreTests.Sequences.Analysis.Types;

[TestClass]
public class OverlapGraphTests
{
    [TestMethod]
    public void SimpleConstruction()
    {
        Assert.IsNotNull(new OverlapGraph());
    }
    
    [TestMethod]
    public void AddingARead()
    {
        var temp = new OverlapGraph();
        temp.Insert("ABCD");
        
    }
}