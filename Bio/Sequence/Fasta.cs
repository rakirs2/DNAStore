using System.Text.Json;

namespace Bio.Sequence;

public class Fasta : IFasta
{
    public Fasta(string name, string rawSequence)
    {
        Name = name;
        RawSequence = rawSequence;
        // This is, imo, bad. It's ok for now but it will need to be refactored
        // Additionally, this is getting to the point where I'd rather a static fasta method returned a type of each
        // rather than what it's doing now
        ContentType = ContentType.Unknown;
        bool isPossibleRNA = false;
        foreach (char c in RawSequence)
        {
            if (ContentType == ContentType.Unknown)
            {
                // If we know there's a single value that passes the RNA vs DNA comparison, we no longer need to run it
                // It's just an uridine check, but for a person without domain knowledge this may help.
                if (!isPossibleRNA)
                    isPossibleRNA = SequenceHelpers.IsKnownRNADifferentiator(c);

                if (SequenceHelpers.IsKnownProteinDifferentiator(c))
                    ContentType = ContentType.Protein;
            }

            XorHash ^= c;
            if (!Frequencies.TryAdd(c, 1))
                Frequencies[c] += 1;
        }

        // If it's already defined, we're good
        if (ContentType != ContentType.Unknown) return;
        ContentType = isPossibleRNA ? ContentType.RNA : ContentType.DNA;
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