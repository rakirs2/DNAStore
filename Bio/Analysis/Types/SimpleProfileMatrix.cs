using System.Text;
using Base.DataStructures;
using Bio.Analysis.Interfaces;
using Bio.IO;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;

// For now, it's fine if it's accessible, but if the only use case is for a fasta read, it should probably be subclassed and interfaced
public class SimpleProfileMatrix : IProfileMatrix
{
    private readonly HashSet<char> listOfChars = new();
    private readonly List<BasePairDictionary> listOfFrequencies;

    public SimpleProfileMatrix(IList<Fasta> inputs)
    {
        if (inputs is null)
            throw new ArgumentNullException(nameof(inputs));
        
        if(inputs.Count == 0)
            throw new ArgumentException("Input list is empty", nameof(inputs));
        
        LengthOfSequences = inputs[0].RawSequence.Length;
        listOfFrequencies = new List<BasePairDictionary>();

        for (long i = 0; i < LengthOfSequences; i++) listOfFrequencies.Add(new BasePairDictionary());

        foreach (var input in inputs)
            for (var i = 0; i < input.Length; i++)
            {
                listOfFrequencies[i].Add(input.RawSequence[i]);
                listOfChars.Add(input.RawSequence[i]);
            }

        QuantityAnalyzed = inputs.Count;
    }

    public long LengthOfSequences { get; }

    public long QuantityAnalyzed { get; }

    public AnySequence GetProfileSequence()
    {
        // TODO: if this ever gets called repeatedly, cache it
        var stringBuilder = new StringBuilder();
        foreach (var basePairDictionary in listOfFrequencies)
            stringBuilder.Append(basePairDictionary.HighestFrequencyBasePair);

        return new AnySequence(stringBuilder.ToString());
    }

    public string GetCleanOutput()
    {
        var sequence = GetProfileSequence();
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(sequence);
        stringBuilder.Append("\n");

        return stringBuilder.ToString();
    }

    public string FrequencyMatrix()
    {
        var stringBuilder = new StringBuilder();
        var characters = listOfChars.ToArray();
        Array.Sort(characters);
        foreach (var bp in characters)
        {
            stringBuilder.Append(bp + ":");
            for (var i = 0; i < LengthOfSequences; i++)
                stringBuilder.Append(" " + listOfFrequencies[i].GetFrequency(bp));
            stringBuilder.Append("\n");
        }

        return stringBuilder.ToString();
    }
}