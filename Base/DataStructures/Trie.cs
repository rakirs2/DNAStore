using Base.Interfaces;

namespace Base.DataStructures;
public class Trie : ITrie
{
    public Trie()
    {
        root = new TrieNode();
    }

    public void AddWord(string word)
    {
        // Go ahead and verify the maximum length here
        MaxStringLength = Math.Max(word.Length, MaxStringLength);

        var node = root;
        foreach (char c in word)
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
        if (wordToCheck.Length > MaxStringLength)
        {
            return false;
        }

        var temp = root;
        foreach (var character in wordToCheck)
        {
            if (!temp.Children.ContainsKey(character))
            {
                return false;
            }

            temp = temp.Children[character];
        }

        return temp.IsTerminus;
    }

    public int MaxStringLength { get; private set; }

    private TrieNode root;

    private class TrieNode
    {
        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
        }

        public bool IsTerminus
        {
            get => _isTerminus;
            set => _isTerminus = value;
        }

        public Dictionary<char, TrieNode> Children;

        private bool _isTerminus = false;
    }
}
