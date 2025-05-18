using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio.Analysis.Interfaces;

public interface IKmerCounter
{
    string HighestFrequencyKmer { get; }
    int KmerLength { get; }
}