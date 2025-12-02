namespace Bio.Sequence.Interfaces;

public interface IStrictSequence : ISequence
{
    static abstract HashSet<char> GetValidInputs { get; }
}