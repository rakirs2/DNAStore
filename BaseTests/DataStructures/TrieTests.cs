namespace Base.DataStructures.Tests;

[TestClass]
public class TrieTests
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
}