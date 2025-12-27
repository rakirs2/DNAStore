using DNAStore.Sequences.Types;

namespace DNAStore.Sequences.Analysis.Interfaces;

// TODO: Consider base classing this. For now it's probably ok
public interface IKmerClumpCounter
{
    /// <summary>
    ///     Length of string on which to search for a given clump
    /// </summary>
    int ScanLength { get; }

    /// <summary>
    ///     Length of the Kmers we are searching for
    /// </summary>
    int KmerLength { get; }

    /// <summary>
    ///     The minimum number of times a Kmer must occur for it to be registered
    /// </summary>
    int MinCount { get; }

    /// <summary>
    ///     The seqquence being analyzed
    /// </summary>
    Sequence Sequence { get; }

    public HashSet<string> ValidKmers { get; }
}