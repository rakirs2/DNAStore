using Bio.Analysis.Interfaces;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;
public class KmerClumpCounter : IKmerClumpCounter
{
    // TODO: there's probably some work to generalize this later
    // TODO: Design phil -- do I really care if there's calculations going on construction?
    public KmerClumpCounter(AnySequence sequence, int scanLength, int kmerLength, int minCount)
    {
        Sequence = sequence;
        ScanLength = scanLength;
        KmerLength = kmerLength;
        MinCount = minCount;
        // Always assume that we are adding to a proper Dictionary
        for (int i = 0; i < sequence.RawSequence.Length - kmerLength; i++)
        {
            while (_orderQueue.Count >= ScanLength)
            {
                var temp = _orderQueue.Dequeue();
                _slidingCounter[temp] -= 1;
            }

            var current = sequence.RawSequence.Substring(i, kmerLength);
            _orderQueue.Enqueue(current);

            _slidingCounter.TryAdd(current, 0);
            _slidingCounter[current] += 1;
            if (_slidingCounter[current] >= MinCount)
            {
                ValidKmers.Add(current);
            }

        }
    }

    public int ScanLength { get; }
    public int KmerLength { get; }
    public AnySequence Sequence { get; }
    public int MinCount { get; }

    public HashSet<string> ValidKmers { get; } = new HashSet<string>();


    private Dictionary<string, int> _slidingCounter = new();

    private Queue<string> _orderQueue = new();
}
