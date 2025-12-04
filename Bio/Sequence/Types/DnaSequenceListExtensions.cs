using BioMath;

namespace Bio.Sequence.Types;

/// <summary>
/// TODO: refactor this to be more 'object oriented' and improve understanding
/// Notes, trying out some "vibish" coding here. Algorithms aren't that hard to implement so I'm not minimizing learning
///
/// In general, it's really good at a constrained problem. However, integration is quite hard.
/// The code it generates isn't always extensible but it's close enough that it solves the prompt.
///
/// There are some caveats to this. However, one should not neglect the usefulness of this as a tool
/// and the pitfalls of this approach. If the goal is to maximize learning, it's a bad tool. If the goal is
/// to get unstuck, it's a good tool. If the goal is to mix and match getting things done with learning a little, it is
/// quite good. In general, I like the following approach:
///     1. Trying to write code fresh
///     2. Reverting to source material if stuck
///     3. Try again
///     4. Write unit tests
///     5. If still stuck, try one more code write
///     6. Ask AI
///     7. Verify AI results and understand
///     8. Try rewriting with the new approach.
///     9. If all else fails, copy, paste, and refactor.
/// </summary>
/// <remarks>
///     In basically 3 hours, 'solved' a lot of problems. I'm not sure I understand it much better compared to when
///     I started. I have gists, not deep knowledge.
/// </remarks>.
public static class DnaSequenceListExtensions
{
    private static readonly Dictionary<char, int> NucleotideToIndex = new()
    {
        { 'A', 0 }, { 'C', 1 }, { 'G', 2 }, { 'T', 3 }
    };

    /// <summary>
    ///     This is an incredibly greedy, n^2 implementation. We are just going to go down the list and concatenate the best
    ///     match
    ///     There are clearly better ways to do this. But for now, let's get something that works
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static DnaSequence GenerateLongestStringGreedy(this List<DnaSequence> list)
    {
        // create a shallow copy. We are not manipulating anything in the original list
        var copyOfList = list.ToList();

        // if there's only one, just return it.
        // We're going to stick with just the first string
        while (copyOfList.Count > 1)
        {
            int maxOverlap = -1;
            int bestI = -1, bestJ = -1;

            for (var i = 0; i < copyOfList.Count; i++)
            for (var j = 0; j < copyOfList.Count; j++)
            {
                if (i == j) continue;

                int currentOverlap = AnySequence.CalculateOverlap(copyOfList[i], copyOfList[j]);
                if (currentOverlap > maxOverlap)
                {
                    maxOverlap = currentOverlap;
                    bestI = i;
                    bestJ = j;
                }
            }

            if (maxOverlap == 0) // No overlaps, just concatenate remaining
            {
                var result = new DnaSequence("");
                foreach (var r in list) result += r;
                return result;
            }

            // Merge the two best overlapping reads
            string? mergedString = copyOfList[bestI] + copyOfList[bestJ][maxOverlap..];
            copyOfList.RemoveAt(bestI);
            // Adjust index if bestJ was after bestI
            if (bestJ > bestI) copyOfList.RemoveAt(bestJ - 1);
            else copyOfList.RemoveAt(bestJ);

            copyOfList.Add(new DnaSequence(mergedString));
        }

        return copyOfList[0]; // The single remaining superstring
    }

    /// <summary>
    ///     Generates a list of estimated error corrections. This is a deterministic algorithm.
    /// </summary>
    /// <remarks>
    ///     TODO: this should be generic and only in one location.
    ///     TODO: this should be handled by its own data structure
    /// </remarks>
    /// <param name="list"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static List<ErrorCorrection> GenerateErrorCorrections(this List<DnaSequence> list, int distance = 1)
    {
        var output = new List<ErrorCorrection>();
        var dict = new ReverseComplementDictionary();

        var needsReview = new HashSet<DnaSequence>();
        foreach (var item in list)
        {
            if (dict[item] == 0) needsReview.Add(item);

            dict.Add(item);
        }

        foreach (var item in needsReview)
            if (dict[item] == 1)
                // ok, we have something that need analysis
                // we have 2 options --> 1 go through the list of the options and see if there exists a valid point match
                // with the Hamming Distance 
                foreach (var knownRead in list)
                {
                    if (knownRead.Equals(item))
                        continue;

                    var rc = knownRead.GetReverseComplement();
                    if (rc.Equals(item)) continue;

                    if (AnySequence.HammingDistance(knownRead, item) == distance && dict[knownRead] >= 2)
                    {
                        output.Add(new ErrorCorrection(item, knownRead));
                        break;
                    }

                    if (AnySequence.HammingDistance(knownRead.GetReverseComplement(), item) == distance &&
                        dict[knownRead.GetReverseComplement()] >= 2)
                    {
                        output.Add(new ErrorCorrection(item, knownRead.GetReverseComplement()));
                        break;
                    }
                }

        return output;
    }

