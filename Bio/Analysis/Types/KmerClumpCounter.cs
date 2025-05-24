using Bio.Analysis.Interfaces;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;
public class KmerClumpCounter : IKmerClumpCounter
{
    public KmerClumpCounter(AnySequence sequence, int scanLength, int kmerLength, int minCount)
    {
        Sequence = sequence;
        ScanLength = scanLength;
        KmerLength = kmerLength;
        MinCount = minCount;
        for (int i = 0; i < sequence.RawSequence.Length - kmerLength; i++)
        {
            var current = sequence.RawSequence.Substring(i, kmerLength);
            if (Tracker.Count > MinCount)
            {
                //var toBeRemoved = new
            }
        }
    }

    public int ScanLength { get; }
    public int KmerLength { get; }
    public AnySequence Sequence { get; }
    public int MinCount { get; }

    public HashSet<string> ValidKmers { get; }


    private Dictionary<string, int> slidingCounter;

    private Queue<string> Tracker;
}
