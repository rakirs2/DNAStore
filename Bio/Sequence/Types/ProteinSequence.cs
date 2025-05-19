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
            foreach (var character in RawSequence) output += Reference.MolecularWeightsDictionary[character];
            return output;
        }
    }

    public int NumberOfPossibleRNA(int modulo = (int)1e6)
    {
        int result = 1;
        foreach (var protein in RawSequence)
        {
            result *= SequenceHelpers.NumberOfPossibleProteins(protein.ToString());
        }
        // finally, we need to account for the stop
        result *= SequenceHelpers.NumberOfPossibleProteins("Stop");
        return result % modulo;
    }
}