using System.Text;

namespace DNAStore.Executors;

internal class EasterEgg : BaseExecutor
{
    /// <summary>
    /// TODO: for a different day, let's make a new executor here to get rid of these ugly overrides
    /// </summary>
    protected override void CalculateResult()
    {
    }

    protected override void GetInputs()
    {
    }

    protected override void OutputResult()
    {
        StringBuilder theWhy = new();
        theWhy.Append("Welcome to DNA Store, a C# based implementation of everything bioinformatics related. ");
        theWhy.Append(
            "Today is 4/30/2025. At some point of analyzing file streams, I realized I want to focus on the life stream in my spare time. ");
        theWhy.Append("I really do miss Biology and Chemistry. They were my first loves for a reason.");

        Console.WriteLine(theWhy.ToString());
    }
}