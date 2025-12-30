using System.Text.Json;
using DNAStore.Base.DataStructures;
using DNAStore.Sequences.Types;

namespace DNAStore.Sequences.IO;

public class Fasta : IFasta
{
    public Fasta(string name, string rawSequence)
    {
        Name = name;
        RawSequence = rawSequence;
        ContentType = ContentType.Unknown;
        BasePairDictionary = new BasePairDictionary();

        var isPossibleRna = false;
        foreach (var c in RawSequence)
        {
            if (ContentType == ContentType.Unknown)
            {
                if (!isPossibleRna)
                    isPossibleRna = SequenceHelpers.IsKnownRNADifferentiator(c);

                if (SequenceHelpers.IsKnownProteinDifferentiator(c))
                    ContentType = ContentType.Protein;
            }

            BasePairDictionary.Add(c);
        }

        if (ContentType != ContentType.Unknown) return;
        ContentType = isPossibleRna ? ContentType.RNA : ContentType.DNA;
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

    public Sequence GenerateSequence()
    {
        return new Sequence(RawSequence, Name);
    }

    public RnaSequence GenerateRNASequence()
    {
        return new RnaSequence(RawSequence, Name);
    }

    public DnaSequence GenerateDNASequence()
    {
        return new DnaSequence(RawSequence, Name);
    }

    public ProteinSequence GenerateProteinSequence()
    {
        throw new NotImplementedException();
    }

    public Sequence GenerateInferred()
    {
        switch (ContentType)
        {
            case ContentType.Protein:
                return GenerateProteinSequence();
            case ContentType.RNA:
                return GenerateRNASequence();
            case ContentType.DNA:
                return GenerateDNASequence();
            default:
                return GenerateSequence();
        }
    }

    public override string ToString()
    {
        return Name;
    }

    // TODO: surely this is not it. 
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

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}