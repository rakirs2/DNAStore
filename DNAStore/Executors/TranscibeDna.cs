using Bio.Sequence.Types;

namespace DNAStore.Executors;

public class TranscibeDna : IExecutor
{
    public void Run()
    {
        GetInputs();
        OutputResult();
    }

    private void GetInputs()
    {
        Console.WriteLine("Please input the DNA in question");
        string? inputString = Console.ReadLine();
        if (inputString != null) _dnaSequence = new DNASequence(inputString);
    }

    private void OutputResult()
    {
        Console.WriteLine(_dnaSequence?.TranscribeToRNA().RawSequence);
    }

    private DNASequence? _dnaSequence;
}