using Bio.Sequence.Types;

namespace ConsoleRunner;

internal class SequenceAnalysis : IExecutor
{
    // TODO: Run should have a timer around it-- one more level of abstraction
    public void Run()
    {
        GetInputs();
        OutputResult();
    }

    private void GetInputs()
    {
        Console.WriteLine("Please Input the string in question");
        var inputString = Console.ReadLine();
        if (inputString != null) _anySequence = new AnySequence(inputString);
    }

    private void OutputResult()
    {
        Console.WriteLine(_anySequence?.Counts);
    }

    private AnySequence? _anySequence;
}