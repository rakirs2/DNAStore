using Bio.Sequence.Interfaces;

namespace Bio.Sequence.Types
{
    public class ProteinSequence : AnySequence, IProtein
    {
        /// <summary>
        /// This is really bad form right now. Lets clear this out eventually.
        /// </summary>
        /// <param name="rawSequence"></param>
        public ProteinSequence(string rawSequence) : base(rawSequence)
        {
        }

        /// <summary>
        /// Right now, this is a perfect constructor.
        /// RNA conversions to protein are the classic definition of an alignment problem
        /// However, for right now, storing this here might make sense.
        /// My long term theory is that I'll create a static converter class.
        ///
        /// TODO: for longer strings, this is another great place for buffered and parallel writes
        /// TODO: the underlying class should be able to support whatever comes in.
        /// </summary>
        /// <param name="rna"></param>
        public ProteinSequence(RNASequence rna) : base(rna.RawSequence)
        {
            RawRNA = rna.RawSequence;
        }

        public string RawRNA { get; }

        public string GetExpectedProteinString()
        {
            return SequenceHelpers.ConvertStringToProtein(RawRNA);
        }
    }
}
