using Bio.Analysis.Interfaces;
using Bio.Math;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;

public class MismatchKmerCounter : IMismatchKmerCounter
{
    public MismatchKmerCounter(int kmerLength, Sequence.Types.Sequence sequence, int tolerance)
    {
        KmerLength = kmerLength;
        Tolerance = tolerance;
        _sequence = sequence;
    }

    public HashSet<string> HighestFrequencyKmers { get; private set; }
    public int KmerLength { get; }
    public int Tolerance { get; }

    private Sequence.Types.Sequence _sequence;

    public HashSet<string> GetKmers(string matchString, bool checkComplement = false)
    {
        var listPossible = Probability.GenerateAllKmers(matchString, KmerLength);
        foreach (var kmer in listPossible) MismatchDictionaryTracker[kmer] = 0;

        var currentHighest = 0;
        for (var i = 0; i <= _sequence.RawSequence.Length - KmerLength; i++)
            foreach (var key in MismatchDictionaryTracker.Keys)
            {
                currentHighest = AnalyzeKmer(i, key, currentHighest, false);
                // TODO: maybe a bad pattern, can be iterated
                if (checkComplement)
                    currentHighest = AnalyzeKmer(i, key, currentHighest, true);
            }

        return HighestFrequencyKmers;
    }

    private int AnalyzeKmer(int i, string key, int currentHighest, bool checkComplement)
    {
        var stringToCheck = key;
        if (checkComplement) stringToCheck = new DNASequence(key).ToReverseComplement().RawSequence;

        if (Sequence.Types.Sequence.HammingDistance(_sequence.RawSequence.Substring(i, KmerLength), stringToCheck) <= Tolerance)
        {
            // TODO: this really can get refactored
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

        return currentHighest;
    }

    private Dictionary<string, int> MismatchDictionaryTracker = new();
}