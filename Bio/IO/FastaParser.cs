using System.Text.RegularExpressions;

namespace Bio.IO;

public static class FastaParser
{
    public static List<Fasta> Read(string filePath)
    {
        var name = "";
        var currentSequence = "";
        List<Fasta> output = [];
        // Not sure about best practice here -- but I think we can be relatively optimistic about the incoming format
        // Realistically, we do not want to deal with bad formatting 
        foreach (string? line in File.ReadLines(filePath))
            if (line.StartsWith('>'))
            {
                // Let's handle possible existing classes.
                // There might be some formatting things that can be optimized later on down the line.
                if (name != "" || currentSequence != "")
                {
                    output.Add(new Fasta(name, currentSequence));
                    name = "";
                    currentSequence = "";
                }

                name = line[1..];
            }
            else
            {
                // This is terrible form. I doubt I'll run into perf bottlenecks locally.
                // But there is a likely a point at large genomics where this can be optimized to a string builder.
                // Maybe we can infer from the size of the file if we should use string.concat/builder etc.
                currentSequence += line;
            }

        if (name != "" || currentSequence != "") output.Add(new Fasta(name, currentSequence));

        return output;
    }

    /// <summary>
    /// This is a helper method to simply deserialze input from where the Fasta file can't actually be read
    /// Completely. For now, this assumes only 1 Fasta per request.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Fasta DeserializeRawString(string input)
    {
        string[]? listofStrings = Regex.Split(input, @"\r?\n");
        return new Fasta(listofStrings[0].Substring(1), string.Concat(listofStrings[1..]));
    }
}