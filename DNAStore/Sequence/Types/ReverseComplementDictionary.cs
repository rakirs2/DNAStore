namespace Bio.Sequences.Types;

/// <summary>
///     Effectively allows for reverse complements to be searched, counted and handled
///     without needing to manipulate anything else.
/// </summary>
/// <remarks>
///     For now, this only works for a DNA object. It should be abstracted out and moved to the Base class
///     It effectively is a wrapper for the entire Dictionary class
/// </remarks>
public class ReverseComplementDictionary
{
    private readonly Dictionary<DnaSequence, int> _inputs = new();
    private readonly Dictionary<DnaSequence, int> _reverseComplements = new();

    #region Dictionary implementations

    /// <summary>
    ///     This method is currently the only way to change the dictionary. All adds must funnel through here
    ///     1 sequence at a time.
    /// </summary>
    /// <param name="key"></param>
    public void Add(DnaSequence key)
    {
        if (key.Length == 0) return;
        if (!_inputs.TryAdd(key, 1)) _inputs[key]++;

        var rc = key.GetReverseComplement();
        if (!_reverseComplements.TryAdd(rc, 1)) _reverseComplements[rc]++;
    }

    public int this[DnaSequence index]
    {
        get
        {
            _reverseComplements.TryGetValue(index, out int value1);
            _inputs.TryGetValue(index, out int value2);
            return value1 + value2;
        }
    }

    #endregion
}