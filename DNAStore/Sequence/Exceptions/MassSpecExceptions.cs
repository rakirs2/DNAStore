namespace Bio.Exceptions;

public static class MassSpecExceptions
{
    public class InvalidMassException(string? message) : ArgumentException(message);
}