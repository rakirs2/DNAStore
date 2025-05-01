namespace Bio.Sequence;

public interface ISequence
{
    long Length { get; }
    string RawSequence { get; }
}