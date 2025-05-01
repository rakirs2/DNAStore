using Bio.Sequence.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRunner
{
    public class TranscibeDna : IExecutor
    {
        public void Run()
        {
            GetInputs();
            OutputResult();
        }

        private void GetInputs()
        {
            Console.WriteLine("Please input the DNA in question");
            var inputString = Console.ReadLine();
            if (inputString != null)
            {
                _dnaSequence = new DNASequence(inputString);
            }
        }

        private void OutputResult()
        {
            Console.WriteLine(_dnaSequence?.TranscribeToRNA().RawSequence);
        }

        private DNASequence? _dnaSequence;
    }
}
