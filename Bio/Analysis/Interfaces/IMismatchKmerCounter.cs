namespace Bio.Analysis.Interfaces;
internal interface IMismatchKmerCounter : IKmerCounter
{
    int Tolerance { get; }

    HashSet<string> GetKmers(string matchString, bool checkComplement);
}
