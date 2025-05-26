using DNAStore.Executors;

namespace DNAStore;

internal static class InputProcessor
{
    public static IExecutor GetExecutor(string request) => BaseExecutor.GetExecutorFromString(request);
}