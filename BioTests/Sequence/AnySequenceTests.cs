using Bio.Sequence;

namespace BioTests.Sequence
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