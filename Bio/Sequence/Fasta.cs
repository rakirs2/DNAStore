using System.Text.Json;

namespace Bio.Sequence;

public class Fasta : IFasta
{
    public Fasta(string name, string rawSequence)
    {
        Name = name;
        RawSequence = rawSequence;
        ContentType = ContentType.Unknown;
        foreach (char c in RawSequence)
        {
            XorHash ^= c;
            if (!Frequencies.TryAdd(c, 1))
                Frequencies[c] += 1;
        }
    }

    public string Name { get; }
    public string RawSequence { get; }
    public Dictionary<char, int> Frequencies { get; } = new();
    public int XorHash { get; }
    public ContentType ContentType {get;}

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
            string fileContents = reader.ReadToEnd();
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
            return fasta.Name.Equals(Name) && XorHash.Equals(fasta.XorHash);
        }
        catch
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return XorHash;
    }
}