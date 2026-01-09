using MathNet.Numerics;

namespace DNAStore.BioMath;

public static class Markov
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
            
        var statesIndex = new Dictionary<char, int>();
        var idx = 0;
        foreach (var state in states)
        {
            statesIndex[state] = idx;
            idx++;
        }
        
        for (var i = 1; i < pi.Length; i++)
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

        int idx;
        var alphabetIndex = ValuesToIndex(sigma);

        var statesIndex = ValuesToIndex(states);
        
        for (var i = 0; i < outcome.Length; i++)
        {
            output *=  emission[statesIndex[pi[i]], alphabetIndex[outcome[i]]];
        }

        return output;
    }

    // TODO: rename and/or decide if Iw ant this as an external helper
    private static Dictionary<char, int> ValuesToIndex(char[] sigma)
    {
        var alphabetIndex = new Dictionary<char, int>();
        var idx = 0;
        foreach (var s in sigma)
        {
            alphabetIndex[s] = idx;
            idx++;
        }

        return alphabetIndex;
    }

    /// <summary>
    ///     Generates the highest likelihood path given the following parameters
    /// </summary>
    /// <remarks>
    ///     Invented by the same guy who USC's engineering school is named after
    ///     AKA Qualcomm CEO
    /// </remarks>
    /// <returns></returns>
    public static string ViterbiAlgorithm(
        string x, 
        char[] sigma, 
        char[] states, 
        double[,] transmission,
        double[,] emission)
    {
        int n = x.Length;
        int m = states.Length;

        // standard that we've used before.
        var alphabetIndex = ValuesToIndex(sigma);

        var viterbiLog = new double[m, n];
        var backPointers = new int[m, n];

        double initialLogProb = Math.Log(1.0 / m);
        int firstObsIdx = alphabetIndex[x[0]];

        for (var s = 0; s < m; s++) viterbiLog[s, 0] = initialLogProb + Math.Log(emission[s, firstObsIdx]);

        for (var t = 1; t < n; t++)
        {
            int obsIdx = alphabetIndex[x[t]];
            for (var s = 0; s < m; s++)
            {
                double logEmiss = Math.Log(emission[s, obsIdx]);
                double maxLogProb = double.NegativeInfinity;
                var bestPrevState = 0;

                for (var prevS = 0; prevS < m; prevS++)
                {
                    double currentLogProb = viterbiLog[prevS, t - 1] + Math.Log(transmission[prevS, s]) + logEmiss;
                    if (currentLogProb > maxLogProb)
                    {
                        maxLogProb = currentLogProb;
                        bestPrevState = prevS;
                    }
                }

                viterbiLog[s, t] = maxLogProb;
                backPointers[s, t] = bestPrevState;
            }
        }

        var currStateIdx = 0;
        double maxFinalLog = double.NegativeInfinity;
        for (var s = 0; s < m; s++)
            if (viterbiLog[s, n - 1] > maxFinalLog)
            {
                maxFinalLog = viterbiLog[s, n - 1];
                currStateIdx = s;
            }

        var path = new char[n];
        path[n - 1] = states[currStateIdx];

        for (int t = n - 1; t > 0; t--)
        {
            currStateIdx = backPointers[currStateIdx, t];
            path[t - 1] = states[currStateIdx];
        }

        return new string(path);
    }
}