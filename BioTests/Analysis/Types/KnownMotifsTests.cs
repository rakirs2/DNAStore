using Bio.Analysis.Types;

namespace BioTests.Analysis.Types;
public class KnownMotifsTests
{
    [TestMethod]
    public void NGlycostatin()
    {
        Assert.IsTrue(KnownMotifs.NGlycostatin.IsMatch("NNSN"));
        Assert.IsFalse(KnownMotifs.NGlycostatin.IsMatchStrict("NNSNA"));
    }
}
