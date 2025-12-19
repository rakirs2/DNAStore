using System.Numerics;
using Bio.Sequences.Interfaces;

namespace Bio.Sequences.Types;

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
    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}