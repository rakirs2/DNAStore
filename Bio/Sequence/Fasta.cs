using System.Text.Json;

namespace Bio.Sequence;

public class Fasta : IFasta
{
    public string Name { get; }
    public string RawSequence { get; }
    public Dictionary<char,int> Frequencies { get; } = new();
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

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
            Fasta fasta = (Fasta)obj;
            return fasta.Name.Equals(this.Name) && this.XorHash.Equals(fasta.XorHash);
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

    public int XorHash { get; } = 0;

    public Fasta(string name, string rawSequence)
    {
        Name = name;
        RawSequence = rawSequence;
        foreach (char c in RawSequence)
        {
            XorHash = XorHash ^ c;
            if (Frequencies.ContainsKey(c))
            {
                Frequencies[c] += 1;
            }
            else
            {
                Frequencies[c] = 1;
            }
        }
    }

    public IEnumerable<string> SplitInHalf()
    {
        int halfWay = RawSequence.Length / 2;
        return [RawSequence[..halfWay], RawSequence[halfWay..]];
    }
}