using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio.Sequence.Interfaces
{
    public interface IProtein
    {
        /// <summary>
        /// Uses the underlying raw string to get the protein string
        /// </summary>
        /// <returns></returns>
        public string GetExpectedProteinString();
    }
}
