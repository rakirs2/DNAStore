using Bio.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNAStore.Executors;

public class Permutations : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Input Number");
        total = int.Parse(Console.ReadLine());
    }

    protected override void CalculateResult()
    {
        values = Bio.Math.Probability.GetPermutations(Enumerable.Range(1, total), total);
    }

    protected override void OutputResult()
    {
        // Yes, double counting, not good but let's build there
        Console.WriteLine(values.Count());
        foreach (var row in values) Console.WriteLine(string.Join(" ", row));
    }

    private IEnumerable<IEnumerable<int>> values;
    private int total;
}