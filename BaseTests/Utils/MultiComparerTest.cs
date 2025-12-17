using Base.Utils;
using JetBrains.Annotations;

namespace BaseTests.Utils;

[TestClass]
[TestSubject(typeof(MultiComparer))]
public class MultiComparerTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Base()
    {
        Assert.AreEqual(0, MultiComparer.Min(null));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ZeroLength()
    {
        Assert.AreEqual(0, MultiComparer.Min());
    }

    [TestMethod]
    public void Min()
    {
        Assert.AreEqual(1, MultiComparer.Min(1, 2, 3));
    }

    [TestMethod]
    public void Max()
    {
        Assert.AreEqual(3, MultiComparer.Max(1, 2, 3));
    }
}