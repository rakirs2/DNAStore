namespace DnaStore.Base.Interfaces;

public interface ITrie
{
    /// <summary>
    ///     Tracked on insertion. It is
    /// </summary>
    public int MaxStringLength { get; }

    /// <summary>
    ///     Adds a specific word the Trie. Duplicates are allowed
    /// </summary>
    /// <param name="word"></param>
    public void AddWord(string word);


    /// <summary>
    ///     Returns if a given word completely exists as a terminal point in the structure
    /// </summary>
    /// <returns></returns>
    public bool Search(string wordToSearch);
}