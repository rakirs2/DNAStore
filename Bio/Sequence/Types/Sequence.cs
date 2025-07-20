using Base.DataStructures;

using Bio.Analysis.Types;
using Bio.IO;
using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

/// <summary>
/// Base class for any sequence. This is the main driver for all types of analysis where the program does not
/// know what type of string we are analyzing.
///
/// TODO: this should eventually have 1 static creator which can take in a string and returns the implied typing
/// </summary>
public class Sequence : ISequence
{
    public long Length { get; set; }
    public string RawSequence { get; set; }
    public string? Name { get; }

    public override bool Equals(object obj)
    {
        if (obj is Sequence other)
        {
            return RawSequence == other.RawSequence;
        }

        return false;
    }

    public override string ToString()
    {
        return RawSequence;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="motif"></param>
    /// <param name="isZeroIndex"></param>
    /// <returns></returns>
    public long[] MotifLocations(Motif motif, bool isZeroIndex = false)
    {
        var modifier = isZeroIndex ? 0 : 1;
        var output = new List<long>();
        for (var i = 0; i < Length - motif.ExpectedLength; i++)
            // TODO: make everything an int?
            // TODO: do I need to make this a long implementation -- does that even help?
            if (motif.IsMatchStrict(RawSequence.Substring(i, motif.ExpectedLength)))
                output.Add(i + modifier);

        return output.ToArray();
    }


    // TODO: this should be cleaned up
    public Sequence(string rawSequence)
    {
        ConstructionLogic(rawSequence);
    }

    public Sequence(string name, string rawSequence)
    {
        Name = name;
        ConstructionLogic(rawSequence);
    }

    public Sequence(Fasta fasta)
    {
        Name = fasta.Name;
        ConstructionLogic(fasta.RawSequence);
    }

    public BasePairDictionary Counts = new();

    /// <summary>
    /// Returns the hamming distance, the difference between any string at any given point.
    /// Hamming distance requires both sequences to be the same length.
    /// </summary>
    /// <remarks>
    /// This has some potential for scaling. What if both sequences are 20 gb long -- we can't exactly store that in memory
    /// Also, hamming distance to hash difference seems intriguing if nothing else
    /// </remarks>
    public static long HammingDistance(Sequence a, Sequence b)
    {
        if (a.Length != b.Length) throw new InvalidDataException("Lengths must match");

        var result = 0;
        for (var i = 0; i < a.Length; i++)
            if (a.RawSequence[i] != b.RawSequence[i])
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
    public static bool AreSequenceEqual(Sequence a, Sequence b)
    {
        return a.RawSequence.Equals(b.RawSequence);
    }

    /// <summary>
    /// This is a pretty simple cleanup t
    /// </summary>
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

        Length = RawSequence.Length;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}