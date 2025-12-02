using Bio.Analysis.Interfaces;
using Bio.Analysis.Types;
using Bio.Sequence.Interfaces;
using Bio.Sequence.Types;

namespace BioTests.Analysis.Types;

public class MotifProfile<T> : IMotifProfile where T : IStrictSequence
{
    public MotifProfile(List<Motif> motifs)
    {
        
    }

    public Dictionary<char, List<int>> MotifCounts { get; } = new Dictionary<char, List<int>>();
    public Dictionary<char, List<double>> MotifProbabilities { get; } = new Dictionary<char, List<double>>();
    public string Consensus { get; }
    public int Score()
    {
        throw new NotImplementedException();
    }
}