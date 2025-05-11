
using Bio.Sequence.Types;

namespace Bio.Analysis.Types
{
    public class KmerCounter : IKmerCounter
    {
        /// <summary>
        /// This only gets the highest value. This is the bareminimum lightweight implementation
        /// as sequences can theoretically be infinitely large.
        ///
        /// TODO: add a heap implementation which can return all of the Kmers and their frequencies
        /// </summary>
        public string HighestFrequencyKmer { get; } = "";

        public int CurrentHighestFrequency { get; } = 0;
        public int KmerLength { get; }

        public KmerCounter(AnySequence sequence, int kmerLength)
        {
            KmerLength = kmerLength;
            // ok, we need to populate the right values here
            for (int i = 0; i < sequence.RawSequence.Length-1; i++)
            {
                var currentWord = sequence.RawSequence.Substring(i, KmerLength);
                if (Counts.ContainsKey(sequence.RawSequence.Substring(i, KmerLength)))
                {
                    Counts[currentWord] += 1;
                    if (Counts[currentWord] > CurrentHighestFrequency)
                    {
                        CurrentHighestFrequency = Counts[currentWord];
                        HighestFrequencyKmer = currentWord;
                    }
                }
                else
                {
                    Counts[currentWord] = 1;
                }
            }
        }

        private static readonly Dictionary<string, int> Counts = new();
    }
}
