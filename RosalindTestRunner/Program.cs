using RosalindTestRunner;

var currentInput = "notExist";

while (currentInput != "exit")
{
    Console.WriteLine("What would you like to do");
    // TODO: a good exercise -- fuzzy match these to best chance
    Console.WriteLine($"Current options are to: {string.Join(", ", InputProcessor.ExecutorRegistry.GetExecutorNames())}");
    currentInput = Console.ReadLine();
    var executor = InputProcessor.GetExecutor(currentInput);
    executor.Run();
}