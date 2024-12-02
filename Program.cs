// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
// var file = File.Open("/Users/rakirs/github/DNAStore/data/ncbi_dataset/ncbi_dataset/data/GCA_009914755.4/GCA_009914755.4_T2T-CHM13v2.0_genomic.fna", FileMode.Open);

byte[] byteArray = new byte[1024];


foreach(string line in File.ReadLines(@"/Users/rakirs/github/DNAStore/data/ncbi_dataset/ncbi_dataset/data/GCA_009914755.4/GCA_009914755.4_T2T-CHM13v2.0_genomic.fna")) 
{ 
    // Printing the file contents 
    if(line.Contains('>'))
        Console.WriteLine(line);
} 

