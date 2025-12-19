using Bio.Sequences.Interfaces;

namespace Bio.Sequences.Types;

public class Codon : ICodon
{
    public Codon(string seq)
    {
        VerifyContent(seq);
        RawRna = seq.ToUpper();
    }

    public string RawRna { get; }

    public string ExpectedProtein => SequenceHelpers.RNAToProteinConverter(RawRna);

    private void VerifyContent(string seq)
    {
        if (seq.Length != 3) throw new InvalidDataException("Codons can only have 3 characters");
    }

    private string CleanString(string input)
    {
        return input.ToUpper();
    }
}