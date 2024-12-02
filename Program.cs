// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var file = File.Open("/Users/rakirs/github/DNAStore/data/ncbi_dataset/ncbi_dataset/data/GCA_009914755.4/GCA_009914755.4_T2T-CHM13v2.0_genomic.fna", FileMode.Open);

byte[] byteArray = new byte[1024];

Console.WriteLine(file.Length);

var bytesRead = 0;
while (bytesRead < file.Length)
{
    bytesRead += file.Read(byteArray, 0, 1024);
    var bufferString = System.Text.Encoding.UTF8.GetString(byteArray);
    if (bufferString.Contains('>'))
    {
        Console.WriteLine(bufferString);
    }
}