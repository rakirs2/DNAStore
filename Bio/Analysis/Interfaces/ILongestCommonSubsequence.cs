using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bio.Sequence.Types;

namespace Bio.Analysis.Interfaces
{
    // Only cares about 1, not all
    public interface ILongestCommonSubsequence
    {
        /// <summary>
        /// Returns any longest subsequence in the List
        /// </summary>
        /// <returns></returns>
        AnySequence GetAnyLongest();
    }
}
