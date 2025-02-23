using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio.Tests
{
    [TestClass]
    public class UtilsTests
    {
        // TODO there might be a corruptoin angle with all of these as well. For now, focus just on making sure
        // these methods work
        private static HashSet<char> KnownProteinSequenceDifferentiators = new HashSet<char>() { 'E', 'F', 'I', 'L', 'P', 'Q', 'Z', 'X', '*' };
        [TestMethod]
        public void IsRNADifferentiatorFalse()
        {
            Assert.IsFalse(Utils.IsKnownRNADifferentiator('a'));
        }

        [TestMethod]
        public void IsRNADifferentatiorTrue()
        {
            Assert.IsTrue(Utils.IsKnownRNADifferentiator('U'));
        }

        [TestMethod]
        public void IsRNADifferentatiorCaseSensitive()
        {
            Assert.IsTrue(Utils.IsKnownRNADifferentiator('u'));
        }

        [TestMethod]
        public void IsProteinSequenceDifferentiator()
        {
            foreach (char c in KnownProteinSequenceDifferentiators)
            {
                Assert.IsTrue(Utils.IsKnownProteinSequenceDifferentiator(c));
            }
        }

        [TestMethod]
        public void IsProteinSequenceDifferentiatorCaseSensitive()
        {
            foreach (char c in KnownProteinSequenceDifferentiators)
            {
                Assert.IsTrue(Utils.IsKnownProteinSequenceDifferentiator(char.ToLowerInvariant(c)));
            }
        }

        [TestMethod]
        public void IsProteinSequenceDifferentiatorAmbiguousCharactersReturnsFalse()
        {
            Assert.IsFalse(Utils.IsKnownProteinSequenceDifferentiator('u'));
        }
    }
}