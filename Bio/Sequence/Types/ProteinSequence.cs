using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types
{
    public class ProteinSequence: AnySequence, IProtein
    {
        public ProteinSequence(string rawSequence) : base(rawSequence)
        {
        }

        public double MolecularWeight
        {
            get
            {
                double output = 0;
                foreach (var character in RawSequence)
                {
                    output += Reference.MolecularWeightsDictionary[character];
                }
                return output;
            }
        }
    }
}
