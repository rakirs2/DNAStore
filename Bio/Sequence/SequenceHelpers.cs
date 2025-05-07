using System.Text;

namespace Bio.Sequence;

public class SequenceHelpers
{
    // TODO: might be worth using a different string comparator if perf ever becomes an issue.
    public static bool IsKnownRNADifferentiator(char c)
    {
        return DistinctRNAMarkers.Contains(char.ToUpperInvariant(c));
    }

    public static bool IsKnownProteinDifferentiator(char c)
    {
        return DistinctProteinMarkers.Contains(char.ToUpperInvariant(c));
    }

    public static bool IsValidRNA(char c)
    {
        return AllRNAMarkers.Contains(char.ToUpperInvariant(c));
    }

    public static bool IsValidDNA(char c)
    {
        return AllRNAMarkers.Contains(char.ToUpperInvariant(c));
    }

    // Maybe this belongs on the codon class
    public static string ConvertStringToProtein(string input)
    {
        if (input.Length % 3 != 0) throw new InvalidDataException("String must have length mod 3");

        var convertedRNA = new StringBuilder();
        var i = 0;
        var hitStop = false;
        while (i < input.Length && !hitStop)
        {
            var temp = RNAToProteinConverter(input.Substring(i, 3));
            if (temp.Length == 1)
            {
                convertedRNA.Append(temp);
                i += 3;
            }
            else
            {
                hitStop = true;
            }
        }

        return convertedRNA.ToString();
    }

    public static string RNAToProteinConverter(string codon)
    {
        if (RNAToProteinCode.TryGetValue(codon, out var value))
            return value;

        throw new InvalidDataException("Value does not exist");
    }

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
    private static readonly HashSet<char> DistinctRNAMarkers = new() { 'U' };
    private static readonly HashSet<char> AllRNAMarkers = new() { 'U', 'A', 'C', 'G', 'N' };
    private static readonly HashSet<char> AllDNAMarkers = new() { 'T', 'A', 'C', 'G', 'N' };

    private static readonly Dictionary<string, string> RNAToProteinCode = new()
    {
        { "UUU", "F" },
        { "CUU", "L" },
        { "AUU", "I" },
        { "GUU", "V" },
        { "UUC", "F" },
        { "CUC", "L" },
        { "AUC", "I" },
        { "GUC", "V" },
        { "UUA", "L" },
        { "CUA", "L" },
        { "AUA", "I" },
        { "GUA", "V" },
        { "UUG", "L" },
        { "CUG", "L" },
        { "AUG", "M" },
        { "GUG", "V" },
        { "UCU", "S" },
        { "CCU", "P" },
        { "ACU", "T" },
        { "GCU", "A" },
        { "UCC", "S" },
        { "CCC", "P" },
        { "ACC", "T" },
        { "GCC", "A" },
        { "UCA", "S" },
        { "CCA", "P" },
        { "ACA", "T" },
        { "GCA", "A" },
        { "UCG", "S" },
        { "CCG", "P" },
        { "ACG", "T" },
        { "GCG", "A" },
        { "UAU", "Y" },
        { "CAU", "H" },
        { "AAU", "N" },
        { "GAU", "D" },
        { "UAC", "Y" },
        { "CAC", "H" },
        { "AAC", "N" },
        { "GAC", "D" },
        { "UAA", "Stop" },
        { "CAA", "Q" },
        { "AAA", "K" },
        { "GAA", "E" },
        { "UAG", "Stop" },
        { "CAG", "Q" },
        { "AAG", "K" },
        { "GAG", "E" },
        { "UGU", "C" },
        { "CGU", "R" },
        { "AGU", "S" },
        { "GGU", "G" },
        { "UGC", "C" },
        { "CGC", "R" },
        { "AGC", "S" },
        { "GGC", "G" },
        { "UGA", "Stop" },
        { "CGA", "R" },
        { "AGA", "R" },
        { "GGA", "G" },
        { "UGG", "W" },
        { "CGG", "R" },
        { "AGG", "R" },
        { "GGG", "G" }
    };

    private static readonly HashSet<char>
        DistinctProteinMarkers = new() { 'E', 'F', 'I', 'L', 'P', 'Q', 'Z', 'X', '*' };
}