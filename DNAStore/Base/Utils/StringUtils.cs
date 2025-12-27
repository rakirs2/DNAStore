using System.Text;
using Ganss.Text;

namespace DnaStore.Base.Utils;

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
        var array = input.ToCharArray();
        (array[indexA], array[indexB]) = (array[indexB], array[indexA]);
        return new string(array);
    }

    public static string ForceJoinPerfectOrder(this List<string> sequences)
    {
        var sb = new StringBuilder();
        sb.Append(sequences[0][..^1]);
        foreach (var seq in sequences) sb.Append(seq[^1]);

        return sb.ToString();
    }

    /// <summary>
    ///     Returns the basic Levenshtein distance. Base DP programming algorithm for a lot of alignment
    ///     It's called edit distance. However, for the purposes of this, let's keep this separated
    ///     from all 'bio' project work and call it as necessary.
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
    public static int LevenshteinDistanceInternal(string a, string b, out int[,] d)
    {
        var m = a.Length;
        var n = b.Length;
        d = new int[m + 1, n + 1];

        if (string.IsNullOrEmpty(a)) return b?.Length ?? 0;

        if (string.IsNullOrEmpty(b)) return a?.Length ?? 0;


        for (var i = 1; i <= m; d[i, 0] = i++) ;
        for (var j = 1; j <= n; d[0, j] = j++) ;

        for (var i = 1; i <= m; i++)
        for (var j = 1; j <= n; j++)
        {
            var subCost = a[i - 1] == b[j - 1] ? 0 : 1;
            var delete = d[i - 1, j] + 1;
            var insert = d[i, j - 1] + 1;
            var substitution = d[i - 1, j - 1] + subCost;
            d[i, j] = MultiComparer.Min(delete, insert, substitution);
        }

        return d[m, n];
    }

    public static int LevenshteinDistance(string a, string b)
    {
        return LevenshteinDistanceInternal(a, b, out var _);
    }

    /// <summary>
    ///     Same as Levenshtein with a traceback to get the gapped strings
    ///     Guaranteed to return a set of the lowest score. This is not necessarily unique.
    /// </summary>
    /// <remarks>
    ///     It's worth remembering the first 'bad' idea for this. I wanted to greedily add - at every mismatch.
    ///     Why would that fail?
    ///     Trivial case, it actually works:
    ///     ABCDE    --> ABCDE---
    ///     ABCDEFGH --> ABCDEFGH
    ///     What if we mix these up?
    ///     ABCDEF
    ///     GABCDE
    ///     Distance 6
    ///     _ABCDEF
    ///     GABCDE_
    ///     Distance 2
    ///     TODO: for tomorrow. I messed this up somewhere
    ///     <remarks>
    ///         <param name="a"></param>
    ///         <param name="b"></param>
    ///         <param name="a_gapped"></param>
    ///         <param name="b_gapped"></param>
    ///         <returns></returns>
    public static int NeedlemanWunsch(string a, string b, out string a_gapped, out string b_gapped)
    {
        var retVal = LevenshteinDistanceInternal(a, b, out var d);
        var a_sb = new StringBuilder();
        var b_sb = new StringBuilder();
        int i = a.Length, j = b.Length;

        while (i > 0 && j > 0)
            // if both are positive, we are open to all 3 possibilities
            // but most importantly, we need to see if there was a perfect match accounting for the sub penalty
            if (i > 0 && j > 0 && d[i, j] == d[i - 1, j - 1] + (a[i - 1] == b[j - 1] ? 0 : 1))
            {
                a_sb.Insert(0, a[i - 1]);
                b_sb.Insert(0, b[j - 1]);
                i--;
                j--;
            }
            else if (i > 0 && d[i, j] == d[i - 1, j] + 1)
            {
                a_sb.Insert(0, a[i - 1]);
                b_sb.Insert(0, "-");
                i--;
            }
            else
            {
                a_sb.Insert(0, "-");
                b_sb.Insert(0, b[j - 1]);
                j--;
            }

        a_gapped = a_sb.ToString();
        b_gapped = b_sb.ToString();
        return retVal;
    }

    /// <summary>
    ///     Runs basic Aho Corasick test for a string search. Utilizes existing library
    /// </summary>
    /// <remarks>
    ///     TODO: a good exercise eventually to replace it with my own implementation.
    ///     TODO: might be worth flipping this so we maintain the automaton.
    /// </remarks>
    /// <param name="sequence"></param>
    /// <param name="stringsToSearch"></param>
    /// <returns></returns>
    public static List<WordMatch> AhoCorasickStringSearch(this string sequence, List<string> keywords)
    {
        var ahoCorasick = new AhoCorasick(keywords);
        return ahoCorasick.Search(sequence).ToList();
    }
}