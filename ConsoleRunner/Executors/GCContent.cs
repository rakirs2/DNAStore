using Bio.IO;
using Bio.Sequence.Types;

namespace ConsoleRunner.Executors;

public class GCContent : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please input path to file");
        var location = Console.ReadLine();
        if (location != null) fastas = FastaParser.Read(location);
    }

    protected override void CalculateResult()
    {
        largestGCContent = fastas?.Aggregate((i1, i2) => i1.GCContent > i2.GCContent ? i1 : i2);
    }

    protected override void OutputResult()
    {
        Console.WriteLine($"{largestGCContent?.Name}\n{largestGCContent?.GCContent * 100}");
    }

    private IList<Fasta>? fastas;
    private Fasta? largestGCContent;
}