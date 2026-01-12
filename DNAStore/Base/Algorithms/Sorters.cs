namespace DNAStore.Base.Algorithms;

public static class Sorters<T> where T : IComparable<T>
{
    public static long InPlaceMergeSort(ref T[] array)
    {
        return MergeSort(ref array, 0, array.Length - 1);
    }

    private static long MergeSort(ref T[] array, int left, int right)
    {
        if (left >= right) return 0;
        var mid = (left + right) / 2;

        var leftInv = MergeSort(ref array, left, mid);
        var rightInv = MergeSort(ref array, mid + 1, right);

        return leftInv + rightInv + Merge(array, left, mid, right);
    }

    private static long Merge(T[] arr, int left, int mid, int right)
    {
        var n1 = mid - left + 1;
        var n2 = right - mid;

        var tempLeftArray = new T[n1];
        var tempRightArray = new T[n2];

        for (var x = 0; x < n1; x++) tempLeftArray[x] = arr[left + x];

        for (var y = 0; y < n2; y++)
            tempRightArray[y] = arr[mid + 1 + y];

        var i = 0;
        var j = 0;
        long inversions = 0;
        var k = left;

        while (i < n1 && j < n2)
        {
            if (tempLeftArray[i].CompareTo(tempRightArray[j]) <= 0)
            {
                arr[k] = tempLeftArray[i];
                i++;
            }
            else
            {
                arr[k] = tempRightArray[j];
                inversions += tempLeftArray.Length - i;
                j++;
            }

            k++;
        }

        while (i < n1)
        {
            arr[k] = tempLeftArray[i];
            i++;
            k++;
        }

        while (j < n2)
        {
            arr[k] = tempRightArray[j];
            j++;
            k++;
        }

        return inversions;
    }

    public static T[] Merge2SortedArrays(T[] a, T[] b)
    {
        if (a.Length == 0) return b;
        if (b.Length == 0) return a;

        var result = new T[a.Length + b.Length];
        MergeRecursiveHelper(a, b, result, 0, 0, 0);
        return result;
    }

    private static void MergeRecursiveHelper(T[] arr1, T[] arr2, T[] result, int i, int j, int k)
    {
        // Base case: If all elements from both arrays have been processed
        if (i >= arr1.Length && j >= arr2.Length) return;

        if (i >= arr1.Length)
        {
            result[k] = arr2[j];
            MergeRecursiveHelper(arr1, arr2, result, i, j + 1, k + 1);
            return;
        }

        if (j >= arr2.Length)
        {
            result[k] = arr1[i];
            MergeRecursiveHelper(arr1, arr2, result, i + 1, j, k + 1);
            return;
        }

        if (arr1[i].CompareTo(arr2[j]) <= 0)
        {
            result[k] = arr1[i];
            MergeRecursiveHelper(arr1, arr2, result, i + 1, j, k + 1);
        }
        else
        {
            result[k] = arr2[j];
            MergeRecursiveHelper(arr1, arr2, result, i, j + 1, k + 1);
        }
    }

    // TODO: maybe move this to an int sorter class
    // TODO: how to add a d priority interface?
    public static List<int> HeapSortMin(IEnumerable<int> values, int? max = null)
    {
        var heap = new PriorityQueue<int, int>();
        foreach (var value in values)
            // TODO: if this ever gets reused, clean this up
            // we're dealing with min heap, we want max
            heap.Enqueue(value, value);

        var outputList = new List<int>();
        var howMany = max ?? heap.Count;
        var i = 0;
        while (i < howMany)
        {
            outputList.Add(heap.Dequeue());
            i++;
        }

        return outputList;
    }
}