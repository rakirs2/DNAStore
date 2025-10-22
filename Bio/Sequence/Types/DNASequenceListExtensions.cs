namespace Bio.Sequence.Types;

public static class DNASequenceListExtensions
{
    /// <summary>
    /// This is an incredibly greedy, n^2 implementation. We are just going to go down the list and concatenate the best match
    /// There are clearly better ways to do this. But for now, let's get something that works
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
            int maxOverlap = -1;
            int bestI = -1, bestJ = -1;

            for (int i = 0; i < copyOfList.Count; i++)
            {
                for (int j = 0; j < copyOfList.Count; j++)
                {
                    if (i == j) continue;

                    int currentOverlap = AnySequence.CalculateOverlap(copyOfList[i], copyOfList[j]);
                    if (currentOverlap > maxOverlap)
                    {
                        maxOverlap = currentOverlap;
                        bestI = i;
                        bestJ = j;
                    }
                }
            }
            
            if (maxOverlap == 0) // No overlaps, just concatenate remaining
            {
                DNASequence result = new DNASequence("");
                foreach (var r in list)
                {
                    result += r;
                }
                return  result;
            }
            
            // Merge the two best overlapping reads
            string mergedString = copyOfList[bestI] + copyOfList[bestJ][maxOverlap..];
            copyOfList.RemoveAt(bestI);
            // Adjust index if bestJ was after bestI
            if (bestJ > bestI) copyOfList.RemoveAt(bestJ - 1); 
            else copyOfList.RemoveAt(bestJ);
            
            copyOfList.Add(new DNASequence(mergedString));
        }
            
        return copyOfList[0]; // The single remaining superstring
    }
}
