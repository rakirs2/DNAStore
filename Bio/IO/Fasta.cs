using System.Text.Json;
using Base.DataStructures;
using Bio.Sequences;
using Bio.Sequences.Types;

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

   

    public ContentType ContentType { get; }

    // TODO: override Hashcode and equals. Not actually important for this right now but could be later on.
    public string Name { get; }

    public string RawSequence { get; }
    public BasePairDictionary BasePairDictionary { get; }

    public long Length => RawSequence.Length;

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public void Compress()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return Name;
    }


    public void Save(string filePath)
    {
        File.WriteAllText(filePath, ToJson());
    }

    /// <summary>
    ///     Potentially dubious method. Let's see wheere it goes.
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
        return fastas.Aggregate((i1, i2) => new DnaSequence(i1.RawSequence).GCRatio() > new DnaSequence(i2.RawSequence).GCRatio() ? i1 : i2);
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}