namespace Bio.Sequence;

public class Fasta : IFasta
{
    public string Name { get; }
    public string RawSequence { get; }
    public Dictionary<char,int> Frequencies { get; } = new Dictionary<char,int>();


    public Fasta(string name, string rawSequence)
    {
        Name = name;
        RawSequence = rawSequence;
        foreach (char c in RawSequence)
        {
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
}