// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var lineNum = 0;
var lineNumbers = new List<int>();
foreach (string lineToRead in File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(),
             "../../../../data/otherdata/crab.fasta")))
{
    // Printing the file contents 
    if (lineToRead.Contains('>'))
    {
        Console.WriteLine(lineToRead);
        lineNumbers.Add(lineNum);
    }

    lineNum++;
}


// Let's create a Fasta Object