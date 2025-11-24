using System.Collections;
using System.Text;
using Base.DataStructures;
using Bio.Analysis.Types;
using Bio.IO;
using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

/// <summary>
///     Base class for any sequence. This is the main driver for all types of analysis where the program does not
///     know what type of string we are analyzing.
/// </summary>
public class AnySequence : ISequence, IComparable, IEnumerable<char>
{
    public BasePairDictionary Counts = new();

    public AnySequence(string rawSequence, string? name = null)
    {
        Name = name;
        ConstructionLogic(rawSequence);
    }

    public AnySequence(Fasta fasta)
    {
        Name = fasta.Name;
        ConstructionLogic(fasta.RawSequence);
    }

    public string RawSequence { get; set; }

    public int CompareTo(object? obj)
    {
        if (obj is AnySequence other)
            return RawSequence.CompareTo(other.RawSequence);

        throw new ArgumentException("Object is not a sequence");
    }

    public IEnumerator<char> GetEnumerator()
    {
        return RawSequence.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // TODO: this needs to be seriously thought through.
    public IEnumerable<string> GetKmerEnumerator(int k)
    {
        for (var i = 0; i < Length - k + 1; i++)
        {
            yield return RawSequence.Substring(k, i);
        }
    }
    
    public long Length => RawSequence.Length;

    public string? Name { get; }

    /// <summary>
    /// </summary>
    /// <param name="motif"></param>
    /// <param name="isZeroIndex"></param>
    /// <returns></returns>
    public long[] MotifLocations(Motif motif, bool isZeroIndex = false)
    {
        var modifier = isZeroIndex ? 0 : 1;
        var output = new List<long>();
        for (var i = 0; i < Length - motif.ExpectedLength; i++)
            if (motif.IsMatchStrict(RawSequence.Substring(i, motif.ExpectedLength)))
                output.Add(i + modifier);

        return output.ToArray();
    }

    public AnySequence RemoveIntrons(List<AnySequence> introns)
    {
        if (introns == null) throw new ArgumentNullException();
        var trie = new Trie();
        foreach (var intron in introns) trie.AddWord(intron.RawSequence);

        var outputString = new StringBuilder();

        for (var i = 0; i < Length; i++)
        {
            var isValid = true;
            for (var j = 0; j < trie.MaxStringLength; j++)
            {
                if (i + j > Length - 1) break;

                if (trie.Search(RawSequence.Substring(i, j + 1)))
                {
                    i += j;
                    isValid = false;
                }
            }

            if (isValid) outputString.Append(RawSequence[i]);
        }

        return new AnySequence(outputString.ToString());
    }

    public List<int> FindFirstPossibleSubSequence(AnySequence subsequence, bool isZeroIndex = false)
    {
        if (subsequence == null || subsequence.Length == 0 || Length < subsequence.Length)
            return new List<int>();

        var modifier = isZeroIndex ? 0 : 1;
        var indices = new List<int>();
        var i = 0;
        var j = 0;
        while (i < Length && j < subsequence.Length)
        {
            if (RawSequence[i].Equals(subsequence[j]))
            {
                indices.Add(i + modifier);
                j++;
            }

            i++;
        }

        return indices;
    }

    public bool ContainsString(string stringToMatch, int distance)
    {
        for (var i = 0; i < Length - stringToMatch.Length + 1; i++)
            if (HammingDistance(RawSequence.Substring(i, stringToMatch.Length), stringToMatch) <= distance)
                return true;

        return false;
    }

    // Overloading the addition operator (+)
    public static AnySequence operator +(AnySequence p1, AnySequence p2)
    {
        return new AnySequence(p1.RawSequence + p2.RawSequence);
    }

    public override bool Equals(object obj)
    {
        if (obj is AnySequence other) return RawSequence.Equals(other.RawSequence);

        return false;
    }

    public override string ToString()
    {
        return RawSequence;
    }
    
    /// <summary>
    ///     Returns the hamming distance, the difference between any string at any given point.
    ///     Hamming distance requires both sequences to be the same length.
    /// </summary>
    /// <remarks>
    ///     This has some potential for scaling. What if both sequences are 20 gb long -- we can't exactly store that in memory
    ///     Also, hamming distance to hash difference seems intriguing if nothing else
    /// </remarks>
    public static int HammingDistance(AnySequence a, AnySequence b)
    {
        if (a.Length != b.Length) throw new InvalidDataException("Lengths must match");

        var result = 0;
        for (var i = 0; i < a.Length; i++)
            if (a[i] != b[i])
                result++;

        return result;
    }

    public static int HammingDistance(string a, string b)
    {
        if (a.Length != b.Length) throw new InvalidDataException("Lengths must match");

        var result = 0;
        for (var i = 0; i < a.Length; i++)
            if (a[i] != b[i])
                result++;

        return result;
    }

    // TODO: there still needs to be a determination made if this should or should not be case-sensitive
    public static bool AreSequenceEqual(AnySequence a, AnySequence b)
    {
        return a.RawSequence.Equals(b.RawSequence);
    }

    /// <summary>
    ///     This is a pretty simple cleanup method that is implemented in each child class.
    /// </summary>
    /// <remarks>
    ///     We know that some classes have slight differences. We want to be as strict as possible when
    ///     creating classes because biology data is inherently a mess.
    /// </remarks>
    /// <param name="bp"></param>
    /// <returns></returns>
    protected virtual bool IsValid(char bp)
    {
        return true;
    }

    private void ConstructionLogic(string rawSequence)
    {
        RawSequence = rawSequence;

        foreach (var basePair in rawSequence)
            // TODO: virtual member call in constructor is an issue? why?
            // Ah it's a design flaw on my part -- what's a better way to do this
            // abstract,

            if (IsValid(basePair))
                Counts.Add(basePair);
            else
                throw new Exception();
    }


    public override int GetHashCode()
    {
        return RawSequence.GetHashCode();
    }

    public static int CalculateOverlap(AnySequence s1, AnySequence s2)
    {
        var maxOverlap = 0;
        for (var i = 1; i <= Math.Min(s1.Length, s2.Length); i++)
            if (s1.Sequence.EndsWith(s2.Substring(0, i)))
                maxOverlap = i;

        return maxOverlap;
    }

    public static AnySequence LongestCommonSubsequence(AnySequence s1, AnySequence s2)
    {
        return new AnySequence(AlignmentMatrix.LongestCommonSubSequence(s1.RawSequence, s2.RawSequence));
    }

    #region String Accessors

    public char this[int index] => RawSequence[index];

    public string Sequence => RawSequence;
    public string this[Range range] => RawSequence[range];

    public string Substring(int i, int kmerLength)
    {
        return RawSequence.Substring(i, kmerLength);
    }

    #endregion
}