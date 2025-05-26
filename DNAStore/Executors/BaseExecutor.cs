using System.Diagnostics;

namespace DNAStore.Executors;

public abstract class BaseExecutor : IExecutor
{
    public static IExecutor GetFromString(string inputString)
    {
        switch (inputString)
        {
            case "SequenceAnalysis":
                return new SequenceAnalysis();
            case "DNAToRNA":
                return new TranscibeDna();
            case "DNAComplement":
                return new DNAComplement();
            case "GCContent":
                return new GCContent();
            case "hamming":
                return new Hamming();
            case "TranslateRNA":
                return new TranslateRNA();
            case "PercentDominant":
                return new PercentDominant();
            case "Permutations":
                return new Permutations();
            case "MotifFinder":
                return new MotifFinder();
            case "ProfileMatrix":
                return new ProfileMatrixExecutor();
            case "ProteinWeight":
                return new ProteinWeight();
            case "OverlapGraph":
                return new OverlapGraphExecutor();
            case "LongestCommonSubsequence":
                return new LongestCommonSubsequenceExecutor();
            case "ProteinMotif":
                return new ProteinMotifFinder();
            case "ProteinToNumRNA":
                return new ProteinTomRNACount();
            case "ClumpFinder":
                return new ClumpFinder();
            case "MinSkewLocation":
                return new MinSkewLocation();
            case "RestrictionSites":
                return new RestictionSites();
            case "why":
                return new EasterEgg();
            default:
                // probably safe to do it this way
                return new SequenceAnalysis();
        }
    }

    public void Run()
    {
        GetInputs();
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
        CalculateResult();
        _stopwatch.Stop();
        OutputResult();
        ReportMetrics();
    }

    /// <summary>
    /// Should solely be user facing
    /// </summary>
    protected abstract void GetInputs();

    /// <summary>
    /// Ideally this is one function. However, that isn't always possible
    /// </summary>
    protected abstract void CalculateResult();

    /// <summary>
    /// 
    /// </summary>
    protected abstract void OutputResult();

    private void ReportMetrics()
    {
        Console.WriteLine($"Calculation took: {_stopwatch.ElapsedMilliseconds}ms");
    }

    private Stopwatch? _stopwatch;
}