using DNAStore.Base.DataStructures;
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

    [TestMethod]
    public void SimpleOverlapTests()
    {
        var temp = new OverlapGraph();
        temp.Insert("ABCD");
        temp.Insert("BCDA");
        var actual = temp.ReadToReadEdgeList();
        var expected = new DirectedGraph<string> { };
        expected.Insert("ABCD", "BCDA");
        
        // TODO: assert
    }
    
    [TestMethod]
    public void GivenOverlapTests()
    {
        var temp = new OverlapGraph();
        temp.Insert("ATGCG");
        temp.Insert("GCATG");
        temp.Insert("CATGC");
        temp.Insert("AGGCA");
        temp.Insert("GGCAT");
        var actual = temp.ReadToReadEdgeList().AllEdges();
        var expected = new List<string>
        {
            "AGGCA -> GGCAT",
            "CATGC -> ATGCG",
            "GCATG -> CATGC",
            "GGCAT -> GCATG"
        };
        Assert.IsTrue(actual.SequenceEqual(expected));
    }
}