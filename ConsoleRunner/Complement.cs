using Bio.Sequence.Types;

namespace ConsoleRunner;

public class Complement : IExecutor
{
    public void Run()
    {
        GetInputs();
        OutputResult();
    }

    private void GetInputs()
    {
        Console.WriteLine("Please input the DNA in question");
        var inputString = Console.ReadLine();
        if (inputString != null) _dnaSequence = new DNASequence(inputString);
    }

    private void OutputResult()
    {
        Console.WriteLine(_dnaSequence?.ToReverseComplement().RawSequence);
    }

    private DNASequence? _dnaSequence;
}