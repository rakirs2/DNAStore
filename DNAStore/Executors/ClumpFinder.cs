using Bio.Analysis.Types;
using Bio.Sequence.Types;

namespace DNAStore.Executors;

internal class ClumpFinder : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please enter the sequence");
        _a = new AnySequence(Console.ReadLine());
        Console.WriteLine("Please enter the expected Length");
        _kmerLength = int.Parse(Console.ReadLine());

        Console.WriteLine("Window Size");
        _windowSize = int.Parse(Console.ReadLine());

        Console.WriteLine("Minimum Count");
        _minCount = int.Parse(Console.ReadLine());
    }

    protected override void CalculateResult()
    {
        _clumpCounter = new KmerClumpCounter(_a, _windowSize, _kmerLength, _minCount);
    }

    protected override void OutputResult()
    {
        Console.WriteLine($"{string.Join(' ', _clumpCounter.ValidKmers)}");
    }

    private int _kmerLength;
    private int _windowSize;
    private int _minCount;
    private AnySequence? _a;
    private KmerClumpCounter? _clumpCounter;
}