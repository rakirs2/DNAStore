using Bio.Sequence.Types;

namespace DNAStore.Executors;

internal class ProteinWeight : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please input the protein string");
        _protein = new ProteinSequence(Console.ReadLine());
    }

    protected override void CalculateResult()
    {
        _result = _protein.MolecularWeight;
    }

    protected override void OutputResult()
    {
        Console.WriteLine(_result);
    }

    private double _result;
    private ProteinSequence? _protein;
}