using Bio.Sequence.Types;

namespace DNAStore.Executors;

internal class SequenceAnalysis : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please Input the string in question");
        _inputString = Console.ReadLine();
    }

    protected override void CalculateResult()
    {
        _anySequence = new AnySequence(_inputString ?? string.Empty);
    }

    protected override void OutputResult()
    {
        Console.WriteLine(_anySequence?.Counts);
    }

    private string? _inputString;
    private AnySequence? _anySequence;
}