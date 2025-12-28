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
    
    public static double PathOutcomeProbability(string outcome,char[] sigma, string pi, char[] states, double[,] emission)
    {
        var output = 1.0;
        if(sigma.Distinct().Count() != sigma.Length)
            throw new InvalidDataException("alphabet must be unique");
        
        if(states.Distinct().Count() != states.Length)
            throw new InvalidDataException("All states must be unique");

        if (emission.GetLength(0) != states.Length || emission.GetLength(1)!= sigma.Length)
            throw new InvalidDataException("Emission array must have the correct dimensions");
            
        Dictionary<char, int> alphabetIndex = new Dictionary<char, int>();
        var idx = 0;
        foreach (var s in sigma)
        {
            alphabetIndex[s] = idx;
            idx++;
        }
        
        Dictionary<char, int> statesIndex = new Dictionary<char, int>();
        idx = 0;
        foreach (var state in states)
        {
            statesIndex[state] = idx;
            idx++;
        }
        
        for (var i = 0; i < outcome.Length; i++)
        {
            output *=  emission[statesIndex[pi[i]], alphabetIndex[outcome[i]]];
        }

        return output;
    }
}