using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.Sequence.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bio.Math;

namespace Bio.Sequence.Types.Tests
{
    [TestClass()]
    public class ProteinSequenceTests
    {
        [TestMethod()]
        public void ProteinSequenceTest()
        {
            var protein = new ProteinSequence("SKADYEK");
            Assert.IsTrue(Helpers.DoublesEqualWithinRange(821.392, protein.MolecularWeight));
        }
    }
}