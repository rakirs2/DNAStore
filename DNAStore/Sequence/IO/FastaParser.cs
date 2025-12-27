using System.Text;
using System.Text.RegularExpressions;

namespace Bio.IO;

public static class FastaParser
{
    /// <summary>
    ///     This defaults to assuming that there are multiple fastas in a file. Works for single.
    ///     Not really something that's worth optimizing.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static List<Fasta> Read(string filePath)
    {
        var name = "";
        var currentSequence = new StringBuilder();
        List<Fasta> output = [];
        foreach (var line in File.ReadLines(filePath))
            if (line.StartsWith('>'))
            {
                if (name != "" || currentSequence.ToString() != "" || currentSequence.Length > 0)
                {
                    output.Add(new Fasta(name, currentSequence.ToString()));
                    currentSequence = new StringBuilder();
                }

                name = line[1..];
            }
            else
            {
                currentSequence.Append(line);
            }

        if (name != "" || currentSequence.ToString().Length != 0)
            output.Add(new Fasta(name, currentSequence.ToString()));

        return output;
    }

    public static void SplitMultiFasta(string filePath, string outputFilePath)
    {
        var fastas = Read(filePath);
        foreach (var fasta in fastas)
        {
        }
    }

    /// <summary>
    ///     This is a helper method to simply deserialize input from where the Fasta file can't actually be read
    ///     Completely. For now, this assumes only 1 Fasta per request.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Fasta DeserializeRawString(string input)
    {
        var listofStrings = Regex.Split(input, @"\r?\n");
        return new Fasta(listofStrings[0].Substring(1), string.Concat(listofStrings[1..]));
    }
}