using Bio.Math;

namespace ConsoleRunner.Executors
{

    public class PercentDominant : BaseExecutor
    {
        protected override void GetInputs()
        {
            Console.WriteLine("k");
            var inputString = Console.ReadLine();
            k = uint.Parse(inputString);
            Console.WriteLine("m");
            inputString = Console.ReadLine();
            m = uint.Parse(inputString);
            Console.WriteLine("n");
            inputString = Console.ReadLine();
            n = uint.Parse(inputString);
        }

        protected override void CalculateResult()
        {
            output = Probability.PercentDominant(k, m, n);
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"The percent Dominant is {output}");
        }

        private uint k;
        private uint m;
        private uint n;
        private double output;
    }
}
