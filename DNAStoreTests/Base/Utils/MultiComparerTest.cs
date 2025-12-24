using Base.Utils;
using JetBrains.Annotations;

namespace BaseTests.Utils;

[TestClass]
[TestSubject(typeof(MultiComparer))]
public class MultiComparerTest
{
    [TestMethod]
    public void Base()
    {
        Assert.ThrowsExactly<ArgumentNullException>(()=>MultiComparer.Min(null));
    }

    [TestMethod]
    public void ZeroLength()
    {
        Assert.ThrowsExactly<ArgumentException>(()=>MultiComparer.Min());
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