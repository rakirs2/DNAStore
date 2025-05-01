using Bio.Sequence;

namespace ConsoleRunner
{
    internal class SequenceAnalysis : IExecutor
    {
        public void Run()
        {
            GetInputs();
            OutputResult();
        }

        private void GetInputs()
        {
            Console.WriteLine("Please Input the string in question");
            var inputString = Console.ReadLine();
            if (inputString != null)
            {
                _anySequence = new AnySequence(inputString);
            }
        }

        private void OutputResult()
        {
            Console.WriteLine(_anySequence?.Counts);
        }

        private AnySequence? _anySequence;
    }
}
