using Bio.Analysis.Interfaces;
using Bio.IO;
using Bio.Sequence.Types;

namespace Bio.Analysis.Types;

public class LongestCommonSubsequence : ILongestCommonSubsequence
{
    public LongestCommonSubsequence(List<Fasta> fastas)
    {
        var first = fastas[0];
        for (long i = first.Length; i > 0; i--)
        {
            if (_longest != null) break;
            for (var start = 0; start < first.Length - i; start++)
            {
                string? currentString = first.RawSequence.Substring(start, (int)i);
                var isValid = true;
                foreach (var fasta in fastas[1..])
                    if (!fasta.RawSequence.Contains(currentString))
                    {
                        isValid = false;
                        break;
                    }

                if (isValid)
                    _longest = new AnySequence(currentString);
            }
        }
        // to generate each possible subsequence
    }

    public AnySequence GetAnyLongest()
    {
        return _longest;
    }

    private readonly AnySequence _longest;
}