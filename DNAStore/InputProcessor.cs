using DNAStore.Executors;

namespace DNAStore;

internal static class InputProcessor
{
    public static IExecutor GetExecutor(string? request)
    {
        // TODO: static constructors probably
        // TODO: reconsider style on this
        // TODO: case sensitivity
        // TODO: probably should have flexibility within each execution
        // TODO: surely there's a clean way to autopopulate all of this
        // TODO: pretty sure there's a clean design pattern in the book for console apps. Might be worth exploring

        return BaseExecutor.GetFromString(request);
    }
}