using Base.Interfaces;

using Bio.Math;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;
public class FrequencyArray : IFrequencyArray
{
    public FrequencyArray(Sequence.Types.Sequence sequence)
    {
        _sequence = sequence;
    }
    public List<int> GetFrequencyArrayInLexicographicOrder(string kmerValues, int kmerLength)
    {
        // TODO: eventually making this sorted might have some value
        List<string> allKmers = Probability.GenerateAllKmers(kmerValues, kmerLength);
        Dictionary<string, int> counter = new();
        foreach (var kmer in allKmers)
        {
            counter.Add(kmer, 0);
        }
        for (int i = 0; i < _sequence.RawSequence.Length - kmerLength + 1; i++)
        {
            counter[_sequence.RawSequence.Substring(i, kmerLength)] += 1;
        }

        var output = new List<int>();
        foreach (var kmer in allKmers)
        {
            output.Add(counter[kmer]);
        }

        return output;
    }

    private Sequence.Types.Sequence _sequence;

}
