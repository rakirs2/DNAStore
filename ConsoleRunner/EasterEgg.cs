using System.Text;

namespace ConsoleRunner
{
    internal class EasterEgg : IExecutor
    {
        public void Run()
        {
            StringBuilder theWhy = new();
            theWhy.Append("Welcome to DNA Store, a C# based implementation of everything bioinformatics related. ");
            theWhy.Append(
                "Today is 4/30/2025. At some point of analyzing file streams, I realized I want to focus on the life stream in my spare time. ");
            theWhy.Append("Perhaps this is the real test to see how committed I am to a subject when I get to pick the subject matter. ");
            theWhy.Append("I really do miss Biology and Chemistry. They were my first loves for a reason. ");
            theWhy.Append("If I implement everything on project Rosalind here, I think it's safe to say I'm ready and capable.");

            Console.WriteLine(theWhy.ToString());
        }
    }
}
