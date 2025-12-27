using DNAStore.Sequences.Analysis.Interfaces;
using DNAStore.Sequences.IO;

namespace DNAStore.Sequences.Analysis.Types;

public class OverlapGraph : IOverlapGraph
{
    private readonly List<Tuple<Fasta, Fasta>> _overlaps = new();

    public OverlapGraph(IList<Fasta> fastas, int matchLength)
    {
        Number = fastas.Count;
        MatchLength = matchLength;

        // can't match with itself
        for (var i = 0; i < Number - 1; i++)
        for (var j = i + 1; j < Number; j++)
        {
            // there are two possible matches
            if (fastas[i].RawSequence[..MatchLength].Equals(fastas[j].RawSequence[^MatchLength..]))
                _overlaps.Add(new Tuple<Fasta, Fasta>(fastas[j], fastas[i]));

            if (fastas[j].RawSequence[..MatchLength].Equals(fastas[i].RawSequence[^MatchLength..]))
                _overlaps.Add(new Tuple<Fasta, Fasta>(fastas[i], fastas[j]));
        }
    }

    public int Number { get; }
    public int MatchLength { get; }

    public List<Tuple<Fasta, Fasta>> GetOverlaps()
    {
        return _overlaps;
    }
}