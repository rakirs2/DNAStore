namespace ConsoleRunner
{
    internal static class InputProcessor
    {
        public static IExecutor GetExecutor(string? request)
        {
            IExecutor output;
            // TODO: static constructors probably
            // TODO: reconsider style on this
            switch (request)
            {
                case "analyzeString":
                    output = new SequenceAnalysis();
                    break;

                case "DNAtoRNA":
                    output = new TranscibeDna();
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
}
