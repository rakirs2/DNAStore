using Bio.Sequence.Types;

namespace DNAStore.Executors;

public class MinGCSkewLocation : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Enter Sequence To be analyzed");
        sequence = new DNASequence(Console.ReadLine());
    }

    protected override void CalculateResult()
    {
        output = sequence.CalculateMinPrefixGCSkew();
    }

    protected override void OutputResult()
    {
        Console.WriteLine($"{string.Join(' ', output)}");
    }

    private NucleotideSequence? sequence;
    private int[]? output;
}