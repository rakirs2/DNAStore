using Bio.Analysis.Types;
using Bio.Sequence.Types;

namespace DNAStore.Executors;

public class MotifFinder : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please enter the first sequence");
        a = new AnySequence(Console.ReadLine());
        Console.WriteLine("Please enter the motif");
        var motifString = Console.ReadLine();
        Console.WriteLine("Please enter the expected Length");
        var expectedLength = int.Parse(Console.ReadLine());
        b = new Motif(motifString, expectedLength);

        Console.WriteLine("Is Zero Index 'y'");
        var input = Console.ReadLine();
        _isZeroIndex = input.Equals("y", StringComparison.OrdinalIgnoreCase);
    }

    protected override void CalculateResult()
    {
        result = a.MotifLocations(b, _isZeroIndex);
    }

    protected override void OutputResult()
    {
        var indexType = _isZeroIndex ? "Zero" : "One";
        Console.WriteLine($"The {indexType}-Index Locations are: \n{string.Join(" ", result)}");
    }

    private bool _isZeroIndex;
    private AnySequence? a;
    private Motif? b;
    private long[]? result;
}