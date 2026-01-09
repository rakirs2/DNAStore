using DNAStore.BioMath;

namespace DNAStoreTests.BioMath;

[TestClass]
public class MarkovTest
{

    [TestMethod]
    public void CalculateHiddenPathProbability()
    {
        var pi = "FFFBBBBBBFFF";
        var states = new char[] { 'F', 'B' };
        var transition =new double[2, 2] { { .9, .1 }, { .1, .9 } };
        var output = Markov.HiddenPathProbability(pi, states,
            transition);
        Assert.AreEqual(0.001937102445, output, 1e-4);
    }
    
    [TestMethod]
    public void CalculateHiddenPathInvalidStates()
    {
        var pi = "FFFBBBBBBFFF";
        var states = new char[] { 'F', 'F' };
        var transition =new double[2, 2] { { .9, .1 }, { .1, .9 } };
        Assert.ThrowsExactly<InvalidDataException>(()=>Markov.HiddenPathProbability(pi, states,
            transition));
    }
    
    [TestMethod]
    public void CalculateHiddenPathInvalidTransitionMatrixSize()
    {
        var pi = "FFFBBBBBBFFF";
        var states = new char[] { 'F', 'F' };
        var transition =new double[1,1] { { .9} };
        Assert.ThrowsExactly<InvalidDataException>(()=>Markov.HiddenPathProbability(pi, states,
            transition));
    }
    
    [TestMethod]
    public void CalculateHiddenPathProbabilityGiven()
    {
        var pi = "AABBBAABABAAAABBBBAABBABABBBAABBAAAABABAABBABABBAB";
        var states = new char[] { 'A', 'B' };
        var transition = new double[2, 2] { { .194, .806 }, { .273, .727 } };
        var output = Markov.HiddenPathProbability(pi, states, transition);
        Assert.AreEqual(5.01732865318E-19, output, 1E-20);
    }

    [TestMethod]
    public void CalculateOutcomeProbabilityGivenHiddenPath()
    {
        var outcome = "xxyzyxzzxzxyxyyzxxzzxxyyxxyxyzzxxyzyzxzxxyxyyzxxzx";
        var sigma = new char[] { 'x', 'y', 'z' };
        var hiddenPath = "BBBAAABABABBBBBBAAAAAABAAAABABABBBBBABAABABABABBBB";
        var states = new char[] { 'A', 'B' };
        var emission = new double[2, 3] { { .612, .314, .074 }, { .346, .317, .336 } };
        var output = Markov.PathOutcomeProbability(outcome, sigma, hiddenPath, states, emission);
        Assert.AreEqual(1.93157070893e-28, output, 1E-31);
    }

    [TestMethod]
    public void ViterbiAlgorithm()
    {
        var x = "xyxzzxyxyy";
        var sigma = new char[] { 'x', 'y', 'z' };
        var states = new char[] { 'A', 'B' };
        var transmission = new double[2, 2] { { .641, .359 }, { .729, .271} };
        var emission = new double[2, 3] { {  0.117 ,  0.691,   0.192    }, {0.097   ,0.42    ,0.483 } };
        var hiddenPath = Markov.ViterbiAlgorithm(x, sigma, states, transmission, emission);
        Assert.AreEqual("AAABBAAAAA", hiddenPath);
    }
}