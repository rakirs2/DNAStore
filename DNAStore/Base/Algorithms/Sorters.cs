namespace Base.Algorithms;

public static class Sorters<T> where T : IComparable<T>
{
    public static long InPlaceMergeSort(ref T[] array)
    {
        return MergeSort(ref array, 0, array.Length - 1);
    }

    private static long MergeSort(ref T[] array, int left, int right)
    {
        if (left >= right) return 0;
        int mid = (left + right) / 2;

        long leftInv = MergeSort(ref array, left, mid);
        long rightInv = MergeSort(ref array, mid + 1, right);

        return leftInv + rightInv + Merge(array, left, mid, right);
    }

    private static long Merge(T[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        var tempLeftArray = new T[n1];
        var tempRightArray = new T[n2];

        for (var x = 0; x < n1; x++) tempLeftArray[x] = arr[left + x];

        for (var y = 0; y < n2; y++)
            tempRightArray[y] = arr[mid + 1 + y];

        var i = 0;
        var j = 0;
        long inversions = 0;
        int k = left;

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
        if (a == null || a.Length == 0) return b;
        if (b == null || b.Length == 0) return a;

        var result = new T[a.Length + b.Length];
        MergeRecursiveHelper(a, b, result, 0, 0, 0);
        return result;
    }

    private static void MergeRecursiveHelper(T[] arr1, T[] arr2, T[] result, int i, int j, int k)
    {
        // Base case: If all elements from both arrays have been processed
        if (i >= arr1.Length && j >= arr2.Length) return;

        // If all elements from arr1 have been processed, append remaining from arr2
        if (i >= arr1.Length)
        {
            result[k] = arr2[j];
            MergeRecursiveHelper(arr1, arr2, result, i, j + 1, k + 1);
            return;
        }

        // If all elements from arr2 have been processed, append remaining from arr1
        if (j >= arr2.Length)
        {
            result[k] = arr1[i];
            MergeRecursiveHelper(arr1, arr2, result, i + 1, j, k + 1);
            return;
        }

        // Compare elements and place the smaller one into the result array
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
}