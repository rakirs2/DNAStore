namespace Base.Algorithms;

public static class BinarySearch
{
    // TODO: generalize this, C# already has it
    // A fun exercise would be to use this for sequences
    // This assumes the array is already sorted.
    // TODO rename to "custom" something
    public static bool Contains(List<int> arrayToCheck, int value)
    {
        var right = arrayToCheck.Count - 1;
        var left = 0;
        while (left < right)
        {
            var middle = left + (right - left) / 2;
            if (arrayToCheck[middle] == value)
                return true;
            if (arrayToCheck[middle] < value)
                left = middle + 1;
            if (arrayToCheck[middle] > value)
                right = middle - 1;
        }

        return false;
    }

    public static int GetIndexAt(List<int> arrayToCheck, int value)
    {
        var right = arrayToCheck.Count - 1;
        var left = 0;
        while (left <= right)
        {
            var middle = left + (right - left) / 2;
            if (arrayToCheck[middle] == value)
                return middle;
            if (arrayToCheck[middle] < value)
                left = middle + 1;
            else
                right = middle - 1;
        }

        return -1;
    }

    public static List<int> GetIndices(List<int> arrayToCheck, List<int> values, bool oneIndex = false)
    {
        var output = new List<int>();
        foreach (var value in values)
        {
            var index = GetIndexAt(arrayToCheck, value);
            if (oneIndex)
                output.Add(index == -1 ? -1 : index + 1);
            else
                output.Add(index);
        }

        return output;
    }
}