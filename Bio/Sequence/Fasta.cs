namespace Bio.Sequence;

public class Fasta : IFasta
{
    public string Name { get; }
    public string RawSequence { get; }

    public Fasta(string name, string rawSequence)
    {
        Name = name;
        RawSequence = rawSequence;
    }
}