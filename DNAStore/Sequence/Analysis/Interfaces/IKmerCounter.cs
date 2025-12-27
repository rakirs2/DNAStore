namespace DnaStore.Sequence.Analysis.Interfaces;

public interface IKmerCounter
{
    HashSet<string> HighestFrequencyKmers { get; }

    int KmerLength { get; }
}