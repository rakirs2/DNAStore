using System.Runtime.CompilerServices;
using BioMath;

namespace Bio.Sequence.Types;

public static class DnaSequenceListExtensions
{
    /// <summary>
    ///     This is an incredibly greedy, n^2 implementation. We are just going to go down the list and concatenate the best
    ///     match
    ///     There are clearly better ways to do this. But for now, let's get something that works
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static DnaSequence GenerateLongestStringGreedy(this List<DnaSequence> list)
    {
        // create a shallow copy. We are not manipulating anything in the original list
        var copyOfList = list.ToList();

        // if there's only one, just return it.
        // We're going to stick with just the first string
        while (copyOfList.Count > 1)
        {
            var maxOverlap = -1;
            int bestI = -1, bestJ = -1;

            for (var i = 0; i < copyOfList.Count; i++)
            for (var j = 0; j < copyOfList.Count; j++)
            {
                if (i == j) continue;

                var currentOverlap = AnySequence.CalculateOverlap(copyOfList[i], copyOfList[j]);
                if (currentOverlap > maxOverlap)
                {
                    maxOverlap = currentOverlap;
                    bestI = i;
                    bestJ = j;
                }
            }

            if (maxOverlap == 0) // No overlaps, just concatenate remaining
            {
                var result = new DnaSequence("");
                foreach (var r in list) result += r;
                return result;
            }

            // Merge the two best overlapping reads
            var mergedString = copyOfList[bestI] + copyOfList[bestJ][maxOverlap..];
            copyOfList.RemoveAt(bestI);
            // Adjust index if bestJ was after bestI
            if (bestJ > bestI) copyOfList.RemoveAt(bestJ - 1);
            else copyOfList.RemoveAt(bestJ);

            copyOfList.Add(new DnaSequence(mergedString));
        }

        return copyOfList[0]; // The single remaining superstring
    }

    /// <summary>
    ///     Generates a list of estimated error corrections. This is a deterministic algorithm.
    /// </summary>
    /// <remarks>
    ///     TODO: this should be generic and only in one location.
    ///     TODO: this should be handled by its own data structure
    /// </remarks>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<ErrorCorrection> GenerateErrorCorrections(this List<DnaSequence> list, int distance = 1)
    {
        var output = new List<ErrorCorrection>();
        var dict = new ReverseComplementDictionary();

        var needsReview = new HashSet<DnaSequence>();
        foreach (var item in list)
        {
            if (dict[item] == 0) needsReview.Add(item);

            dict.Add(item);
        }

        foreach (var item in needsReview)
            if (dict[item] == 1)
                // ok, we have something that need analysis
                // we have 2 options --> 1 go through the list of the options and see if there exists a valid point match
                // with the Hamming Distance 
                foreach (var knownRead in list)
                {
                    if (knownRead.Equals(item))
                        continue;

                    var rc = knownRead.GetReverseComplement();
                    if (rc.Equals(item)) continue;

                    if (AnySequence.HammingDistance(knownRead, item) == distance && dict[knownRead] >= 2)
                    {
                        output.Add(new ErrorCorrection(item, knownRead));
                        break;
                    }

                    if (AnySequence.HammingDistance(knownRead.GetReverseComplement(), item) == distance &&
                        dict[knownRead.GetReverseComplement()] >= 2)
                    {
                        output.Add(new ErrorCorrection(item, knownRead.GetReverseComplement()));
                        break;
                    }
                }

        return output;
    }

    /// <summary>
    ///     Generates the list of kmers that are within the hamming distance specified
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static HashSet<string> MotifEnumeration(this List<DnaSequence> DnaSequences, int k, int distance)
    {
        var patterns = new HashSet<string>();
        var allPatterns = new HashSet<string>();
        foreach (var item in DnaSequences)
        {
            var currentKmers = item.KmerCompositionUniqueString(k);
            foreach (var kmer in currentKmers)
            foreach (var possible in new DnaSequence(kmer).DNeighborhood(distance))
                allPatterns.Add(possible);
        }

        foreach (var item in allPatterns)
        {
            var inAll = true;
            foreach (var seq in DnaSequences)
                if (!seq.ContainsString(item, distance))
                {
                    inAll = false;
                    break;
                }

            if (inAll) patterns.Add(item);
        }

        return patterns;
    }
    
    /// <summary>
    /// Returns the median string of the set of sequences
    /// </summary>
    /// <param name="sequences"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static List<string> MedianString(this List<DnaSequence> sequences, int size)
    {
        var kmers = Probability.GenerateAllKmers("ACGT", size);
        var results = new Dictionary<int, List<string>>();
        foreach (var kmer in kmers)
        {
            var currentMin = int.MinValue;
            foreach (var sequence in sequences)
            {
                currentMin = Math.Max(currentMin, sequence.GetMinimumDistanceForKmer(kmer));
            }

            if (results.ContainsKey(currentMin))
            {
                results[currentMin].Add(kmer);
            }

            else
            {
                results[currentMin] = new List<string>() { kmer };
            }
        }
        
        return results[results.Keys.Min()];
    }
}