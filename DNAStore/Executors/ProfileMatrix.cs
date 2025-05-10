using Bio.Analysis.Types;
using Bio.IO;
using DNAStore.Executors;

public class ProfileMatrixExecutor : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Please input path to file");
        var location = Console.ReadLine();
        if (location != null) fastas = FastaParser.Read(location);
    }

    protected override void CalculateResult()
    {
        matrix = new ProfileMatrix(fastas);
    }

    protected override void OutputResult()
    {
        Console.WriteLine(matrix.GetProfileSequence().RawSequence);
        Console.WriteLine(matrix.FrequencyMatrix());
    }

    private IList<Fasta> fastas;
    private ProfileMatrix matrix;
}