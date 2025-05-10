using Bio.Sequence.Types;

namespace Bio.Analysis.Interfaces
{
    public interface IProfileMatrix
    {
        /// <summary>
        /// Returns the length of all sequences (assuming perfect data)
        /// </summary>
        long LengthOfSequences { get; }

        /// <summary>
        /// Returns the quantity of sequences analyzed
        /// </summary>
        long QuantityAnalyzed { get; }

        /// <summary>
        /// Returns a profile string with the max frequency at each location.
        /// Ties are broken with the last highest number
        /// </summary>
        /// <returns></returns>
        AnySequence GetProfileSequence();


        /// <summary>
        /// Returns a string object which can be read in the console
        /// </summary>
        /// <returns></returns>
        string GetCleanOutput();
    }
}
