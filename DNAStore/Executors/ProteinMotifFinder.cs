using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bio.IO;
using Bio.Sequence.Types;
using Clients;

namespace DNAStore.Executors;

public class ProteinMotifFinder : BaseExecutor
{
    protected override void GetInputs()
    {
        Console.WriteLine("Type the UniProt Protein for motif");
        // TODO: clean up, async all the way
        // TODO: clean up, this is ugly as crap
        // TODO: Ugh threading
        motif = new AnySequence(UniprotClient.GetAsync(Console.ReadLine()).Result);

        var input = "";
        sequencesToCompare = new List<AnySequence>();
        while (!input.Equals("complete"))
        {
            // TODO, create a generic non try catch for this to prevent nulls/io errors
            input = Console.ReadLine();
            if (input.Equals("complete", StringComparison.InvariantCultureIgnoreCase)) break;

            var seq = UniprotClient.GetAsync(input).Result;
            sequencesToCompare.Add(new AnySequence(seq));
        }
    }

    protected override void CalculateResult()
    {
        foreach (var seq in sequencesToCompare) output.Add(seq.MotifLocations(motif));
    }

    protected override void OutputResult()
    {
        for (var i = 0; i < sequencesToCompare.Count; i++)
        {
            Console.WriteLine($"{sequencesToCompare[i].Name}");
            Console.WriteLine($"{string.Join(" ", output[i])}");
        }
    }

    private List<Task> tasksToAwait = new();
    private List<long[]> output = new();
    private List<AnySequence> sequencesToCompare = new();
    private AnySequence motif;
    private long[] result;
}