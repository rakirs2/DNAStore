namespace Base.Utils;

public class AlphabetGenerator
{
    public static IEnumerable<char> GetAllAlphabetChars()
    {
        // Add all uppercase letters 'A' through 'Z'
        for (char c = 'A'; c <= 'Z'; c++)
        {
            yield return c;
        }

        // Add all lowercase letters 'a' through 'z'
        for (char c = 'a'; c <= 'z'; c++)
        {
            yield return c;
        }
    }
}