namespace DNAStore.Base.Utils;

public static class IntegerArrayUtils
{
    public static int FindUnsignedBreakPoints(int[] current, int[] target)
    {
        var bp = 0;
        for (int i = 0; i < current.Length - 1; i++)
        {
            if (Math.Abs(Array.IndexOf(current, target[i]) - Array.IndexOf(current, target[i+1]))>1)
            {
                bp++;
            }
        }
        
        return bp;
    }
    
    public static void ReverseSubsequence(int[] s, int start, int end, bool signed = true)
    {
        if (s == null) throw new ArgumentNullException(nameof(s));

        int left = start, right = end;
        while (left <= right)
        {
            if(signed)
                (s[left], s[right]) = (-s[right], -s[left]);
            else
                (s[left], s[right]) = (s[right], s[left]);
            left++;
            right--;
        }
    }
}