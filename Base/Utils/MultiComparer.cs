namespace Base.Utils;

/// <summary>
///     Potentially stupid idea but let's see. Eventually expand and generalize this if I like the pattern.
/// </summary>
public class MultiComparer
{
    public static int Min(params int[] values)
    {
        BasicErrorChecking(values);

        return values.Min();
    }

    private static void BasicErrorChecking(int[] values)
    {
        if (values == null)
            throw new ArgumentNullException("Arguemnt can't be null");

        if (values.Length == 0) throw new ArgumentException("Array cannot beempty.", nameof(values));
    }

    public static int Max(params int[] values)
    {
        BasicErrorChecking(values);

        return values.Max();
    }
}