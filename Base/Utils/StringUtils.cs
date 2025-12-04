namespace Base.Utils;

public static class StringUtils
{
    /// <summary>
    ///     Swaps the characers of the passed in string
    /// </summary>
    /// <returns>
    ///     a new string object with the indices swapped
    /// </returns>
    /// <param name="input"></param>
    /// <param name="indexA"></param>
    /// <param name="indexB"></param>
    public static string SwapIndex(string input, int indexA, int indexB)
    {
        if (input == null || indexA > input.Length - 1 || indexB > input.Length - 1 || indexA < 0 || indexB < 0)
            throw new ArgumentException();

        // Swaps characters in a string.
        char[]? array = input.ToCharArray();
        (array[indexA], array[indexB]) = (array[indexB], array[indexA]);
        return new string(array);
    }
}