using System.Numerics;
using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

public class ProteinSequence : AnySequence, IProtein
{
    public ProteinSequence(string rawSequence) : base(rawSequence)
    {
    }

    public double MolecularWeight
    {
        get
        {
            double output = 0;
            foreach (var character in ToString()) output += Reference.MolecularWeightsDictionary[character];
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
    // Ideally, tihs would be passed in at construction?
    // TODO: perf vs spooky hashes?
    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    // TODO: this needs clarity/implementation
    public static HashSet<char> GetValidInputs => throw new NotImplementedException();
}