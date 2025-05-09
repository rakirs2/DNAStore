using System.Text;
using DNAStore.Executors;

namespace ConsoleRunner.Executors;

public class EasterEgg : BaseExecutor
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
        theWhy.Append(
            "Perhaps this is the real test to see how committed I am to a subject when I get to pick the subject matter. ");
        theWhy.Append("I really do miss Biology and Chemistry. They were my first loves for a reason. This is my way of looking back");

        Console.WriteLine(theWhy.ToString());
    }
}