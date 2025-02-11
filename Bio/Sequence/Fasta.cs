using System.Text.Json;

namespace Bio.Sequence;

public class Fasta : IFasta
{
    public Fasta(string name, string rawSequence)
    {
        Name = name;
        RawSequence = rawSequence;
        foreach (char c in RawSequence)
        {
            XorHash = XorHash ^ c;
            if (Frequencies.ContainsKey(c))
                Frequencies[c] += 1;
            else
                Frequencies[c] = 1;
        }
    }

    public string Name { get; }
    public string RawSequence { get; }
    public Dictionary<char, int> Frequencies { get; } = new();

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public void Compress()
    {
        throw new NotImplementedException();
    }

    public int XorHash { get; }

    public void Save(string filePath)
    {
        File.WriteAllText(filePath, ToJson());
    }

    public static Fasta? GetFromFile(string filePath)
    {
        TextReader reader = null;
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

    public override bool Equals(object? obj)
    {
        try
        {
            var fasta = (Fasta)obj;
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