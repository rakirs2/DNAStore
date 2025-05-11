using DNAStore;

var currentInput = "notExist";

while (currentInput != "exit")
{
    //var getUrl = "https://rest.uniprot.org/uniprotkb/A2Z669.fasta";
    //var getResponse = await UniprotClient.GetAsync<Post>(getUrl);
    //Console.WriteLine($"Title: {getResponse.Title}");

    Console.WriteLine("What would you like to do");
    Console.WriteLine("Current options are to: 'analyzeString'");
    currentInput = Console.ReadLine();
    var executor = InputProcessor.GetExecutor(currentInput);
    executor.Run();
}