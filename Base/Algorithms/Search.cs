using Base.Utils;

namespace Base.Algorithms;

public static class Search
{
    public static List<List<int>> ThreeSumNoSort(List<int> inputArray, int target)
    {
        // Use a HashSet to store unique triplets (sorting each triplet ensures uniqueness regardless of order)
        var result = new HashSet<List<int>>(ListEqualityComparer<int>.Default);

        for (var i = 0; i < inputArray.Count - 2; i++)
        {
            // Use a secondary HashSet inside the loop to track numbers seen so far for the current 'i'
            var seen = new Dictionary<int, int>();
            var targetSum = -inputArray[i]; // Target for the remaining two numbers

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

                // Add the current number to the 'seen' set for future checks
                // TODO: this is restrictive. It should work for duplicates
                if (!seen.ContainsKey(inputArray[j]))
                    seen.Add(inputArray[j], j);
            }
        }

        return result.ToList();
    }
}