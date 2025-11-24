using Base.Utils;

namespace Bio.Sequence.Types;

public class ProbabilityProfile
{
    private Dictionary<char, List<double>> _probabilities = new Dictionary<char, List<double>>(new CaseInsensitiveCharComparer());
    private int size;
    public ProbabilityProfile(List<List<double>> probabilities, string values)
    {
        size = probabilities[0].Count;
        for (int i = 0; i < values.Length; i++)
        {
            if (probabilities[i].Count != size)
            {
                throw new ArgumentException();
            }
            
            _probabilities[values[i]] = probabilities[i];
        }
    }

    public string HighestLikelihood(DnaSequence sequence)
    {
        var current = 0.0;
        var ret = sequence.Substring(0, size);
        foreach (var kmer in sequence.KmerCompositionUniqueString(size))
        {
            var temp = 1.0;
            for(int i = 0; i <  kmer.Length; i++)
            {
                temp*= _probabilities[kmer[i]][i];
            }

            if (temp > current)
            {
                current = temp;
                ret = kmer;
            }
        }
        
        return ret;
    }
}