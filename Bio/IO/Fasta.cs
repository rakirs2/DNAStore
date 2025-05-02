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
}