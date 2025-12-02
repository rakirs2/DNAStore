using BioMath;

namespace Bio.Sequence.Types;

public static class DnaSequenceListExtensions
{
    private static readonly Dictionary<char, int> NucleotideToIndex = new Dictionary<char, int>
    {
        { 'A', 0 }, { 'C', 1 }, { 'G', 2 }, { 'T', 3 }
    };

    private static readonly char[] IndexToNucleotide = { 'A', 'C', 'G', 'T' };

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
            var maxOverlap = -1;
            int bestI = -1, bestJ = -1;

            for (var i = 0; i < copyOfList.Count; i++)
            for (var j = 0; j < copyOfList.Count; j++)
            {
                if (i == j) continue;

                var currentOverlap = AnySequence.CalculateOverlap(copyOfList[i], copyOfList[j]);
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
            var mergedString = copyOfList[bestI] + copyOfList[bestJ][maxOverlap..];
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
    /// <param name="distance"></param>
    /// <returns></returns>
    public static HashSet<string> MotifEnumeration(this List<DnaSequence> DnaSequences, int k, int distance)
    {
        var patterns = new HashSet<string>();
        var allPatterns = new HashSet<string>();
        foreach (var item in DnaSequences)
        {
            var currentKmers = item.KmerCompositionUniqueString(k);
            foreach (var kmer in currentKmers)
            foreach (var possible in new DnaSequence(kmer).DNeighborhood(distance))
                allPatterns.Add(possible);
        }

        foreach (var item in allPatterns)
        {
            var inAll = true;
            foreach (var seq in DnaSequences)
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
        foreach (var kmer in kmers)
        {
            var currentMin = int.MinValue;
            foreach (var sequence in sequences)
                currentMin = Math.Max(currentMin, sequence.GetMinimumDistanceForKmer(kmer));

            if (results.ContainsKey(currentMin))
                results[currentMin].Add(kmer);

            else
                results[currentMin] = new List<string> { kmer };
        }

        return results[results.Keys.Min()];
    }

    public static List<string> GreedyMotifSearch(this List<DnaSequence> sequences, int k, int t, bool usePseudocounts = false)
    {
        var bestMotifs = new List<string>();
        foreach (var seq in sequences)
        {
            bestMotifs.Add(seq.Substring(0, k));
        }

        var firstDna = sequences[0];
        for (int i = 0; i <= firstDna.Length - k; i++)
        {
            var motif1 = firstDna.Substring(i, k);
            var currentMotifs = new List<string> { motif1 };

            for (int j = 1; j < t; j++)
            {
                double[,] profile = usePseudocounts
                    ? CreateProfileWithPseudocounts(currentMotifs, k):
                CreateProfile(currentMotifs, k);

                string bestMatch = GetProfileMostProbableKmer(sequences[j], k, profile);
                currentMotifs.Add(bestMatch);
            }

            if (Score(currentMotifs) < Score(bestMotifs))
            {
                bestMotifs = currentMotifs;
            }
        }

        return bestMotifs;
    }

    // Creates Profile Matrix with Laplace Pseudocounts (+1)
    private static double[,] CreateProfileWithPseudocounts(List<string> motifs, int k)
    {
        double[,] profile = new double[4, k];
        int t = motifs.Count;

        for (int col = 0; col < k; col++)
        {
            // Step A: Count occurrences
            int[] counts = new int[4]; 
            foreach (string motif in motifs)
            {
                counts[NucleotideToIndex[motif[col]]]++;
            }

            // Step B: Apply Laplace Rule (Add 1 to numerator, Add 4 to denominator)
            for (int row = 0; row < 4; row++)
            {
                // prob = (count + 1) / (total_rows + 4)
                profile[row, col] = (double)(counts[row] + 1) / (t + 4);
            }
        }

        return profile;
    }
    
    private static double[,] CreateProfile(List<string> motifs, int k)
    {
        double[,] profile = new double[4, k];
        int t = motifs.Count;

        for (int col = 0; col < k; col++)
        {
            foreach (string motif in motifs)
            {
                char nucleotide = motif[col];
                profile[NucleotideToIndex[nucleotide], col]++;
            }

            for (int row = 0; row < 4; row++)
            {
                profile[row, col] /= t;
            }
        }

        return profile;
    }

    // Finds the k-mer in a text that has the highest probability according to the profile
    
    // TODO: all of this needs to be reformatted/rethought out
    private static string GetProfileMostProbableKmer(DnaSequence text, int k, double[,] profile)
    {
        double maxProb = -1.0;
        string bestKmer = text.Substring(0, k); // Default to first k-mer

        for (int i = 0; i <= text.Length - k; i++)
        {
            string kmer = text.Substring(i, k);
            double currentProb = 1.0;

            for (int j = 0; j < k; j++)
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
        int score = 0;

        for (int col = 0; col < k; col++)
        {
            // Count frequencies in this column
            int[] counts = new int[4];
            foreach (string motif in motifs)
            {
                counts[NucleotideToIndex[motif[col]]]++;
            }

            // The score for this column is the number of "unpopular" letters
            // i.e., Total Rows - Max Frequency
            int maxFreq = counts.Max();
            score += (t - maxFreq);
        }

        return score;
    }
}