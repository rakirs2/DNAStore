using Bio.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bio.Analysis.Types;

namespace DNAStore.Executors;

internal class OverlapGraphExecutor : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please input path to file");
        var location = Console.ReadLine();
        if (location != null) _fastas = FastaParser.Read(location);
    }

    protected override void CalculateResult()
    {
        _overlapGraph = new OverlapGraph(_fastas, 3);
    }

    protected override void OutputResult()
    {
        foreach (var tuple in _overlapGraph.GetOverlaps()) Console.WriteLine(tuple.Item1.Name + " " + tuple.Item2.Name);
    }

    private IList<Fasta>? _fastas;
    private OverlapGraph _overlapGraph;
}