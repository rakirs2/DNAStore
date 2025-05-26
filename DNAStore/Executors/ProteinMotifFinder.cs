using Bio.Analysis.Types;
using Bio.Sequence.Types;

using Clients;

namespace DNAStore.Executors;

internal class ProteinMotifFinder : BaseExecutor
{
    protected override void GetInputs()
    {
        // TODO: at some point get a better pattern
        sequencesToCompare = new List<AnySequence>();
        while (true)
        {
            Console.WriteLine("Type the UniProt Protein for motif. type 'complete' when ready to start analysis");
            // TODO, create a generic non try catch for this to prevent nulls/io errors
            var input = Console.ReadLine();

            // TODO: I hate these-- get these as a rule
            if (input.Equals("complete", StringComparison.InvariantCultureIgnoreCase)) break;

            // TODO: async all the way
            var seq = UniprotClient.GetAsync(input).Result;

            inputNames.Add(input);
            sequencesToCompare.Add(new AnySequence(seq));
        }
    }

    protected override void CalculateResult()
    {
        foreach (var seq in sequencesToCompare)
            output.Add(seq.MotifLocations(KnownMotifs.NGlycostatin));
    }

    protected override void OutputResult()
    {
        for (var i = 0; i < sequencesToCompare.Count; i++)
            if (output[i].Length > 0)
            {
                Console.WriteLine($"{inputNames[i]}");
                Console.WriteLine($"{string.Join(" ", output[i])}");
            }
    }

    private List<long[]> output = new();
    private List<AnySequence> sequencesToCompare = new();
    private List<string> inputNames = new();
}