using Bio.Math;

namespace BioTests.Math;

[TestClass]
public class HelpersTests
{
    [TestMethod]
    public void GenerationalGrowthTest()
    {
        Assert.AreEqual(19, Helpers.GenerationalGrowth(5, 3));
    }

    [TestMethod]
    public void GenerationalGrowthTestWithNoNewRabbits()
    {
        Assert.AreEqual(1, Helpers.GenerationalGrowth(2, 34));
    }

    [TestMethod]
    public void GenerationalGrowthTestSolution()
    {
        Assert.AreEqual(3048504677680, Helpers.GenerationalGrowth(36, 3));
    }
}