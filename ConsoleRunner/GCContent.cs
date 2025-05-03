using Bio.IO;
using Bio.Sequence.Types;

namespace ConsoleRunner
{
    public class GCContent : IExecutor
    {
        public void Run()
        {
            GetInputs();
            CalculateResult();
            OutputResult();
        }

        private void GetInputs()
        {
            Console.WriteLine("Please input path to file");
            var location = Console.ReadLine();
            if (location != null)
            {
                fastas = FastaParser.Read(location);
            }
        }

        private void CalculateResult()
        {
            Fasta biggest = fastas.Aggregate((i1, i2) => i1.GCContent > i2.GCContent ? i1 : i2);
        }

        private void OutputResult()
        {
            Console.WriteLine($"{largestGCContent.Name}\nGCContent:{largestGCContent.GCContent}");
        }

        private IList<Fasta> fastas;
        private Fasta largestGCContent;
    }
}
