using System.Text;
using Base.Utils;
using DnaStore.Base.Utils;

namespace Bio.Sequences;

public class SequenceHelpers
{
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
    private static readonly HashSet<char> DistinctRNAMarkers = new(CaseInsensitiveCharComparer.Shared) { 'U' };

    private static readonly HashSet<char> AllRNAMarkers = new(CaseInsensitiveCharComparer.Shared)
        { 'U', 'A', 'C', 'G', 'N' };

    private static readonly HashSet<char> AllDNAMarkers = new(CaseInsensitiveCharComparer.Shared)
        { 'T', 'A', 'C', 'G', 'N' };

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

    public static readonly Dictionary<string, List<string>> ProteinCodesToRNA = new()
    {
        { "W", ["UGG"] },
        { "M", ["AUG"] },
        { "F", ["UUU", "UUC"] },
        { "Y", ["UAU", "UAC"] },
        { "H", ["CAU", "CAC"] },
        { "N", ["AAU", "AAC"] },
        { "D", ["GAU", "GAC"] },
        { "Q", ["CAA", "CAG"] },
        { "K", ["AAA", "AAG"] },
        { "E", ["GAA", "GAG"] },
        { "C", ["UGU", "UGC"] },
        { "I", ["AUU", "AUC", "AUA"] },
        { "V", ["GUU", "GUC", "GUA", "GUG"] },
        { "P", ["CCU", "CCC", "CCA", "CCG"] },
        { "T", ["ACU", "ACC", "ACA", "ACG"] },
        { "A", ["GCU", "GCC", "GCA", "GCG"] },
        { "G", ["GGU", "GGC", "GGA", "GGG"] },
        { "R", ["CGU", "CGC", "CGA", "AGA", "CGG", "AGG"] },
        { "S", ["UCU", "UCC", "UCA", "UCG", "AGU", "AGC"] },
        { "L", ["CUU", "CUC", "UUA", "CUA", "UUG", "CUG"] },
        { "Stop", ["UAA", "UAG", "UGA"] }
    };

    public static Dictionary<string, string> DNAToProteinCode = new()
    {
        { "TTT", "F" }, { "TTC", "F" },
        { "TTA", "L" }, { "TTG", "L" }, { "CTT", "L" }, { "CTC", "L" }, { "CTA", "L" }, { "CTG", "L" },
        { "ATT", "I" }, { "ATC", "I" }, { "ATA", "I" },
        { "ATG", "M" }, // Start codon
        { "GTT", "V" }, { "GTC", "V" }, { "GTA", "V" }, { "GTG", "V" },
        { "TCT", "S" }, { "TCC", "S" }, { "TCA", "S" }, { "TCG", "S" }, { "AGT", "S" }, { "AGC", "S" },
        { "CCT", "P" }, { "CCC", "P" }, { "CCA", "P" }, { "CCG", "P" },
        { "ACT", "T" }, { "ACC", "T" }, { "ACA", "T" }, { "ACG", "T" },
        { "GCT", "A" }, { "GCC", "A" }, { "GCA", "A" }, { "GCG", "A" },
        { "TAT", "Y" }, { "TAC", "Y" },
        { "CAT", "H" }, { "CAC", "H" },
        { "CAA", "Q" }, { "CAG", "Q" },
        { "AAT", "N" }, { "AAC", "N" },
        { "AAA", "K" }, { "AAG", "K" },
        { "GAT", "D" }, { "GAC", "D" },
        { "GAA", "E" }, { "GAG", "E" },
        { "TGT", "C" }, { "TGC", "C" },
        { "TGG", "W" },
        { "CGT", "R" }, { "CGC", "R" }, { "CGA", "R" }, { "CGG", "R" }, { "AGA", "R" }, { "AGG", "R" },
        { "GGT", "G" }, { "GGC", "G" }, { "GGA", "G" }, { "GGG", "G" },
        { "TAA", "Stop" }, { "TAG", "Stop" }, { "TGA", "Stop" } // Stop codons
    };

    private static readonly HashSet<char>
        DistinctProteinMarkers = new(CaseInsensitiveCharComparer.Shared)
            { 'E', 'F', 'I', 'L', 'P', 'Q', 'Z', 'X', '*' };

    // TODO: might be worth using a different string comparator if perf ever becomes an issue.
    public static bool IsKnownRNADifferentiator(char c)
    {
        return DistinctRNAMarkers.Contains(c);
    }

    public static bool IsKnownProteinDifferentiator(char c)
    {
        return DistinctProteinMarkers.Contains(c);
    }

    public static bool IsValidRNA(char c)
    {
        return AllRNAMarkers.Contains(c);
    }

    public static bool IsValidDNA(char c)
    {
        return AllRNAMarkers.Contains(c);
    }


    public static List<string> AllPossibleKmersList(string kmers)
    {
        var output = new List<string>();
        GeneratePerms(kmers.Length, kmers, ref output);
        return output;
    }

    private static void GeneratePerms(int currentLength, string inputString, ref List<string> arrayToAddTo)
    {
        if (currentLength == 1)
            arrayToAddTo.Add(inputString);
        else
            for (var i = 0; i < currentLength; i++)
            {
                GeneratePerms(currentLength - 1, inputString, ref arrayToAddTo);
                if (currentLength % 2 == 0)
                    inputString = StringUtils.SwapIndex(inputString, currentLength - 1, i);
                else
                    inputString = StringUtils.SwapIndex(inputString, 0, currentLength - 1);
            }
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
            string? temp = RNAToProteinConverter(input.Substring(i, 3));
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
        if (RNAToProteinCode.TryGetValue(codon, out string? value))
            return value;

        throw new InvalidDataException("Value does not exist");
    }

    public static int NumberOfPossibleProteins(string protein)
    {
        return ProteinCodesToRNA[protein].Count;
    }
}