    /// <summary>
    ///     Generates the list of kmers that are within the hamming distance specified
    /// </summary>
    /// <param name="dnaSequences"></param>
    /// <param name="k"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static HashSet<string> MotifEnumeration(this List<DnaSequence> dnaSequences, int k, int distance)
    {
        var patterns = new HashSet<string>();
        var allPatterns = new HashSet<string>();
        foreach (var item in dnaSequences)
        {
            var currentKmers = item.KmerCompositionUniqueString(k);
            foreach (string? kmer in currentKmers)
            foreach (string? possible in new DnaSequence(kmer).DNeighborhood(distance))
                allPatterns.Add(possible);
        }

        foreach (string? item in allPatterns)
        {
            var inAll = true;
            foreach (var seq in dnaSequences)
                if (!seq.ContainsString(item, distance))
                {
                    inAll = false;
                    break;
                }

            if (inAll) patterns.Add(item);
        }

        return patterns;
    }

    /// <summary>
    ///     Returns the median string of the set of sequences
    /// </summary>
    /// <param name="sequences"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static List<string> MedianString(this List<DnaSequence> sequences, int size)
    {
        var kmers = Probability.GenerateAllKmers("ACGT", size);
        var results = new Dictionary<int, List<string>>();
        foreach (string? kmer in kmers)
        {
            var currentMin = int.MinValue;
            foreach (var sequence in sequences)
                currentMin = Math.Max(currentMin, sequence.GetMinimumDistanceForKmer(kmer));

            if (results.ContainsKey(currentMin))
                results[currentMin].Add(kmer);

            else
                results[currentMin] = [kmer];
        }

        return results[results.Keys.Min()];
    }

    public static List<string> GreedyMotifSearch(this List<DnaSequence> sequences, int k, int t,
        bool usePseudocounts = false)
    {
        var bestMotifs = new List<string>();
        foreach (var seq in sequences) bestMotifs.Add(seq.Substring(0, k));

        var firstDna = sequences[0];
        for (var i = 0; i <= firstDna.Length - k; i++)
        {
            string? motif1 = firstDna.Substring(i, k);
            var currentMotifs = new List<string> { motif1 };

            for (var j = 1; j < t; j++)
            {
                double[,] profile = usePseudocounts
                    ? CreateProfileWithPseudocounts(currentMotifs, k)
                    : CreateProfile(currentMotifs, k);

                string bestMatch = GetProfileMostProbableKmer(sequences[j], k, profile);
                currentMotifs.Add(bestMatch);
            }

            if (Score(currentMotifs) < Score(bestMotifs)) bestMotifs = currentMotifs;
        }

        return bestMotifs;
    }

    public static List<string> RandomMotifSearch(this List<DnaSequence> sequences, int k, int t, int iterations,
        bool usePseudocounts = true)
    {
        List<string> bestMotifs = null;
        var bestScore = int.MaxValue;

        for (var i = 0; i < iterations; i++)
        {
            var currentMotifs = RunSingleSearch(sequences, k, t, usePseudocounts);
            int currentScore = Score(currentMotifs);

            if (bestMotifs == null || currentScore < bestScore)
            {
                bestScore = currentScore;
                bestMotifs = currentMotifs;
            }
        }

        return bestMotifs;
    }

    public static List<string> GibbsSampler(this List<DnaSequence> sequences, int k, int t, int N, int randomStarts)
    {
        List<string> globalBestMotifs = null;
        var globalBestScore = int.MaxValue;

        // Gibbs Sampler is a randomized algorithm, so we run it multiple times 
        // to avoid getting stuck in a local optimum.
        for (var i = 0; i < randomStarts; i++)
        {
            List<string> currentBestMotifs = RunSingleGibbsPass(k, t, N, sequences);
            int currentScore = Score(currentBestMotifs);

            if (globalBestMotifs == null || currentScore < globalBestScore)
            {
                globalBestScore = currentScore;
                globalBestMotifs = currentBestMotifs;
            }
        }

        return globalBestMotifs;
    }

    private static List<string> RunSingleGibbsPass(int k, int t, int N, List<DnaSequence> dnaSequences)
    {
        // 1. Randomly select k-mers from each string to form initial Motifs
        List<string> motifs = RandomlyInitializeMotifs(k, dnaSequences);

        List<string> bestMotifs = new(motifs);
        int bestScore = Score(bestMotifs);

        // 2. Iterate N times
        for (var j = 0; j < N; j++)
        {
            // a. Randomly choose one sequence index (i) to exclude/update
            int i = random.Value.Next(t);

            // b. Create a profile from all motifs EXCEPT motif[i]
            List<string> subsetMotifs = new(motifs);
            subsetMotifs.RemoveAt(i); // Remove the motif at index i temporarily

            double[,] profile = CreateProfileWithPseudocounts(subsetMotifs, k);

            // c. Generate a new motif for sequence i based on the profile probabilities
            motifs[i] = ProfileRandomlyGeneratedKmer(dnaSequences[i], k, profile);

            // d. Check if the new set is better than the best seen in this run
            int currentScore = Score(motifs);
            if (currentScore < bestScore)
            {
                bestScore = currentScore;
                bestMotifs = new List<string>(motifs);
            }
        }

        return bestMotifs;
    }

