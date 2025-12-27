using DNAStore.Base.Interfaces;

namespace DNAStore.Base.DataStructures;

public class Trie : ITrie
{
    private readonly TrieNode root;

    public Trie()
    {
        root = new TrieNode();
    }

    public void AddWord(string word)
    {
        // Go ahead and verify the maximum length here
        MaxStringLength = Math.Max(word.Length, MaxStringLength);

        var node = root;
        foreach (var c in word)
        {
            if (!node.Children.ContainsKey(c))
                node.Children.Add(c, new TrieNode());
            node = node.Children[c];
        }

        node.IsTerminus = true;
    }

    public bool Search(string wordToCheck)
    {
        // Go ahead and quick fail for this scenario
        if (wordToCheck.Length > MaxStringLength) return false;

        var temp = root;
        foreach (var character in wordToCheck)
        {
            if (!temp.Children.ContainsKey(character)) return false;

            temp = temp.Children[character];
        }

        return temp.IsTerminus;
    }

    public int MaxStringLength { get; private set; }

    private class TrieNode
    {
        public readonly Dictionary<char, TrieNode> Children;

        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
        }

        public bool IsTerminus { get; set; }
    }
}