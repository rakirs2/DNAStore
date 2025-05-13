using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.Analysis.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bio.IO;

namespace Bio.Analysis.Types.Tests
{
    [TestClass()]
    public class OverlapGraphTests
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "../../../../BioTests/TestData/OverlapFastas.fasta");

        [TestMethod()]
        public void OverlapGraphTest()
        {
            var result = new OverlapGraph(FastaParser.Read(_filePath), 3);
            Assert.AreEqual(3, result.GetOverlaps().Count());
        }
    }
}