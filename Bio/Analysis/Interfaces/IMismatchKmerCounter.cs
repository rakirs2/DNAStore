namespace Bio.Analysis.Interfaces;
internal interface IMismatchKmerCounter : IKmerCounter
{
    int Tolerance { get; }

    List<string> GetKmers(string matchString);
}
