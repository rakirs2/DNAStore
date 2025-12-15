using System.Text;

namespace Base.Utils;

public static class StringUtils
{
    /// <summary>
    ///     Swaps the characters of the passed in string
    /// </summary>
    /// <returns>
    ///     a new string object with the indices swapped
    /// </returns>
    /// <param name="input"></param>
    /// <param name="indexA"></param>
    /// <param name="indexB"></param>
    public static string SwapIndex(string input, int indexA, int indexB)
    {
        if (input == null || indexA > input.Length - 1 || indexB > input.Length - 1 || indexA < 0 || indexB < 0)
            throw new ArgumentException();

        // Swaps characters in a string.
        char[]? array = input.ToCharArray();
        (array[indexA], array[indexB]) = (array[indexB], array[indexA]);
        return new string(array);
    }

    public static string ForceJoinPerfectOrder(this List<string> sequences)
    {
        var sb = new StringBuilder();
        sb.Append(sequences[0][..^1]);
        foreach(var seq in sequences)
        {
            sb.Append(seq[^1]);
        }
        
        return sb.ToString();
    }

    /// <summary>
    /// Returns the basic Levenshtein distance. Base DP programming algorithm for a lot of alignment
    /// It's called edit distance in computational biology. However, for the purposes of this, let's keep this separated
    /// from all 'bio' project work and call it as necessary.
    /// </summary>
    /// <remarks>
    ///     A way to remember this is almost that the columns and rows are compared
    ///     We need a way to instantiate the values. What does that look like?
    ///     Well, in the top row and left most column, both are effectively comparing to an empty string. Either add or
    ///     delete to the lengths of the string
    /// </remarks>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int LevenshteinDistance(string a, string b)
    {
        if (string.IsNullOrEmpty(a))
        {
            return b?.Length ?? 0;
        }
        
        if (string.IsNullOrEmpty(b))
        {
            return a?.Length ?? 0;
        }
        
        var m = a.Length;
        var n = b.Length;
        var d = new int[m +1, n+1];
        
        for (int i = 1; i <= m; d[i, 0] = i++) ;
        for (int j = 1; j <= n; d[0, j] = j++) ;

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                int subCost = a[i-1] == b[j-1] ? 0 : 1;
                var delete = d[i-1, j] + 1;
                var insert = d[i, j - 1] + 1;
                var substitution = d[i - 1, j - 1] + subCost;
                d[i, j] = MultiComparer.Min(delete, insert , substitution );
            }
        }
        
        return d[m, n];
    }
}