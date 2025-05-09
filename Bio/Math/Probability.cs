namespace Bio.Math;

// TODO: find a new namespace that's not math

public static class Probability
{
    /// <summary>
    /// Given a population of x dominant, y heterozygous and z recessive individuals,
    /// What is the percentage an offspring will have the dominant phenotype?
    /// </summary>
    /// <remarks>
    /// This is a raw Mendelian Genetics calculator</remarks>
    /// <returns></returns>
    public static double PercentDominant(uint k, uint m, uint n)
    {
        var total = k + m + n;
        var totalCombinations = 4 * nCr(total, 2);
        var dominant = 4 * nCr(k, 2) + 4 * k * m + 4 * k * n + 3 * nCr(m, 2) + 2 * m * n;

        return (double) dominant / totalCombinations;
    }

    public static long nCr(uint n, uint r)
    {
        return Factorial(n) / (Factorial(r) * Factorial(n - r));
    }

    public static long nPr(uint n, uint r)
    {
        // naive: return Factorial(n) / Factorial(n - r);
        return Factorial(n) / Factorial(n - r);
    }

    public static long Factorial(uint i)
    {
        if (i <= 1)
            return 1;
        return i * Factorial(i - 1);
    }
}