namespace Base.Algorithms.Tests;

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
    [ExpectedException(typeof(InvalidDataException))]
    public void GetNthTestOutOfRange()
    {
        Fibonacci.GetNth(-1);
    }

    [TestMethod]
    public void GetNthKnown()
    {
        Assert.AreEqual(8, Fibonacci.GetNth(6));
    }
}