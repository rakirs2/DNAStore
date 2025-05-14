using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.Analysis.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bio.IO;

namespace Bio.Analysis.Types.Tests
{
    [TestClass()]
    public class LongestCommonSubsequenceTests
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "../../../../BioTests/TestData/LongestSubsequence.fasta");

        [TestMethod()]
        public void LongestCommonSubsequenceTest()
        {
            var result = new LongestCommonSubsequence(FastaParser.Read(_filePath));
            Assert.AreEqual("AC", result.GetAnyLongest().RawSequence);
        }
    }
}