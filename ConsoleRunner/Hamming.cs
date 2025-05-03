using Bio.IO;
using Bio.Sequence.Types;

namespace ConsoleRunner
{
    public class Hamming : IExecutor
    {
        public void Run()
        {
            GetInputs();
            CalculateResult();
            OutputResult();
        }

        private void GetInputs()
        {
            Console.WriteLine("Please enter the first sequence");
            a = new AnySequence(Console.ReadLine());
            Console.WriteLine("Please enter the second sequence");
            b = new AnySequence(Console.ReadLine());
        }

        private void CalculateResult()
        {
            result = AnySequence.HammingDistance(a, b);
        }

        private void OutputResult()
        {
            Console.WriteLine($"The Hamming Distance between both sequences is: {result}");
        }


        private AnySequence a;
        private AnySequence b;
        private long result;
    }
}
