using Bio.Sequence.Types;
using BioTests.Analysis.Interfaces;

namespace Bio.Analysis.Types;

// For now, it's fine if it's accessible, but if the only use case is for a fasta read, it should probably be subclassed and interfaced
public class ProfileMatrix : IProfileMatrix
{
    public ProfileMatrix() { }

    public long Length { get; private set; }
    public long QuantityAnalyzed { get; }
    public AnySequence GetProfileString()
    {
        throw new NotImplementedException();
    }

    public string GetCleanOutput()
    {
        throw new NotImplementedException();
    }
}