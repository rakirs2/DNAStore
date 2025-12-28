using MathNet.Numerics;

namespace DNAStore.BioMath;

public class Markov
{
    /// <summary>
    /// Pr(pi) = product of probabilites pi i-1, i = product t(pi i-1, i)
    /// TODO: consider this as as state machine. Not sure if these should be static yet
    /// </summary>
    /// <param name="pi"></param>
    /// <param name="states"></param>
    /// <param name="transition"></param>
    /// <returns></returns>
    public static double HiddenPathProbability(string pi, char[] states, double[,] transition)
    {
        var output = 1.0/states.Length;
        if(states.Distinct().Count() != states.Length)
            throw new InvalidDataException("All states must be unique");

        if (transition.GetLength(0) != states.Length || transition.GetLength(1)!= states.Length)
            throw new InvalidDataException("Transition array must have the correct dimensions");
            
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