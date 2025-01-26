// See https://aka.ms/new-console-template for more information

using System.Text;
using Bio.Sequence;

Console.WriteLine("Hello, World!");

byte[] byteArray = new byte[1024];

var lineNum = 0;
var lineNumbers = new List<int>();
foreach(string lineToRead in File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), "../../../../data/otherdata/crab.fasta"))) 
{ 
    // Printing the file contents 
    if (lineToRead.Contains('>'))
    {
        Console.WriteLine(lineToRead);
        lineNumbers.Add(lineNum);
    }

    lineNum++;
}




