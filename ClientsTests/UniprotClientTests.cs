using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;

namespace Bio.Clients.Tests
{
    [TestClass]
    public class UniprotClientTests
    {
        [TestMethod]
        public void GetAsyncTest()
        {
            // TODO: need to learn this later, but this can't be ideal
            var result = UniprotClient.GetAsync("http://www.uniprot.org/uniprot/B5ZC00.fasta");
            Assert.IsNotNull(result.Result);
        }
    }
}