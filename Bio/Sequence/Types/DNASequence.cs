using System.Text;

namespace Bio.Sequence.Types
{
    public class DNASequence(string rawSequence) : AnySequence(rawSequence)
    {
        // Should this be static, should this be a class conversion
        // For now, let's just let it be an explicit conversion, pay for the new class
        public RNASequence TranscribeToRNA()
        {
            return new RNASequence(RawSequence.Replace('T', 'U'));
        }
    }
}
