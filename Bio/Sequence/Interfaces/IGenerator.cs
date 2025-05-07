namespace Bio.Sequence.Interfaces;

public interface IGenerator
{
    /// <summary>
    /// Create a sequence of a given type
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    ISequence Create(long length);
}