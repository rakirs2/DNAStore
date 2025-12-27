using DnaStore.Sequence.Types.Interfaces;

namespace DnaStore.Sequence.Types;

public abstract class NucleotideSequence : Sequence, INucleotideSequence
{
    protected NucleotideSequence(string rawSequence) : base(rawSequence)
    {
    }

    protected NucleotideSequence(string rawSequence, string name) : base(rawSequence, name)
    {
    }


    protected abstract HashSet<char> Pyrimidines { get; }
    protected abstract HashSet<char> Purines { get; }

    public int[] CalculateMinPrefixGCSkew()
    {
        var globalMin = int.MaxValue;
        var currentMin = 0;
        var output = new List<int>();
        for (var i = 0; i < Length; i++)
        {
            if (this[i].Equals('C')) currentMin -= 1;

            if (this[i].Equals('G')) currentMin += 1;

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


    public double TransitionToTransversionRatio(NucleotideSequence other)
    {
        if (other == null) throw new NullReferenceException();

        if (other.GetType() != GetType()) throw new ArgumentException("types must match");

        if (other.Length != Length) throw new ArgumentException("length must match");

        var transversions = 0;
        var transitions = 0;

        for (var i = 0; i < Length; i++)
        {
            var a = this[i];
            var b = other[i];
            if (!a.Equals(b))
            {
                if (Pyrimidines.Contains(a) && Pyrimidines.Contains(b))
                    transitions++;
                else if (Purines.Contains(a) && Purines.Contains(b))
                    transitions++;
                else
                    transversions++;
            }
        }

        return (double)transitions / transversions;
    }

    public double GCRatio()
    {
        var totalGC = Counts.GetFrequency('G') + Counts.GetFrequency('g') +
                      Counts.GetFrequency('C') + Counts.GetFrequency('c');
        var totalBp = Counts.Count;
        return (double)totalGC / totalBp;
    }
}