namespace Base.Utils;

public static class StringUtils
{
    /// <summary>
    /// Swaps the characers of the passed in string
    /// </summary>
    /// <returns>
    /// a new string object with the indices swapped
    /// </returns>
    /// <param name="input"></param>
    /// <param name="indexA"></param>
    /// <param name="indexB"></param>
    public static string SwapIndex(string input, int indexA, int indexB)
    {
        if (input == null || indexA > input.Length - 1 || indexB > input.Length - 1 || indexA < 0 || indexB < 0)
            // TODO: I wonder why we don't have this in the base language
            // TODO: make errors clearer
            throw new InvalidDataException("Something wrong w the inputs");

        // Swaps characters in a string.
        var array = input.ToCharArray();
        (array[indexA], array[indexB]) = (array[indexB], array[indexA]);
        return new string(array);
    }
}