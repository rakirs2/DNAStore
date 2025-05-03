using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bio.IO;

public static class FastaParser
{
    public static IList<Fasta> Read(string filePath)
    {
        var name = "";
        var currentSequence = "";
        IList<Fasta> output = [];
        // Not sure about best practice here -- but I think we can be relatively optimistic about the incoming format
        // Realistically, we do not want to deal with bad formatting 
        foreach (var line in File.ReadLines(filePath))
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
}