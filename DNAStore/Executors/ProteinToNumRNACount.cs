using Bio.Analysis.Types;
using Bio.Sequence.Types;

namespace DNAStore.Executors;

public class ProteinToNumRNACount : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please enter the first sequence");
        protein = new ProteinSequence(Console.ReadLine());
    }

    protected override void CalculateResult()
    {
        result = protein.NumberOfPossibleRNA();
    }

    protected override void OutputResult()
    {
        Console.WriteLine($"{result}");
    }

    private ProteinSequence? protein;
    private Motif? b;
    private long result;
}