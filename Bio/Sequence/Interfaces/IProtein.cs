namespace Bio.Sequence.Interfaces;

public interface IProtein : IStrictSequence
{
    /// <summary>
    ///     Returns the molecular weight of a given protein
    /// </summary>
    public double MolecularWeight { get; }

    /// <summary>
    ///     Returns the number of potential proteins including the stop codon modulo input
    /// </summary>
    /// <param name="modulo"></param>
    /// <returns></returns>
    public int NumberOfPossibleRNA(int modulo);
}