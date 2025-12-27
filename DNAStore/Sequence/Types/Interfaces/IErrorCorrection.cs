namespace DnaStore.Sequence.Types.Interfaces;

public interface IErrorCorrection<T>
{
    public T Previous { get; }
    public T Suggested { get; }
}