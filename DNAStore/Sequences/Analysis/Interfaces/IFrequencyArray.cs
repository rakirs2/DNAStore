namespace DNAStore.Sequences.Analysis.Interfaces;

internal interface IFrequencyArray
{
    List<int> GetFrequencyArrayInLexicographicOrder(string kmerValues, int kmerLength);
}