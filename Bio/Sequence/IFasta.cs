namespace Bio.Sequence;

public interface IFasta
{
    string Name { get; }
    string RawSequence { get; }

    public Dictionary<char, int> Frequencies { get; }
}