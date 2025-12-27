namespace DNAStore.Sequences.Exceptions;

public static class MassSpecExceptions
{
    public class InvalidMassException(string? message) : ArgumentException(message);
}