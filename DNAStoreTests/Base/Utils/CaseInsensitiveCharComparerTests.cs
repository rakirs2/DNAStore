using DNAStore.Base.Utils;

namespace BaseTests.Base.Utils;

[TestClass]
public class CaseInsensitiveCharComparerTests
{
    [TestMethod]
    public void BaseEqualityTests()
    {
        Assert.IsTrue(CaseInsensitiveCharComparer.Shared.Equals('a', 'A'), "Cases should be equivalent");
        Assert.IsTrue(CaseInsensitiveCharComparer.Shared.Equals('a', 'a'), "Cases should be equivalent");
    }

    [TestMethod]
    public void HashCode()
    {
        var lowerCase = 'a';
        var upperCase = 'A';
        Assert.AreEqual(CaseInsensitiveCharComparer.Shared.GetHashCode(lowerCase),
            CaseInsensitiveCharComparer.Shared.GetHashCode(upperCase));
    }
}