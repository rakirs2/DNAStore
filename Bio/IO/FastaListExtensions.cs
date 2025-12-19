using Bio.Sequences.Types;

namespace Bio.IO;

public static class FastaListExtensions
{
    public static List<List<double>> GenerateDistanceMatrix(this List<Fasta> list)
    {
        var output = new List<List<double>>();
        for (var i = 0; i < list.Count; i++)
        {
            output.Add(new List<double>());
            for (var j = 0; j < list.Count; j++)
                output[i].Add(Math.Round(
                    (double)Sequence.HammingDistance(list[i].RawSequence, list[j].RawSequence) /
                    list[i].RawSequence.Length, 4));
        }

        return output;
    }

    /// <summary>
    ///     Potentially wasteful. But can work
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<DnaSequence> PostProcessAsDNASequence(this List<Fasta> list)
    {
        var output = new List<DnaSequence>();
        foreach (var fasta in list) output.Add(new DnaSequence(fasta.RawSequence));

        return output;
    }
}