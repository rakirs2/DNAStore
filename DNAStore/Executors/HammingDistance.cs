using Bio.Sequence.Types;

namespace DNAStore.Executors;

internal class HammingDistance : BaseExecutor
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
        result = AnySequence.HammingDistance(a, b);
    }

    protected override void OutputResult()
    {
        Console.WriteLine($"The HammingDistance Distance between both sequences is: {result}");
    }

    private AnySequence? a;
    private AnySequence? b;
    private long result;
}