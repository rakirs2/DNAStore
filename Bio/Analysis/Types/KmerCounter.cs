using Bio.Analysis.Interfaces;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;

public class KmerCounter : IKmerCounter
{
    private static readonly Dictionary<string, int> Counts = new();

    public KmerCounter(AnySequence sequence, int kmerLength)
    {
        KmerLength = kmerLength;
        // ok, we need to populate the right values here
        for (var i = 0; i < sequence.RawSequence.Length - KmerLength + 1; i++)
        {
            var currentWord = sequence.RawSequence.Substring(i, KmerLength);
            if (Counts.ContainsKey(sequence.RawSequence.Substring(i, KmerLength)))
            {
                Counts[currentWord] += 1;
                if (Counts[currentWord] > CurrentHighestFrequency)
                {
                    CurrentHighestFrequency = Counts[currentWord];
                    HighestFrequencyKmers = new HashSet<string> { currentWord };
                }

                if (Counts[currentWord] == CurrentHighestFrequency) HighestFrequencyKmers.Add(currentWord);
            }
            else
            {
                Counts[currentWord] = 1;
            }
        }
    }

    public int CurrentHighestFrequency { get; }

    /// <summary>
    ///     This only gets the highest value. This is the bareminimum lightweight implementation
    ///     as sequences can theoretically be infinitely large.
    ///     TODO: add a heap implementation which can return all of the Kmers and their frequencies
    /// </summary>
    public HashSet<string> HighestFrequencyKmers { get; } = new();

    public int KmerLength { get; }
}