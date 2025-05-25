using Bio.Sequence.Types;

namespace DNAStore.Executors;
internal class RestictionSites : BaseExecutor
{

    protected override void GetInputs()
    {
        Console.WriteLine("Enter Sequence To be analyzed");
        sequence = new DNASequence(Console.ReadLine());
    }

    protected override void CalculateResult()
    {
        output = sequence.RestrictionSites();
    }

    protected override void OutputResult()
    {
        foreach (var tuple in output)
        {
            Console.WriteLine($"{tuple.Item1} {tuple.Item2}");
        }
    }

    private DNASequence? sequence;
    private List<Tuple<int, int>>? output;
}
