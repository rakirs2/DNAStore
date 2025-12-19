using Bio.Analysis.Interfaces;
using Bio.Sequence.Types;
using BioMath;

namespace Bio.Analysis.Types;

public class MismatchKmerCounter : IMismatchKmerCounter
{
    private readonly Sequence.Types.Sequence _sequence;

    private readonly Dictionary<string, int> MismatchDictionaryTracker = new();

    public MismatchKmerCounter(int kmerLength, Sequence.Types.Sequence sequence, int tolerance)
    {
        KmerLength = kmerLength;
        Tolerance = tolerance;
        _sequence = sequence;
    }

    public HashSet<string> HighestFrequencyKmers { get; private set; }
    public int KmerLength { get; }
    public int Tolerance { get; }

    public HashSet<string> GetKmers(string matchString, bool checkComplement = false)
    {
        var listPossible = Probability.GenerateAllKmers(matchString, KmerLength);
        foreach (string? kmer in listPossible) MismatchDictionaryTracker[kmer] = 0;

        var currentHighest = 0;
        for (var i = 0; i <= _sequence.Length - KmerLength; i++)
            foreach (string? key in MismatchDictionaryTracker.Keys)
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
        string? stringToCheck = key;
        if (checkComplement) stringToCheck = new DnaSequence(key).GetReverseComplement().ToString();

        if (Sequence.Types.Sequence.HammingDistance(_sequence.Substring(i, KmerLength), stringToCheck) <= Tolerance)
        {
            // TODO: this really can get refactored
            MismatchDictionaryTracker[key] += 1;
            if (MismatchDictionaryTracker[key] > currentHighest)
            {
                HighestFrequencyKmers = new HashSet<string> { key };
                currentHighest = MismatchDictionaryTracker[key];
            }
            else if (MismatchDictionaryTracker[key] == currentHighest)
            {
                HighestFrequencyKmers.Add(key);
            }
        }

        return currentHighest;
    }
}