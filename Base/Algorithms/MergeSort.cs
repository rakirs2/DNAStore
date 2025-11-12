namespace Base.Algorithms;

public class MergeSort<T> where T : IComparable<T>
{
    public static T[] Merge2SortedArrays(T[] a, T[] b)
    {
        // Handle edge cases where one or both arrays are empty
        if (a == null || a.Length == 0)
        {
            return b;
        }
        if (b == null || b.Length == 0)
        {
            return a;
        }
        
        // Create a new array to store the merged result
        T[] result = new T[a.Length + b.Length];

        // Call the recursive helper function
        MergeRecursiveHelper(a, b, result, 0, 0, 0);

        return result;
    }
    
    private static void MergeRecursiveHelper(T[] arr1, T[] arr2, T[] result, int i, int j, int k)
    {
        // Base case: If all elements from both arrays have been processed
        if (i >= arr1.Length && j >= arr2.Length)
        {
            return;
        }

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
        if (arr1[i].CompareTo(arr2[j]) <=0)
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