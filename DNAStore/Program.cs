using DNAStore;

var currentInput = "notExist";

while (currentInput != "exit")
{
    Console.WriteLine("What would you like to do");
    Console.WriteLine("Current options are to: 'analyzeString'");
    currentInput = Console.ReadLine();
    var executor = InputProcessor.GetExecutor(currentInput);
    executor.Run();
}