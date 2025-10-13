using Bio.Sequence.Types;

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
                    (double)AnySequence.HammingDistance(list[i].RawSequence, list[j].RawSequence) /
                    list[i].RawSequence.Length, 4));
        }

        return output;
    }
}