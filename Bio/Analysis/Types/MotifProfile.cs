using Bio.Analysis.Interfaces;
using Bio.Analysis.Types;
using Bio.Sequence.Interfaces;
using Bio.Sequence.Types;

namespace BioTests.Analysis.Types;

public class MotifProfile<T> : IMotifProfile where T : IStrictSequence
{
    public MotifProfile(List<Motif> motifs)
    {
        Length = motifs[0].Length;
        VerifyInputs(motifs);
        foreach (var validCharacter in T.ValidAlphabet)
        {
            var temp1 = new List<int>(Length);
            var temp2 = new List<double>(Length);
            for (int i = 0; i < Length; i++)
            {
                temp1.Add(0);
                temp2.Add(0.0);
            }
            MotifCounts.TryAdd(validCharacter, temp1);
            MotifProbabilities.TryAdd(validCharacter, temp2);
        }

        foreach (var motif in motifs)
        {
            for (int i = 0; i < Length; i++)
            {
                MotifCounts[motif[i]][i] += 1;
            }
        }

        for (int i = 0; i < Length; i++)
        {
            var valueCounter= new Dictionary<char, int>();
            var max = ' ';
            var currentMax = 0;
            foreach (var value in T.ValidAlphabet)
            {
                valueCounter.TryAdd(value, 0);
            }

            foreach (var value in T.ValidAlphabet)
            {
                foreach (var motif in motifs)
                {
                    valueCounter[motif[i]]++;
                    if (valueCounter[value] <= currentMax) continue;
                    currentMax = valueCounter[value];
                    max = value;
                }
            }

            Consensus += max;
        }
    }

    private void VerifyInputs(List<Motif> motifs)
    {
        foreach (var motif in motifs)
        {
            // TODO: consider adding checks here by type
            if (motif.Length != Length)
            {
                throw new ArgumentException($"Motif {motif} must have the same length.");
            }
        }
    }

    public Dictionary<char, List<int>> MotifCounts { get; } = new Dictionary<char, List<int>>();
    public Dictionary<char, List<double>> MotifProbabilities { get; } = new Dictionary<char, List<double>>();
    public string Consensus { get; }
    public int Score()
    {
        throw new NotImplementedException();
    }

    public int Length { get; }

}