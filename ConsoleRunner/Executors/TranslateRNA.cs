using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bio.Sequence.Types;

namespace ConsoleRunner.Executors
{


    public class TranslateRNA : BaseExecutor
    {
        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the first sequence");
            _a = new RNASequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            _result = _a.GetExpectedProteinString();
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"The translated protein between both sequences is:\n{_result}");
        }

        private RNASequence _a;
        private string _result;
    }
}
