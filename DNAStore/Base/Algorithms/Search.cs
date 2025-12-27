namespace Base.Algorithms;

public static class Search
{
    /// <summary>
    ///     Returns -1 if not found. Returns the 0 index starting point of all matches
    /// </summary>
    /// <remarks>
    ///     Base Algorithm basically from CLRS
    /// </remarks>
    /// <param name="text"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static int[] KnuthMorrisPratt(this string text, string pattern)
    {
        var failureArray = pattern.KMPFailureArray();
        var output = new List<int>();
        var q = 0;
        for (var i = 0; i < text.Length; i++)
        {
            while (q > 0 && pattern[q] != text[i]) q = failureArray[q];

            if (pattern[q] == text[i]) q++;

            if (q == pattern.Length)
            {
                output.Add(i - pattern.Length + 1);
                q = failureArray[q - 1];
            }
        }

        return output.ToArray();
    }

    /// <summary>
    ///     This is based off of the Compute-Prefix-Function from CLRS
    /// </summary>
    /// <remarks>
    ///     A couple of small modifications to make this 0-indexed
    /// </remarks>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static int[] KMPFailureArray(this string pattern)
    {
        var pi = new int[pattern.Length];
        var k = 0;

        for (var q = 1; q < pattern.Length; q++)
        {
            while (k > 0 && pattern[k] != pattern[q]) k = pi[k - 1];

            if (pattern[k] == pattern[q]) k++;
            pi[q] = k;
        }

        return pi;
    }

    public static List<List<int>> AhoCorasick(this string text, List<string> target)
    {
        return null;
    }
}