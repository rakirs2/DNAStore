using RosalindTestRunner;

var currentInput = "notExist";

while (currentInput != "exit")
{
    Console.WriteLine("What would you like to do");
    Console.WriteLine($"Current options are to: {string.Join(", ", InputProcessor.ExecutorRegistry.Map.Keys.ToArray())}");
    currentInput = Console.ReadLine();
    var executor = InputProcessor.GetExecutor(currentInput);
    executor.Run();
}