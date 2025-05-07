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
    public static double PercentDominant(uint dominant, uint hetero, uint recessive)
    {
        var total = dominant + hetero + recessive;
        // Assume 2 genes, X and Y;

        // We want 1 - P(X == recessive AND Y == recessive);
        // 

        // P(X = recessive) = P(Recessive) + 1/2(P(Hetero))
        // P(Y = Recessive) = P(Recessive) + 1/2(P(Hetero))

        // Each person has 2 genes. Dominant as XX, hetero has Xx or xX and recessive has xx

        // To get dominant, you only need to know the probability to two recessive individuals can get together
        // and the probability that 2 hetero individuals get together
        return 1 - System.Math.Pow((double)recessive / total, 2) - System.Math.Pow((double)hetero / total, 2) / 4;
    }
}