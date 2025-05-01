using Bio.Sequence.Types;

namespace BioTests.Sequence.Types
{
    [TestClass]
    public class AnySequenceTests
    {
        [TestMethod]
        public void BasicConstruction()
        {
            var anySequence = new AnySequence("Somaf321434");
            Assert.IsNotNull(anySequence, "Object should construct");
            Assert.AreEqual(11, anySequence.Length);
        }
    }
}