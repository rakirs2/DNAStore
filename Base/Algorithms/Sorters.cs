namespace Base.Algorithms;

public class Sorters<T> where T : IComparable<T>
{
    // The main function that sorts the array arr[left...right]
    public static void MergeSort(ref T[] array)
    {
        MergeSort(ref array, 0, array.Length - 1);
    }

    private static void MergeSort(ref T[] array, int left, int right)
    {
        // Base case: a list with 0 or 1 element is already sorted
        if (left >= right) return;
        // Find the middle point
        var mid = (left + right) / 2;

        // Recursively sort first and second halves
        MergeSort(ref array, left, mid);
        MergeSort(ref array, mid + 1, right);

        // Merge the sorted halves
        Merge(array, left, mid, right);
    }

    // Merges two subarrays of arr[]
    // First subarray is arr[left...mid]
    // Second subarray is arr[mid+1...right]
    private static void Merge(T[] arr, int left, int mid, int right)
    {
        // Calculate lengths of the two subarrays
        var n1 = mid - left + 1;
        var n2 = right - mid;

        // Create temporary arrays
        var leftArray = new T[n1];
        var rightArray = new T[n2];

        // Copy data to temporary arrays LeftArray[] and RightArray[]
        for (var x = 0; x < n1; x++) leftArray[x] = arr[left + x];

        for (var y = 0; y < n2; y++)
            rightArray[y] = arr[mid + 1 + y];

        // Merge the temporary arrays back into arr[left...right]
        var i = 0;
        var j = 0; // Initial indexes of first and second subarrays
        var k = left; // Initial index of merged subarray

        while (i < n1 && j < n2)
        {
            if (leftArray[i].CompareTo(rightArray[j]) <= 0)
            {
                arr[k] = leftArray[i];
                i++;
            }
            else
            {
                arr[k] = rightArray[j];
                j++;
            }

            k++;
        }

        // Copy the remaining elements of LeftArray[], if any
        while (i < n1)
        {
            arr[k] = leftArray[i];
            i++;
            k++;
        }

        // Copy the remaining elements of RightArray[], if any
        while (j < n2)
        {
            arr[k] = rightArray[j];
            j++;
            k++;
        }
    }

    public static T[] Merge2SortedArrays(T[] a, T[] b)
    {
        // Handle edge cases where one or both arrays are empty
        if (a == null || a.Length == 0) return b;
        if (b == null || b.Length == 0) return a;

        // Create a new array to store the merged result
        var result = new T[a.Length + b.Length];

        // Call the recursive helper function
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