namespace Bio.Sequence;

public interface IFasta
{
    string Name { get; }
    string RawSequence { get; }
    int XorHash { get; }
    public Dictionary<char, int> Frequencies { get; }
    public string ToJson();
    public void Compress();
}