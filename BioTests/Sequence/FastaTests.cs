using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio.Sequence.Tests
{
    [TestClass]
    public class FastaTests
    {
        private const string _someName = "some Name";
        private const string _someIllegitimateSequence = "aaccttg";
        private readonly Dictionary<char, int> _expectedSequenceCounts = new Dictionary<char, int> { { 'a', 2 }, { 'c', 2 }, { 't', 2 }, { 'g', 1 } };


        [TestMethod]
        public void FastaConstructor()
        {
            var someFasta = new Fasta(_someName, _someIllegitimateSequence);
            Assert.IsNotNull(someFasta);
            Assert.AreEqual(someFasta.Name, _someName);
            Assert.AreEqual(someFasta.RawSequence, _someIllegitimateSequence);
            Assert.IsTrue(someFasta.Frequencies.Count == _expectedSequenceCounts.Count && !someFasta.Frequencies.Except(_expectedSequenceCounts).Any());
        }
    }
}