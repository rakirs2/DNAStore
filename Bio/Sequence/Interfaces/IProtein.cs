namespace Bio.Sequence.Interfaces;

public interface IProtein
{
    /// <summary>
    /// Returns the molecular weight of a given protein
    /// </summary>
    public double MolecularWeight { get; }
}