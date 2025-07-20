using Base.Utils;

namespace BaseTests.Utils;

[TestClass]
public class CaseInsensitiveCharComparerTests
{
    private readonly CaseInsensitiveCharComparer _comparer = new();

    [TestMethod]
    public void BaseEqualityTests()
    {
        Assert.IsTrue(_comparer.Equals('a', 'A'), "Cases should be equivalent");
        Assert.IsTrue(_comparer.Equals('a', 'a'), "Cases should be equivalent");
    }

    [TestMethod]
    public void HashCode()
    {
        var lowerCase = 'a';
        var upperCase = 'A';
        Assert.AreEqual(_comparer.GetHashCode(lowerCase), _comparer.GetHashCode(upperCase));
    }
}