using Microsoft.VisualStudio.TestTools.UnitTesting;
using Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utils.Tests
{
    [TestClass()]
    public class CaseInsensitiveCharComparerTests
    {
        private readonly CaseInsensitiveCharComparer _comparer = new CaseInsensitiveCharComparer();
        
        [TestMethod()]
        public void BaseEqualityTests()
        {
            Assert.IsTrue(_comparer.Equals('a', 'A'), "Cases should be equivalent");
            Assert.IsTrue(_comparer.Equals('a', 'a'), "Cases should be equivalent");
        }

        [TestMethod()]
        public void HashCode()
        {
            char lowerCase = 'a';
            char upperCase = 'A';
            Assert.AreEqual(_comparer.GetHashCode(lowerCase), _comparer.GetHashCode(upperCase));
        }
    }
}