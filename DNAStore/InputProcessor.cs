using Bio.Analysis.Types;
using DNAStore.Executors;

namespace DNAStore;

internal static class InputProcessor
{
    public static IExecutor GetExecutor(string? request)
    {
        IExecutor output;
        // TODO: static constructors probably
        // TODO: reconsider style on this
        // TODO: case sensitivity
        // TODO: probably should have flexibility within each execution
        // TODO: surely there's a clean way to autopopulate all of this
        // TODO: pretty sure there's a clean design pattern in the book for console apps. Might be worth exploring
        switch (request)
        {
            case "analyzeString":
                output = new SequenceAnalysis();
                break;
            case "DNAtoRNA":
                output = new TranscibeDna();
                break;
            case "DNAComplement":
                output = new Complement();
                break;
            case "GCContent":
                output = new GCContent();
                break;
            case "Hamming":
                output = new Hamming();
                break;
            case "translateRNA":
                output = new TranslateRNA();
                break;
            case "PercentDominant":
                output = new PercentDominant();
                break;
            case "Permutations":
                output = new Permutations();
                break;
            case "motifFinder":
                output = new MotifFinder();
                break;
            case "ProfileMatrix":
                output = new ProfileMatrixExecutor();
                break;
            case "ProteinWeight":
                output = new ProteinWeight();
                break;
            case "OverlapGraph":
                output = new OverlapGraphExecutor();
                break;
            case "LongestCommonSubsequence":
                output = new LongestCommonSubsequenceExecutor();
                break;
            case "ProteinMotif":
                output = new ProteinMotifFinder();
                break;
            case "why":
                output = new EasterEgg();
                break;
            default:
                // probably safe to do it this way
                output = new SequenceAnalysis();
                break;

            // TODO: clean up the exit pathway
        }

        return output;
    }
}