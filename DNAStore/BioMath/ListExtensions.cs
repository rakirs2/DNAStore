namespace DNAStore.BioMath;

public static class ListExtensions
{
    // TODO: I don't like the way this class looks/functions. It's a similiar lesson in AI slop
    public static List<int> LongestIncreasingSubsequence(this List<int> list)
    {
        var ret = new List<int>();
        var dp = new List<(int, int)>();
        var prv = new Dictionary<int, int>();
        var count = list.Count;

        for (var index = count - 1; index >= 0; --index)
        {
            var end = -list[index];

            int l = 0, r = dp.Count;
            while (l < r)
            {
                var m = l + (r - l) / 2;
                if (dp[m].Item1 < end)
                    l = m + 1;
                else
                    r = m;
            }

            var i = l;

            var tmp = -1;

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

        var cur = dp[^1].Item2;
        while (cur >= 0)
        {
            ret.Add(list[cur]);
            cur = prv.TryGetValue(cur, out var value) ? value : -1;
        }

        return ret;
    }

    public static List<int> LongestDecreasingSubsequence(this List<int> list)
    {
        if (list.Count == 0) return new List<int>();

        var ldsLists = new List<List<int>>(list.Count);

        for (var i = 0; i < list.Count; i++) ldsLists.Add(new List<int> { list[i] });

        for (var i = 1; i < list.Count; i++)
        for (var j = 0; j < i; j++)
            if (list[i] < list[j])
                if (ldsLists[i].Count < ldsLists[j].Count + 1)
                {
                    ldsLists[i] = new List<int>(ldsLists[j]);
                    ldsLists[i].Add(list[i]);
                }

        var longestLDS = new List<int>();
        foreach (var subList in ldsLists)
            if (subList.Count > longestLDS.Count)
                longestLDS = subList;

        return longestLDS;
    }
}