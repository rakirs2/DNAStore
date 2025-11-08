using System.Diagnostics;
using System.Security.Cryptography;
using Base.Interfaces;

namespace Base.DataStructures;

public class AlignmentMatrix : IAlignmentMatrix
{
    private List<List<Node>> _matrix;
    public AlignmentMatrix(string a, string b)
    {
        Node[,] _matrix = new Node[a.Length +1,b.Length+1];
        for (int i=0;i<(a.Length+1)*(b.Length+1);i++) _matrix[i%(a.Length+1),i/(a.Length+1)]=new Node(); 
        

        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < b.Length; j++)
            {
                // what value do we add?
                Console.WriteLine($"{_matrix[i,j].Value}");
            }
        }
    }

    private class Node
    {
        public Node()
        {
            Pointer = Direction.none;
            Value = 0;
        }
        public Direction Pointer { get; set; }
        public int Value { get; set; }
    }
    
    private enum Direction
    {
         none = 0,
         match = 1,
         left = 2,
         up = 4
    }

    public string LongestCommonSubSequence()
    {
        
        return null;
    }
}