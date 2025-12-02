using System.Text.RegularExpressions;

namespace Bio.Analysis.Interfaces;

public interface IMotif : IMatch
{
    string Name { get; }

    /// <summary>
    ///     The original string added at construction
    /// </summary>
    string InputMotif { get; }
}