namespace BaseTests.Algorithms;

public class Greedy
{
    public static int LeastCoins(int[] coinValues, int target)
    {
        var coinTracker = new int[target + 1];
        Array.Fill(coinTracker, target + 1);
        coinTracker[0] = 0;
        for (var i = 1; i <= target; i++)
            foreach (int coin in coinValues)
                if (coin <= i)
                    coinTracker[i] = Math.Min(coinTracker[i], coinTracker[i - coin] + 1);

        return coinTracker[target] > target ? -1 : coinTracker[target];
    }
}