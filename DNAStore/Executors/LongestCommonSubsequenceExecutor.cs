using Bio.Analysis.Types;
using Bio.IO;

namespace DNAStore.Executors;

internal class LongestCommonSubsequenceExecutor : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please input path to file");
        var location = Console.ReadLine();
        if (location != null) _fastas = FastaParser.Read(location);
    }

    protected override void CalculateResult()
    {
        _result = new LongestCommonSubsequence(_fastas);
    }

    protected override void OutputResult()
    {
        Console.WriteLine($"A longest common subsequence is: \n{_result.GetAnyLongest().RawSequence}");
    }

    private List<Fasta> _fastas;
    private LongestCommonSubsequence _result;
}