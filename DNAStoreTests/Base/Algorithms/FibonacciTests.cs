using DnaStore.Base.Algorithms;

namespace BaseTests.Base.Algorithms;

[TestClass]
public class FibonacciTests
{
    [TestMethod]
    public void GetNthBaseCases()
    {
        Assert.AreEqual(1, Fibonacci.GetNth(1));
        Assert.AreEqual(1, Fibonacci.GetNth(2));
    }

    [TestMethod]
    public void GetNthTestOutOfRange()
    {
        Assert.ThrowsExactly<InvalidDataException>(() => Fibonacci.GetNth(-1));
    }

    [TestMethod]
    public void GetNthKnown()
    {
        Assert.AreEqual(8, Fibonacci.GetNth(6));
    }
}