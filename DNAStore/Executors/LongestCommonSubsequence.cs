using Bio.Analysis.Types;
using Bio.IO;

namespace DNAStore.Executors;

internal class LongestCommonSubsequence : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please input path to file");
        var location = Console.ReadLine();
        if (location != null) _fastas = FastaParser.Read(location);
    }

    protected override void CalculateResult()
    {
        _result = new Bio.Analysis.Types.LongestCommonSubsequence(_fastas);
    }

    protected override void OutputResult()
    {
        Console.WriteLine($"A longest common subsequence is: \n{_result.GetAnyLongest().RawSequence}");
    }

    private List<Fasta>? _fastas;
    private Bio.Analysis.Types.LongestCommonSubsequence? _result;
}