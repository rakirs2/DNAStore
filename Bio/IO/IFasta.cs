using Base.DataStructures;

namespace Bio.IO;

public interface IFasta
{
    string Name { get; }
    string RawSequence { get; }
    BasePairDictionary BasePairDictionary { get; }
    long Length { get; }
    public string ToJson();
    public void Compress();
}