using System.Numerics;
using System.Text;
using Bio;
using DNAStore.Sequences.Exceptions;
using DNAStore.Sequences.Types.Interfaces;

namespace DNAStore.Sequences.Types;

public class ProteinSequence : Sequence, IProtein
{
    public ProteinSequence(string rawSequence) : base(rawSequence)
    {
    }

    public double MolecularWeight
    {
        get
        {
            double output = 0;
            foreach (var character in ToString()) output += Reference.MonoisotopicMassTable[character];
            return output;
        }
    }

    // TODO: there's some modular arithmetic fixes to be had here
    public int NumberOfPossibleRNA(int modulo = (int)1e6)
    {
        BigInteger result = 1;
        foreach (var protein in ToString()) result *= SequenceHelpers.NumberOfPossibleProteins(protein.ToString());
        // finally, we need to account for the stop
        result *= SequenceHelpers.NumberOfPossibleProteins("Stop");
        var modulo2 = new BigInteger(modulo);
        var output = result % modulo2;
        return (int)output;
    }

    public override bool Equals(object obj)
    {
        if (obj is ProteinSequence other) return ToString().Equals(other.ToString());

        return false;
    }

    // TODO: implement this. There are a couple of known hashes.
    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    public static ProteinSequence CalculateFromPrefixWeights(double[] spectrum)
    {
        if (spectrum.Length <= 1)
            throw new ArgumentException("A single protein sequence realistically should never be used with this");
        var protein = "";
        for (var i = 0; i < spectrum.Length - 1; i++)
        {
            var diff = spectrum[i + 1] - spectrum[i];

            // Search for best fit by mass
            var match = Reference.MonoisotopicMassTable
                .OrderBy(kvp => System.Math.Abs(kvp.Value - diff))
                .First().Key;

            protein += match;
        }

        return new ProteinSequence(protein);
    }

    /// <summary>
    ///     Returns the best fitting protein string within a given list of ion weights
    /// </summary
    /// <remarks>
    ///     I like this problem. Introduces a few concepts I remember very lightly from mass spec.
    ///     Fun times those. But the beauty of this problem is that, like much of spectroscopy, we're forced to be deductive
    ///     So what can we do?
    ///     Obviously, we start with the largest weight or the total weight.
    ///     Then, sort the remaining spectrum and now we have n^2 possible comparisons.
    ///     However, most of these will be invalid as we can only compare 1 AA at a time.
    ///     If the difference between 2 is within the tolerance, we can go ahead and add it.
    ///     We have to be greedy. At least with my current understanding of the problem. So we build a sequence starting with
    ///     the lowest weight (which is effectively a black box) and add on the next proteins iteratively.
    ///     For the love of everything, use the right units.
    /// </remarks>
    /// <param name="totalWeight"></param>
    /// <param name="spectrum"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    public static string InferFromPrefixWeights(double totalWeight, double[] spectrum, double tolerance = .001)
    {
        var ions = spectrum.OrderBy(x => x).ToArray();
        var n = (ions.Length - 2) / 2;
        var result = new StringBuilder();
        var currentMass = ions[0];
        if (ions.Any(ion => ion > totalWeight))
            throw new MassSpecExceptions.InvalidMassException(
                "Molecular weight of total must be strictly greater than any smaller sum");

        for (var i = 0; i < n; i++)
            foreach (var aminoAcid in Reference.MonoisotopicMassTable)
            {
                var targetMass = currentMass + aminoAcid.Value;
                var nextIon = ions.FirstOrDefault(m => System.Math.Abs(m - targetMass) < tolerance);

                if (nextIon == 0) continue;
                result.Append(aminoAcid.Key);
                // We want the cleanest data possible. It would be possible to use the runnning total.
                // But it's better to use the given data.
                currentMass = nextIon;
                break;
            }

        return result.ToString();
    }
}