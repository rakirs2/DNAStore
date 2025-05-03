using Bio.Sequence;
using System.Text.Json;
using Base.DataStructures;

namespace Bio.IO;

public class Fasta : IFasta
{
    public Fasta(string name, string rawSequence)
    {
        Name = name;
        RawSequence = rawSequence;
        ContentType = ContentType.Unknown;
        BasePairDictionary = new BasePairDictionary();

        var isPossibleRNA = false;
        foreach (var c in RawSequence)
        {
            if (ContentType == ContentType.Unknown)
            {
                if (!isPossibleRNA)
                    isPossibleRNA = SequenceHelpers.IsKnownRNADifferentiator(c);

                if (SequenceHelpers.IsKnownProteinDifferentiator(c))
                    ContentType = ContentType.Protein;
            }

            BasePairDictionary.Add(c);
        }

        if (ContentType != ContentType.Unknown) return;
        ContentType = isPossibleRNA ? ContentType.RNA : ContentType.DNA;
    }

    public string Name { get; }

    public string RawSequence { get; }
    public BasePairDictionary BasePairDictionary { get; }
    public long Length { get; }

    // TODO: consider moving this to a nucleotide class. Or maybe a generic 
    public double GCContent
    {
        get
        {
            // TODO: this is a hack that can get refactored. I need to determine if I can safely assume that everything can be converted to uppercase
            var totalGC = BasePairDictionary.GetFrequency('G') + BasePairDictionary.GetFrequency('g') +
                          BasePairDictionary.GetFrequency('C') + BasePairDictionary.GetFrequency('c');
            var totalBp = BasePairDictionary.Count;
            return (double)totalGC / totalBp;
        }
    }

    public ContentType ContentType { get; }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public void Compress()
    {
        throw new NotImplementedException();
    }


    public void Save(string filePath)
    {
        File.WriteAllText(filePath, ToJson());
    }

    /// <summary>
    /// Potentially dubious method. Let's see wheere it goes.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static Fasta? GetFromFile(string filePath)
    {
        TextReader? reader = null;
        try
        {
            reader = new StreamReader(filePath);
            var fileContents = reader.ReadToEnd();
            return JsonSerializer.Deserialize<Fasta>(fileContents);
        }
        finally
        {
            if (reader != null)
                reader.Close();
        }
    }

    // TODO: this is rife for errors down the line
    public override bool Equals(object? obj)
    {
        try
        {
            var fasta = obj as Fasta;

            // TODO: fix later
            return fasta != null && fasta.Name.Equals(Name);
        }
        catch
        {
            return false;
        }
    }

    public static Fasta GetMaxGCContent(IList<Fasta> fastas)
    {
        return fastas.Aggregate((i1, i2) => i1.GCContent > i2.GCContent ? i1 : i2);
    }
}