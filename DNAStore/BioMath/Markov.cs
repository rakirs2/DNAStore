namespace DNAStore.BioMath;

public class Markov
{
    /// <summary>
    /// Pr(pi) = product of probabilites pi i-1, i = product t(pi i-1, i)
    /// </summary>
    /// <param name="pi"></param>
    /// <param name="states"></param>
    /// <param name="transition"></param>
    /// <returns></returns>
    public static double HiddenPathProbability(string pi, char[] states, double[,] transition)
    {
        var output = (double)1.0/states.Length;
        Dictionary<char, int> statesIndex = new Dictionary<char, int>();
        var idx = 0;
        foreach (var state in states)
        {
            statesIndex[state] = idx;
            idx++;
        }
        
        for (int i = 1; i < pi.Length; i++)
        {
            output *= transition[statesIndex[pi[i-1]], statesIndex[pi[i]]];
        }

        return output;
    }
}