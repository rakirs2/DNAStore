using Bio.Analysis.Interfaces;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;

public class KmerClumpCounter : IKmerClumpCounter
{
    // TODO: there's probably some work to generalize this later
    // TODO: Design phil -- do I really care if there's calculations going on construction?
    // TODO: Get a separate calculate step for all of these data structures
    public KmerClumpCounter(Sequence.Types.Sequence sequence, int scanLength, int kmerLength, int minCount)
    {
        Sequence = sequence;
        ScanLength = scanLength;
        KmerLength = kmerLength;
        MinCount = minCount;
        // Always assume that we are adding to a proper Dictionary
        for (var i = 0; i <= sequence.RawSequence.Length - KmerLength; i++)
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
            if (_slidingCounter[current] >= MinCount) ValidKmers.Add(current);
        }
    }

    public int ScanLength { get; }
    public int KmerLength { get; }
    public Sequence.Types.Sequence Sequence { get; }
    public int MinCount { get; }

    public HashSet<string> ValidKmers { get; } = new();


    private Dictionary<string, int> _slidingCounter = new();

    private Queue<string> _orderQueue = new();
}