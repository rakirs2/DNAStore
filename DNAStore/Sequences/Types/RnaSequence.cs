using System.Numerics;
using DNAStore.Base.Utils;
using DNAStore.BioMath;
using DNAStore.Sequences.Types.Interfaces;

namespace DNAStore.Sequences.Types;

public class RnaSequence : NucleotideSequence, IRna
{
    private static readonly HashSet<char> pyrimidines = new(CaseInsensitiveCharComparer.Shared) { 'C', 'T' };
    private static readonly HashSet<char> purines = new(CaseInsensitiveCharComparer.Shared) { 'A', 'G' };

    public RnaSequence(string rawSequence) : base(rawSequence)
    {
        
    }

    public RnaSequence(string rawSequence, string name) : base(rawSequence, name)
    {
    }

    protected override HashSet<char> Pyrimidines => pyrimidines;

    protected override HashSet<char> Purines => purines;
    
    // TODO: need one that only has one stop
    public static RnaSequence GenerateRandom(int length)
    {
        return new RnaSequence(StringUtils.GenerateRandomString(length, SequenceHelpers.AllRNAMarkers.ToList() ));
    }

    public static RnaSequence GenerateRandomGapped(int length)
    {
        return new RnaSequence(StringUtils.GenerateRandomString(length, SequenceHelpers.AllRNAMarkersGapped.ToList() ));
    }

    public string GetExpectedProteinString()
    {
        return SequenceHelpers.ConvertStringToProtein(ToString());
    }

    public int GetPotentialNumberOfDNAStrings(int mod)
    {
        throw new NotImplementedException();
    }

    public BigInteger NumberOfPerfectMatchings()
    {
        if (IsBalanced())
        {
            var gcFreq = Probability.Factorial((uint)Counts.GetFrequency('C'));
            var auFreq = Probability.Factorial((uint)Counts.GetFrequency('A'));

            return gcFreq * auFreq;
        }

        throw new ArgumentException("The AC and GC counts must be equal for this analysis");
    }

    public int NumberOfPerfectMatchingsDynamic(int modulo = 1000000)
    {
        var dp = new Dictionary<string, int>();
        NumberOfPerfectMatchingsDynamicInternal(dp, modulo);
        if (string.IsNullOrEmpty(RawSequence)) return 1;
        return dp.Values.Max() % modulo;
    }

    private int NumberOfPerfectMatchingsDynamicInternal(Dictionary<string, int> dp, int modulo = 1000000)
    {
        if (string.IsNullOrEmpty(RawSequence)) return 1;
        if (!IsBalanced()) return 0;
        // check the cache
        if (dp.TryGetValue(RawSequence, out var cached)) return cached;

        int total = 0;
        char first = RawSequence[0];
        
        // We assume that there's some magical pivot point where we split the graph. The left and right sides now become their own sub problems.
        for (int k = 1; k < RawSequence.Length; k += 2)
        {
            char target = RawSequence[k];
            
            if (IsValidPair(first, target))
            {
                var left = new RnaSequence(RawSequence.Substring(1, k - 1));
                var right = new RnaSequence(RawSequence.Substring(k + 1));

                if (left.IsBalanced())
                {
                    total = (total + left.NumberOfPerfectMatchingsDynamicInternal(dp) * right.NumberOfPerfectMatchingsDynamicInternal(dp)) ;
                }
            }
        }

        dp[RawSequence] = total;
        return total % modulo;
    }

    // TODO: refactor this for the function
    static bool IsValidPair(char a, char b)
    {
        return (a == 'A' && b == 'U') || (a == 'U' && b == 'A') ||
               (a == 'C' && b == 'G') || (a == 'G' && b == 'C');
    }

    public bool IsBalanced()
    {
        return Counts['A'] == Counts['U'] &&
               Counts['C'] == Counts['G'];
    }

    protected override bool IsValid(char c)
    {
        return SequenceHelpers.IsValidRNA(c);
    }
}