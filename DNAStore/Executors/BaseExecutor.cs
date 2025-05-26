using System.Diagnostics;

namespace DNAStore.Executors;

internal abstract class BaseExecutor : IExecutor
{
    internal static IExecutor GetExecutorFromString(string input)
    {
        switch (input)
        {
            case "SequenceAnalysis":
                return new SequenceAnalysis();
            case "DNAToRNA":
                return new TranscibeDna();
            case "DNAComplement":
                return new DNAComplement();
            case "GCContent":
                return new GCContent();
            case "HammingDistance":
                return new HammingDistance();
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
                return new LongestCommonSubsequence();
            case "ProteinMotif":
                return new ProteinMotifFinder();
            case "ProteinToNumRNA":
                return new ProteinToNumRNACount();
            case "ClumpFinder":
                return new ClumpFinder();
            case "MinGCSkewLocation":
                return new MinGCSkewLocation();
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