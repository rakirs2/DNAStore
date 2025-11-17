namespace BaseTests.Algorithms;

[TestClass]
public class GreedyTest
{
    [TestMethod]
    public void MinCoins()
    {
        var coins = new int[6] { 1, 5, 10, 20, 25, 50 };
        Assert.AreEqual(2, Greedy.LeastCoins(coins, 40));
    }

    [TestMethod]
    public void MinCoinsActual()
    {
        var coins = new[] { 1, 3, 5, 11 };
        Assert.AreEqual(1798, Greedy.LeastCoins(coins, 19762));
    }

    [TestMethod]
    public void ImpossibleValue()
    {
        var coins = new[] { 2 };
        Assert.AreEqual(-1, Greedy.LeastCoins(coins, 3));
    }
}