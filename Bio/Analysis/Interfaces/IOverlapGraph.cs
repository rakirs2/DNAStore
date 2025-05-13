using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bio.IO;

namespace Bio.Analysis.Interfaces
{
    internal interface IOverlapGraph
    {
        public int Number { get; }
        public int MatchLength { get; }
        public List<Tuple<Fasta, Fasta>> GetOverlaps();
    }
}
