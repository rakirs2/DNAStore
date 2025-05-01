namespace ConsoleRunner
{
    internal static class InputProcessor
    {
        public static IExecutor GetExecutor(string? request)
        {
            switch (request)
            {
                case "analyzeString":
                    {
                        // TODO: static constructors probably
                        return new SequenceAnalysis();
                    }
                    break;
                case "why":
                    {
                        return new EasterEgg();
                    }

                default:
                    {
                        // probably safe to do it this way
                        return new SequenceAnalysis();
                    }
            }
        }
    }
}
