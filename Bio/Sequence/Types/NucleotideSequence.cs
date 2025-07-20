using Bio.IO;
using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types;

public abstract class NucleotideSequence : Sequence, INucleotideSequence
{
    protected NucleotideSequence(string rawSequence) : base(rawSequence)
    {
    }

    protected NucleotideSequence(string name, string rawSequence) : base(name, rawSequence)
    {
    }

    protected NucleotideSequence(Fasta fasta) : base(fasta)
    {
    }

    public int[] CalculateMinPrefixGCSkew()
    {
        var globalMin = int.MaxValue;
        var currentMin = 0;
        var output = new List<int>();
        for (var i = 0; i < RawSequence.Length; i++)
        {
            if (RawSequence[i].Equals('C')) currentMin -= 1;

            if (RawSequence[i].Equals('G')) currentMin += 1;

            // Preliminary prefix logic
            if (currentMin < globalMin)
            {
                globalMin = currentMin;
                output = [i + 1];
            }
            else if (currentMin == globalMin)
            {
                output.Add(i + 1);
            }
        }

        return output.ToArray();
    }
}