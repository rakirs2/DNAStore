namespace DNAStore.Sequences.Types.Interfaces;

public interface ICodon
{
    string RawRna { get; }

    // TODO: eventually maybe this should return a static Amino Acid class?
    string ExpectedProtein { get; }
}