using Bio.Sequence;

namespace BioTests.Sequence;

[TestClass()]
public class SequenceHelpersTests
{
    // TODO: there might be a corruptoin angle with all of these as well. For now, focus just on making sure these methods work
    private static HashSet<char> KnownProteinSequenceDifferentiators =
        new() { 'E', 'F', 'I', 'L', 'P', 'Q', 'Z', 'X', '*' };

    [TestMethod]
    public void IsRNADifferentiatorFalse()
    {
        Assert.IsFalse(SequenceHelpers.IsKnownRNADifferentiator('a'));
    }

    [TestMethod]
    public void IsRNADifferentiatorTrue()
    {
        Assert.IsTrue(SequenceHelpers.IsKnownRNADifferentiator('U'));
    }

    [TestMethod]
    public void IsRNADifferentiatorCaseSensitive()
    {
        Assert.IsTrue(SequenceHelpers.IsKnownRNADifferentiator('u'));
    }

    [TestMethod]
    public void IsProteinSequenceDifferentiator()
    {
        foreach (var c in KnownProteinSequenceDifferentiators)
            Assert.IsTrue(SequenceHelpers.IsKnownProteinDifferentiator(c));
    }

    [TestMethod]
    public void IsProteinSequenceDifferentiatorCaseSensitive()
    {
        foreach (var c in KnownProteinSequenceDifferentiators)
            Assert.IsTrue(SequenceHelpers.IsKnownProteinDifferentiator(char.ToLowerInvariant(c)));
    }

    [TestMethod]
    public void IsProteinSequenceDifferentiatorAmbiguousCharactersReturnsFalse()
    {
        Assert.IsFalse(SequenceHelpers.IsKnownProteinDifferentiator('u'));
    }
}