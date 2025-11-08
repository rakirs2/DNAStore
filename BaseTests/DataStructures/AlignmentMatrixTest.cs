
using Base.DataStructures;

namespace BaseTests.DataStructures;

[TestClass]
public class AlignmentMatrixTest
{
    [TestMethod]
    public void Existence()
    {
        Assert.IsNotNull(new AlignmentMatrix("a", "a"));
    }
}