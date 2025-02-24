using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio.Sequence
{
    public class SequenceHelpers
    {
        public static bool IsKnownRNADifferentiator(char c) => RNAMarkers.Contains(char.ToUpperInvariant(c));

        public static bool IsKnownProteinDifferentiator(char c) => ProteinMarkers.Contains(char.ToUpperInvariant(c));

        /*
            RNA/DNA
            A --> adenosine           M --> A C (amino)
            C --> cytidine            S --> G C (strong)
            G --> guanine             W --> A T (weak)
            T --> thymidine           B --> G T C
            U --> uridine (RNA only)  D --> G A T
            R --> G A (purine)        H --> A C T
            Y --> T C (pyrimidine)    V --> G C A
            K --> G T (keto)          N --> A G C T (any)
                                      -  gap of indeterminate length
            The accepted amino acid codes are:
                A ALA alanine                         P PRO proline
                B ASX aspartate or asparagine         Q GLN glutamine
                C CYS cystine                         R ARG arginine
                D ASP aspartate                       S SER serine
                E GLU glutamate                       T THR threonine
                F PHE phenylalanine                   U     selenocysteine
                G GLY glycine                         V VAL valine
                H HIS histidine                       W TRP tryptophan
                I ILE isoleucine                      Y TYR tyrosine
                K LYS lysine                          Z GLX glutamate or glutamine
                L LEU leucine                         X     any
                M MET methionine                      *     translation stop
                N ASN asparagine                      -     gap of indeterminate length
        // Note, U Can be shared. So we need to check, "contains U"
         */
        // TODO: PotentialBottleneck perf bottleneck.
        private static readonly HashSet<char> RNAMarkers = new HashSet<char> { 'U' };
        private static readonly HashSet<char> ProteinMarkers = new HashSet<char> { 'E', 'F', 'I', 'L', 'P', 'Q', 'Z', 'X', '*' };
    }
}
