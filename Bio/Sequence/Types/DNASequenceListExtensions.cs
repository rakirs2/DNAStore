namespace Bio.Sequence.Types;

public static class DNASequenceListExtensions
{
    /// <summary>
    ///     This is an incredibly greedy, n^2 implementation. We are just going to go down the list and concatenate the best
    ///     match
    ///     There are clearly better ways to do this. But for now, let's get something that works
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static DNASequence GenerateLongestStringGreedy(this List<DNASequence> list)
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
                var result = new DNASequence("");
                foreach (var r in list) result += r;
                return result;
            }

            // Merge the two best overlapping reads
            var mergedString = copyOfList[bestI] + copyOfList[bestJ][maxOverlap..];
            copyOfList.RemoveAt(bestI);
            // Adjust index if bestJ was after bestI
            if (bestJ > bestI) copyOfList.RemoveAt(bestJ - 1);
            else copyOfList.RemoveAt(bestJ);

            copyOfList.Add(new DNASequence(mergedString));
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
    public static List<ErrorCorrection> GenerateErrorCorrections(this List<DNASequence> list, int distance = 1)
    {
        var output = new List<ErrorCorrection>();
        var dict = new ReverseComplementDictionary();

        var needsReview = new HashSet<DNASequence>();
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
}