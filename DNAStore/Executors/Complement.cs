using Bio.Sequence.Types;

namespace DNAStore.Executors;

public class Complement : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please input the DNA in question");
        string? inputString = Console.ReadLine();
        if (inputString != null) _dnaSequence = new DNASequence(inputString);
    }

    protected override void CalculateResult()
    {
        output = _dnaSequence?.ToReverseComplement().RawSequence;
    }

    protected override void OutputResult()
    {
        Console.WriteLine($"The complement is {output}");
    }

    private DNASequence? _dnaSequence;
    private string? output;
}