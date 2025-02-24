using Bio.Sequence;

namespace BioTests.Sequence;

[TestClass()]
public class SequenceHelpersTests
{
    // TODO there might be a corruptoin angle with all of these as well. For now, focus just on making sure
    // these methods work
    private static HashSet<char> KnownProteinSequenceDifferentiators = new HashSet<char>() { 'E', 'F', 'I', 'L', 'P', 'Q', 'Z', 'X', '*' };
    [TestMethod]
    public void IsRNADifferentiatorFalse()
    {
        Assert.IsFalse(SequenceHelpers.IsKnownRNADifferentiator('a'));
    }

    [TestMethod]
    public void IsRNADifferentatiorTrue()
    {
        Assert.IsTrue(SequenceHelpers.IsKnownRNADifferentiator('U'));
    }

    [TestMethod]
    public void IsRNADifferentatiorCaseSensitive()
    {
        Assert.IsTrue(SequenceHelpers.IsKnownRNADifferentiator('u'));
    }

    [TestMethod]
    public void IsProteinSequenceDifferentiator()
    {
        foreach (char c in KnownProteinSequenceDifferentiators)
        {
            Assert.IsTrue(SequenceHelpers.IsKnownProteinSequenceDifferentiator(c));
        }
    }

    [TestMethod]
    public void IsProteinSequenceDifferentiatorCaseSensitive()
    {
        foreach (char c in KnownProteinSequenceDifferentiators)
        {
            Assert.IsTrue(SequenceHelpers.IsKnownProteinSequenceDifferentiator(char.ToLowerInvariant(c)));
        }
    }

    [TestMethod]
    public void IsProteinSequenceDifferentiatorAmbiguousCharactersReturnsFalse()
    {
        Assert.IsFalse(SequenceHelpers.IsKnownProteinSequenceDifferentiator('u'));
    }
}
