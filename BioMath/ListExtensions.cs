namespace BioMath;

// TODO: this works, but I need better understanding
public static class ListExtensions
{
    public static List<int> LongestIncreasingSubsequence(this List<int> list)
    {
        var ret = new List<int>();
        var dp = new List<(int, int)>(); // Stores (-value, index) pairs
        var prv = new Dictionary<int, int>(); // Stores previous index for each element
        int count = list.Count;

        // Process array in reverse order
        for (int index = count - 1; index >= 0; --index)
        {
            int end = -list[index];

            // Binary search to find insertion point of the next highest
            int l = 0, r = dp.Count;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                if (dp[m].Item1 < end)
                    l = m + 1;
                else
                    r = m;
            }

            int i = l;

            // Default previous index
            int tmp = -1;

            if (i == dp.Count)
            {
                if (dp.Count > 0) tmp = dp[^1].Item2;
                dp.Add((end, index));
            }
            else
            {
                if (i > 0) tmp = dp[i - 1].Item2;
                dp[i] = (end, index);
            }

            prv[index] = tmp;
        }

        // Reconstruct the LIS
        int cur = dp[^1].Item2;
        while (cur >= 0)
        {
            ret.Add(list[cur]);
            cur = prv.TryGetValue(cur, out int value) ? value : -1;
        }

        return ret;
    }

    public static List<int> LongestDecreasingSubsequence(this List<int> list)
    {
        if (list == null || list.Count == 0) return new List<int>();

        var ldsLists = new List<List<int>>(list.Count);

        // Initialize each ldsList[i] with a list containing only arr[i]
        for (var i = 0; i < list.Count; i++) ldsLists.Add(new List<int> { list[i] });

        // Build the longest decreasing subsequences
        for (var i = 1; i < list.Count; i++)
        for (var j = 0; j < i; j++)
            if (list[i] < list[j]) // If arr[i] can extend the subsequence ending at arr[j]
                if (ldsLists[i].Count < ldsLists[j].Count + 1)
                {
                    ldsLists[i] = new List<int>(ldsLists[j]); // Copy the existing subsequence
                    ldsLists[i].Add(list[i]); // Append the current element
                }

        // Find the overall longest decreasing subsequence
        var longestLDS = new List<int>();
        foreach (var subList in ldsLists)
            if (subList.Count > longestLDS.Count)
                longestLDS = subList;

        return longestLDS;
    }
}