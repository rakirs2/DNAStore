using System.Diagnostics;

namespace ConsoleRunner.Executors;

public abstract class BaseExecutor : IExecutor
{
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

    private Stopwatch _stopwatch;
}