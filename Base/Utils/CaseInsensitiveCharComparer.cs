namespace Base.Utils;

public class CaseInsensitiveCharComparer : IEqualityComparer<char>

{
    private CaseInsensitiveCharComparer()
    {
        
    }

    public static CaseInsensitiveCharComparer Shared => new ();
    public bool Equals(char x, char y)
    {
        return char.ToUpperInvariant(x) == char.ToUpperInvariant(y);
    }

    public int GetHashCode(char x)
    {
        return char.ToUpperInvariant(x).GetHashCode();
    }
}