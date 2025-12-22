using Base.Algorithms;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace BaseTests.Algorithms;

[TestClass]
public class SearchTest
{
    [TestMethod]
    public void KMPFailureArrayCLRSExample()
    {
        int[] expected = new[] { 0,0,1,2,3,0,1};
        string sequence = "ababaca";
        Assert.IsTrue(sequence.KMPFailureArray().SequenceEqual(expected));
    }
    
    [TestMethod]
    public void KMPFailureArrayDnaVersion()
    {
        int[] expected = new[] { 0,0,0,1,2,0,0,0,0,0,0,1,2,1,2,3,4,5,3,0,0};
        string sequence = "CAGCATGGTATCACAGCAGAG";
        Assert.IsTrue(sequence.KMPFailureArray().SequenceEqual(expected));
    }

    [TestMethod]
    public void SimpleSearch()
    {
        int[] expected = new[] { 0, 13,16};
        string sequence = "CAGCATGGTATCACAGCAGAG";
        string pattern = "CAG";
        Assert.IsTrue(sequence.KnuthMorrisPratt(pattern).SequenceEqual(expected));
    }
}