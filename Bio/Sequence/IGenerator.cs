using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio.Sequence
{
    public interface IGenerator
    {
        /// <summary>
        /// Create a sequence of a given type
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        ISequence Create(long length);
    }
}
