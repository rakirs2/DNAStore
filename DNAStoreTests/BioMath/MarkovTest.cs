using DNAStore.BioMath;
using JetBrains.Annotations;

namespace DNAStoreTests.BioMath;

[TestClass]
[TestSubject(typeof(Markov))]
public class MarkovTest
{

    [TestMethod]
    public void CalculateHiddenPathProbability()
    {
        var output = Markov.HiddenPathProbability("FFFBBBBBBFFF", new char[] { 'F', 'B' },
            new double[2, 2] { { .9, .1 }, { .1, .9 } });
    }
    
    [TestMethod]
    public void CalculateHiddenPathProbabilityGiven()
    {
        var pi = "AABBBAABABAAAABBBBAABBABABBBAABBAAAABABAABBABABBAB";
        var states = new char[] { 'A', 'B' };
        var transition = new double[2, 2] { { .194, .806 }, { .273, .727 } };
        var output = Markov.HiddenPathProbability(pi, states, transition);
        Assert.AreEqual(5.01732865318E-19, output, 10E-20);
    }
}