using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types.Tests
{
    [TestClass]
    public class KmerCounterTests
    {
        // TODO: what does eee get counted 2 or 3x -- > is there a DNA related
        // example here?
        private AnySequence sequence = new AnySequence("aabbccddeeffeeggee");
        [TestMethod]
        public void KmerCounterTest()
        {
            var kmerCounter = new KmerCounter(sequence, 2);
            Assert.AreEqual(2, kmerCounter.KmerLength);
            Assert.AreEqual("ee", kmerCounter.HighestFrequencyKmer);
            Assert.AreEqual(3, kmerCounter.CurrentHighestFrequency);
        }
    }
}