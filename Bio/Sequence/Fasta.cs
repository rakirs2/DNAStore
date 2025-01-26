namespace Bio.Sequence;

public class Fasta : IFasta
{
    public string Name { get; }
    public string RawSequence { get; }

    public Fasta()
    {
        Name = "name";
        RawSequence = "rawSequence";
    }
}