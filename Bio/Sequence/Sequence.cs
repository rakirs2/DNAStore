using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio.Sequence;

public abstract class Sequence : ISequence
{
    public long Length { get; private set; }
    public string RawSequence { get; private set; }

    protected Sequence(string rawSequence)
    {
        foreach (var basePair in rawSequence)
            if (IsValid(basePair))
                Counts.Add(basePair);
            else
                throw new Exception();
    }

    public BasePairDictionary Counts = new();

    // Runs before each value, ensure that the sequence upon store is valid for a given typing
    protected abstract bool IsValid(char bp);
}