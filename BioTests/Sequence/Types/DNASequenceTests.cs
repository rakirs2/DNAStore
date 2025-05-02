using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.Sequence.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio.Sequence.Types.Tests
{
    [TestClass]
    public class DNASequenceTests
    {
        [TestMethod]
        public void TranscribeToRNATest()
        {
            var sequence = new DNASequence("GATGGAACTTGACTACGTAAATT");
            var rnaSequence = sequence.TranscribeToRNA();
            Assert.AreEqual("GAUGGAACUUGACUACGUAAAUU", rnaSequence.RawSequence);
        }

        [TestMethod]
        public void ReverseComplementTest()
        {
            var sequence = new DNASequence("AAAACCCGGT");
            var complement = sequence.ToReverseComplement();
            Assert.AreEqual("ACCGGGTTTT", complement.RawSequence);
        }

    }
}