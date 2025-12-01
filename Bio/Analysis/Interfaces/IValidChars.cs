namespace Bio.Analysis.Interfaces;

public interface IValidChars
{
    public HashSet<char> ValidChars { get; }

    public bool IsValidChar(char c);
}