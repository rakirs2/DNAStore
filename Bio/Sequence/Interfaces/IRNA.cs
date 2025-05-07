using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bio.Sequence.Types;

namespace Bio.Sequence.Interfaces
{
    public interface IRNA : ISequence
    {
        /// <summary>
        /// Returns a protein sequence from a given RNA strand.
        /// </summary>
        /// <returns></returns>
        public string GetExpectedProteinString();

    }
}
