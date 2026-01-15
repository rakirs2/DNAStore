using DNAStore.Base.DataStructures;

namespace DNAStoreTests.Base.DataStructures;

[TestClass]
public class TrieTest
{
    [TestMethod]
    public void TrieConstructionTest()
    {
        var simpleTrie = new Trie();
        Assert.AreEqual(0, simpleTrie.MaxStringLength);
    }

    [TestMethod]
    public void AddWordTest()
    {
        var simpleTrie = new Trie();
        simpleTrie.AddWord("ab");
        Assert.AreEqual(2, simpleTrie.MaxStringLength);
        Assert.IsTrue(simpleTrie.Search("ab"));
    }

    [TestMethod]
    public void SearchTest()
    {
        var simpleTrie = new Trie();
        simpleTrie.AddWord("ab");
        simpleTrie.AddWord("abc");
        simpleTrie.AddWord("abcd");
        Assert.IsTrue(simpleTrie.Search("abc"));
        Assert.IsFalse(simpleTrie.Search("abcde"));
    }

    [TestMethod]
    public void KeyNotFound()
    {
        var simpleTrie = new Trie();
        simpleTrie.AddWord("ab");
        simpleTrie.AddWord("abc");
        simpleTrie.AddWord("abcd");
        Assert.ThrowsExactly<KeyNotFoundException>(() => simpleTrie.GetWordsWithPrefix("dbcd"));
    }

    [TestMethod]
    public void GetListOfWordsWithPrefix()
    {
        var simpleTrie = new Trie();
        simpleTrie.AddWord("spade");
        simpleTrie.AddWord("spada");
        simpleTrie.AddWord("spadl");
        simpleTrie.AddWord("spacl");
        var expected = new HashSet<string>
        {
            "spade", "spada", "spadl"
        };
        var actual = simpleTrie.GetWordsWithPrefix("spad");
        Assert.IsTrue(expected.SetEquals(actual));
    }
}