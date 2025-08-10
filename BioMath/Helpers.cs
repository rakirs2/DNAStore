using System.Numerics;

namespace BioMath;

/// <summary>
///     Most of these sequence Analysis are some combination of dynamic programming and string searches
///     The dynamic programming things can just be stored here.
/// </summary>
public static class Helpers
{
    /// <summary>
    ///     Probably bruteforceable but there are other problems later on with generation death and decay
    ///     that will need memory.
    ///     The population begins in the first month with a pair of newborn rabbits.
    ///     Rabbits reach reproductive age after one month.
    ///     In any given month, every rabbit of reproductive age mates with another rabbit of reproductive age.
    ///     Exactly one month after two rabbits mate, they produce one male and one female rabbit.
    ///     Rabbits never die or stop reproducing.
    /// </summary>
    public static BigInteger GenerationalGrowth(int numGenerations, int growthPerGeneration,
        int monthsToDie = int.MaxValue)
    {
        var totalNewRabbits = new BigInteger[numGenerations];
        BigInteger mature = 0;
        BigInteger dead = 0;
        totalNewRabbits[0] = 1;
        totalNewRabbits[1] = 0;


        // We can just keep track of the number of rabbits generated

        for (var i = 2; i < numGenerations; i++)
        {
            mature += totalNewRabbits[i - 2];

            totalNewRabbits[i] = mature * growthPerGeneration;
            if (i >= monthsToDie)
            {
                // maybe off by 1
                mature -= totalNewRabbits[i - monthsToDie];
                dead += totalNewRabbits[i - monthsToDie];
            }
        }

        BigInteger total = 0;
        foreach (var baby in totalNewRabbits) total += baby;

        return total - dead;
    }

    public static bool DoublesEqualWithinRange(double a, double b, double epsilon = 0.001)
    {
        return System.Math.Abs(a - b) <= epsilon;
    }
}