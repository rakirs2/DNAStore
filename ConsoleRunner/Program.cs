// See https://aka.ms/new-console-template for more information

using ConsoleRunner;

Console.WriteLine("Welcome to DNA Store, a C# based implementation of everything I can do with bioinformatics");
Console.WriteLine("Perhaps this is the real test to see how committed " +
                  "I am to focusing on Biology and Research anb applying everything I've learned");
Console.WriteLine("If I implement everything on project Rosalind here, I think it's safe to say I'm ready and capable");

string? currentInput = "notExist";

while (currentInput != "exit")
{
    Console.WriteLine("What would you like to do");
    Console.WriteLine("Current options are to: 'analyzeString'");
    currentInput = Console.ReadLine();
    IExecutor executor = InputProcessor.GetExecutor(currentInput);
    executor.Run();
}