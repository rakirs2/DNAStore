using System.Text.RegularExpressions;
using Bio.Analysis.Interfaces;

namespace Bio.Analysis.Types;

public class Motif : IMotif
{
    public Motif(string motif, int expectedLength = 0, string name = "")
    {
        Name = name;
        InputMotif = motif;
        ExpectedLength = expectedLength;
    }
    
    public int ExpectedLength { get; }
    public virtual bool IsMatch(string input)
    {
        return InputMotif.Equals(input);
    }

    public char this[int index] => InputMotif[index];
    public string this[Range range] =>InputMotif[range];
    public int Length =>InputMotif.Length;

    public virtual bool IsMatchStrict(string input)
    {
        return InputMotif.Equals(input);
    }
    
    public string Name { get; }
    public string InputMotif { get; }
}