using Bio.Sequence.Types;

namespace BioTests.Sequence.Types;

[TestClass]
public class DeBrujinTest
{
    [TestMethod]
    public void Existence()
    {
        var graph = new DeBrujin();
        Assert.IsNotNull(graph);
    }

    [TestMethod]
    public void AddFromString()
    {
        var graph = new DeBrujin();
        graph.GenerateFromString("ACGT");
        Assert.AreEqual("(ACG, CGT)", graph.GetEdgeList());
    }

    [TestMethod]
    public void SimpleString()
    {
        var graph = new DeBrujin();
        graph.GenerateFromString("TGAT");
        Assert.AreEqual("(ATC, TCA)\n(TGA, GAT)", graph.GetEdgeList());
    }

    [TestMethod]
    public void GivenProblem()
    {
        var graph = new DeBrujin();
        graph.GenerateFromString("TGAT");
        graph.GenerateFromString("CATG");
        graph.GenerateFromString("TCAT");
        graph.GenerateFromString("ATGC");
        graph.GenerateFromString("CATC");
        graph.GenerateFromString("CATC");

        string? edgeList = graph.GetEdgeList();

        // NOTE: there's a slight ordering issue but it shouldn't matter with adjacency lists.
        Assert.AreEqual(
            "(ATC, TCA)\n(ATG, TGA)\n(ATG, TGC)\n(CAT, ATG)\n(CAT, ATC)\n(GAT, ATG)\n(GCA, CAT)\n(TCA, CAT)\n(TGA, GAT)",
            edgeList);
    }
}