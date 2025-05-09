using System.Text;
using Bio.Sequence.Types;

namespace DNAStore.Executors;

public class MotifFinder : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please enter the first sequence");
        a = new AnySequence(Console.ReadLine());
        Console.WriteLine("Please enter the second sequence");
        b = new AnySequence(Console.ReadLine());
    }

    protected override void CalculateResult()
    {
        result = a.MotifLocations(b);
    }

    protected override void OutputResult()
    {
        Console.WriteLine($"The One Index Locations are: \n{string.Join(" ", result)}");
    }

    private AnySequence a;
    private AnySequence b;
    private long[] result;
}