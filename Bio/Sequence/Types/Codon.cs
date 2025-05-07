using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types
{
    public class Codon :ICodon
    {
        public Codon(string seq)
        {
            VerifyContent(seq);
            RawRna = seq.ToUpper();
        }

        private void VerifyContent(string seq)
        {
            if (seq.Length != 3)
            {
                throw new InvalidDataException("Codons can only have 3 characters");
            }
        }

        private string CleanString(string input)
        {
            return input.ToUpper();
        }

        public string RawRna { get; }

        public string ExpectedProtein => SequenceHelpers.RNAToProteinConverter(RawRna);
    }
}
