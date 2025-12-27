using DNAStore.Sequences.Analysis.Interfaces;
using DNAStore.Sequences.IO;
using DNAStore.Sequences.Types;

namespace DNAStore.Sequences.Analysis.Types;

public class LongestCommonSubsequence : ILongestCommonSubsequence
{
    private readonly Sequence _longest;

    public LongestCommonSubsequence(List<Fasta> fastas)
    {
        var first = fastas[0];
        for (var i = first.Length; i > 0; i--)
        {
            if (_longest != null) break;
            for (var start = 0; start < first.Length - i; start++)
            {
                var currentString = first.RawSequence.Substring(start, (int)i);
                var isValid = true;
                foreach (var fasta in fastas[1..])
                    if (!fasta.RawSequence.Contains(currentString))
                    {
                        isValid = false;
                        break;
                    }

                if (isValid)
                    _longest = new Sequence(currentString);
            }
        }
        // to generate each possible subsequence
    }

    public Sequence GetAnyLongest()
    {
        return _longest;
    }
}