    // Weighted Random Selection (Roulette Wheel Selection)
    private static string ProfileRandomlyGeneratedKmer(DnaSequence text, int k, double[,] profile)
    {
        // A hack
        var n = (int)text.Length;
        var probabilities = new List<double>();

        // 1. Calculate probability for every possible k-mer in the text
        for (var i = 0; i <= n - k; i++)
        {
            string kmer = text.Substring(i, k);
            var prob = 1.0;
            for (var j = 0; j < k; j++) prob *= profile[NucleotideToIndex[kmer[j]], j];
            probabilities.Add(prob);
        }

        // 2. Normalize and select randomly
        double sum = probabilities.Sum();
        double randomValue = random.Value.NextDouble() * sum;

        double currentSum = 0;
        for (var i = 0; i < probabilities.Count; i++)
        {
            currentSum += probabilities[i];
            if (currentSum >= randomValue) return text.Substring(i, k);
        }

        // Fallback (rare rounding edge case)
        return text.Substring(probabilities.Count - 1, k);
    }

    private static List<string> RunSingleSearch(List<DnaSequence> dna, int k, int t, bool usePseudocounts = true)
    {
        // 1. Randomly select initial k-mers (Motifs)
        List<string> motifs = RandomlyInitializeMotifs(k, dna);

        List<string> bestMotifs = new(motifs);
        int bestScore = Score(bestMotifs);

        // 2. Iteratively improve Motifs
        while (true)
        {
            // Create Profile with Pseudocounts (Laplace Succession)
            double[,] profile = usePseudocounts
                ? CreateProfileWithPseudocounts(motifs, k)
                : CreateProfile(motifs, k);

            // Form new Motifs based on the profile
            List<string> newMotifs = new();
            foreach (var seq in dna) newMotifs.Add(GetProfileMostProbableKmer(seq, k, profile));

            int currentScore = Score(newMotifs);

            // If score improves, update and continue; otherwise, we reached a local optimum
            if (currentScore < bestScore)
            {
                bestScore = currentScore;
                bestMotifs = newMotifs;
                motifs = newMotifs;
            }
            else
            {
                return bestMotifs;
            }
        }
    }

    private static List<string> RandomlyInitializeMotifs(int k, List<DnaSequence> dna)
    {
        List<string> randomMotifs = new();
        foreach (var seq in dna) randomMotifs.Add(seq.GetRandomKmer(k));
        return randomMotifs;
    }

    // Creates Profile Matrix with Laplace Pseudocounts (+1)
    private static double[,] CreateProfileWithPseudocounts(List<string> motifs, int k)
    {
        var profile = new double[4, k];
        int t = motifs.Count;

        for (var col = 0; col < k; col++)
        {
            // Step A: Count occurrences
            var counts = new int[4];
            foreach (string motif in motifs) counts[NucleotideToIndex[motif[col]]]++;

            // Step B: Apply Laplace Rule (Add 1 to numerator, Add 4 to denominator)
            for (var row = 0; row < 4; row++)
                // prob = (count + 1) / (total_rows + 4)
                profile[row, col] = (double)(counts[row] + 1) / (t + 4);
        }

        return profile;
    }

    private static double[,] CreateProfile(List<string> motifs, int k)
    {
        var profile = new double[4, k];
        int t = motifs.Count;

        for (var col = 0; col < k; col++)
        {
            foreach (string motif in motifs)
            {
                char nucleotide = motif[col];
                profile[NucleotideToIndex[nucleotide], col]++;
            }

            for (var row = 0; row < 4; row++) profile[row, col] /= t;
        }

        return profile;
    }

    // Finds the k-mer in a text that has the highest probability according to the profile

    // TODO: all of this needs to be reformatted/rethought out
    private static string GetProfileMostProbableKmer(DnaSequence text, int k, double[,] profile)
    {
        double maxProb = -1.0;
        string bestKmer = text.Substring(0, k); // Default to first k-mer

        for (var i = 0; i <= text.Length - k; i++)
        {
            string kmer = text.Substring(i, k);
            var currentProb = 1.0;

            for (var j = 0; j < k; j++)
            {
                char nucleotide = kmer[j];
                currentProb *= profile[NucleotideToIndex[nucleotide], j];
            }

            // If strictly greater, update. 
            // This implicitly handles the rule: "if ties, pick the first occurrence"
            if (currentProb > maxProb)
            {
                maxProb = currentProb;
                bestKmer = kmer;
            }
        }

        return bestKmer;
    }

    // Calculates the Score (Sum of Hamming distances from the Consensus string)
    // A lower score is better.
    private static int Score(List<string> motifs)
    {
        int k = motifs[0].Length;
        int t = motifs.Count;
        var score = 0;

        for (var col = 0; col < k; col++)
        {
            // Count frequencies in this column
            var counts = new int[4];
            foreach (string motif in motifs) counts[NucleotideToIndex[motif[col]]]++;

            // The score for this column is the number of "unpopular" letters
            // i.e., Total Rows - Max Frequency
            int maxFreq = counts.Max();
            score += t - maxFreq;
        }

        return score;
    }

    private static Lazy<Random> random = new();
}