using Bio.Analysis.Interfaces;
using Bio.Math;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;
public class MismatchKmerCounter : IMismatchKmerCounter
{
    public MismatchKmerCounter(int kmerLength, AnySequence sequence, int tolerance)
    {
        KmerLength = kmerLength;
        Tolerance = tolerance;
        _sequence = sequence;
    }


    public HashSet<string> HighestFrequencyKmers { get; private set; }
    public int KmerLength { get; }
    public int Tolerance { get; }

    private AnySequence _sequence;

    public HashSet<string> GetKmers(string matchString)
    {
        var listPossible = Probability.GenerateAllKmers(matchString, KmerLength);
        foreach (var kmer in listPossible)
        {
            MismatchDictionaryTracker[kmer] = 0;
        }

        var currentHighest = 0;
        for (int i = 0; i <= _sequence.RawSequence.Length - KmerLength; i++)
        {
            foreach (var key in MismatchDictionaryTracker.Keys)
            {
                if (AnySequence.HammingDistance(_sequence.RawSequence.Substring(i, KmerLength), key) <= Tolerance)
                {
                    MismatchDictionaryTracker[key] += 1;
                    if (MismatchDictionaryTracker[key] > currentHighest)
                    {
                        HighestFrequencyKmers = new HashSet<string>() { key };
                        currentHighest = MismatchDictionaryTracker[key];
                    }
                    else if (MismatchDictionaryTracker[key] == currentHighest)
                    {
                        HighestFrequencyKmers.Add(key);
                    }
                }
            }
        }

        return HighestFrequencyKmers;
    }

    private Dictionary<string, int> MismatchDictionaryTracker = new();
}
