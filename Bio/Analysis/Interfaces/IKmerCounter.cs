namespace Bio.Analysis.Interfaces;

public interface IKmerCounter
{
    string HighestFrequencyKmer { get; }
    int KmerLength { get; }
}