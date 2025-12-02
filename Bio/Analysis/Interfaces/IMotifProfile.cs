using Bio.Analysis.Types;

namespace Bio.Analysis.Interfaces;

public interface IMotifProfile
{
    public Dictionary<char, List<int>> MotifCounts { get;  }
    public Dictionary<char, List<double>> MotifProbabilities { get; }
    public string Consensus { get; }
    public int Score();
    public int Length { get; }
    public List<Motif> Motifs { get; }
}