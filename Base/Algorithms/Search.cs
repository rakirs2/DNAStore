using Base.Utils;

namespace Base.Algorithms;

public static class Search
{
    public static List<List<int>> ThreeSumNoSort(List<int> inputArray, int target)
    {
        var result = new HashSet<List<int>>(ListEqualityComparer<int>.Default);

        for (var i = 0; i < inputArray.Count - 2; i++)
        {
            var seen = new Dictionary<int, int>();
            var targetSum = -inputArray[i]; 
            for (var j = i + 1; j < inputArray.Count; j++)
            {
                var needed = targetSum - inputArray[j];

                if (seen.ContainsKey(needed))
                {
                    // Found a triplet: nums[i], nums[j], needed
                    var triplet = new List<int> { i, j, seen[needed] };
                    triplet.Sort(); // Sort the triplet to handle duplicates in the result set
                    result.Add(triplet);
                }
                
                // TODO: this is restrictive. It should work for duplicates
                if (!seen.ContainsKey(inputArray[j]))
                    seen.Add(inputArray[j], j);
            }
        }

        return result.ToList();
    }
}