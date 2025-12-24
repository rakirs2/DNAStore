using Bio.Analysis.Interfaces;
using Bio.Sequences.Types;
using BioMath;

namespace Bio.Analysis.Types;

public class FrequencyArray : IFrequencyArray
{
    private readonly Sequence _sequence;

    public FrequencyArray(Sequence sequence)
    {
        _sequence = sequence;
    }

    public List<int> GetFrequencyArrayInLexicographicOrder(string kmerValues, int kmerLength)
    {
        // TODO: eventually making this sorted might have some value
        var allKmers = Probability.GenerateAllKmers(kmerValues, kmerLength);
        Dictionary<string, int> counter = new();
        foreach (string? kmer in allKmers) counter.Add(kmer, 0);
        for (var i = 0; i < _sequence.Length - kmerLength + 1; i++)
            counter[_sequence.Substring(i, kmerLength)] += 1;

        var output = new List<int>();
        foreach (string? kmer in allKmers) output.Add(counter[kmer]);

        return output;
    }
}