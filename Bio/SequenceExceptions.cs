namespace Bio;

[Serializable]
public class SequenceExceptions : Exception
{
    public SequenceExceptions() : base()
    {
    }

    public SequenceExceptions(string message) : base(message)
    {
    }

    public SequenceExceptions(string message, Exception inner) : base(message, inner)
    {
    }
}