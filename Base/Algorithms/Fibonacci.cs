namespace Base.Algorithms;

public static class Fibonacci
{
    public static int GetNth(int i)
    {
        if (i < 1)
            throw new InvalidDataException();

        if (i == 1) return 1;

        if (i == 2) return 1;
        var tracker = new int[i];
        tracker[0] = 1;
        tracker[1] = 1;
        for (var j = 2; j < i; j++) tracker[j] = tracker[j - 1] + tracker[j - 2];
        return tracker.Last();
    }
}