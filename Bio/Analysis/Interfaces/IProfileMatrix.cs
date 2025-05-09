using Bio.Sequence.Types;

namespace BioTests.Analysis.Interfaces
{
    public interface IProfileMatrix
    {
        /// <summary>
        /// Returns the length of all sequences (assuming perfect data)
        /// </summary>
        long Length { get; }
        long QuantityAnalyzed { get; }
        AnySequence GetProfileString();

        string GetCleanOutput();
    }
}
