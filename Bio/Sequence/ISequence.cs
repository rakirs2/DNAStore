using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio.Sequence;

public interface ISequence
{
    long Length { get; }
    string RawSequence { get; }
}