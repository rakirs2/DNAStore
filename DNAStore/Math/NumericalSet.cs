namespace DnaStore.Math;

public class NumericalSet : ISet<int>
{
    public int MaxValue;

    public NumericalSet(int maxValue, List<int> values)
    {
        Values = new SortedSet<int>(values);
        MaxValue = maxValue;
    }

    public SortedSet<int> Values { get; }

    public void Add(int value)
    {
        Values.Add(value);
    }

    public void Remove(int value)
    {
        Values.Remove(value);
    }

    public override string ToString()
    {
        return $"{{{string.Join(", ", Values.ToArray())}}}";
    }

    public NumericalSet GetComplement()
    {
        var complementValues = new List<int>();
        for (var i = 1; i <= MaxValue; i++)
            if (!Values.Contains(i))
                complementValues.Add(i);

        return new NumericalSet(MaxValue, complementValues);
    }

    public static NumericalSet Union(NumericalSet a, NumericalSet b)
    {
        var output = new NumericalSet(a.MaxValue, a.Values.ToList());
        foreach (var value in b.Values) output.Add(value);

        return output;
    }

    public static NumericalSet Intersection(NumericalSet a, NumericalSet b)
    {
        var output = new NumericalSet(a.MaxValue, new List<int>());
        foreach (var value in b.Values)
            if (a.Values.Contains(value))
                output.Add(value);

        return output;
    }

    public static NumericalSet operator -(NumericalSet a, NumericalSet b)
    {
        var output = new NumericalSet(a.MaxValue, a.Values.ToList());
        foreach (var value in b.Values) output.Remove(value);

        return output;
    }

    public static NumericalSet operator +(NumericalSet a, NumericalSet b)
    {
        var output = new NumericalSet(a.MaxValue, a.Values.ToList());
        foreach (var value in b.Values) output.Add(value);

        return output;
    }
}