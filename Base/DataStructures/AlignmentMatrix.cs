using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Base.Interfaces;
using Base.Utils;

namespace Base.DataStructures;

public class AlignmentMatrix : IAlignmentMatrix
{
    private Node[,] _matrix;
    public AlignmentMatrix(string a, string b)
    {
         _matrix = new Node[a.Length +1,b.Length+1];
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

    // TODO: I think this should be held as an external object as things get more abstracted.
    private class Node
    {
        public Node()
        {
            Pointer = Direction.none;
            Value = 0;
        }
        public Direction Pointer { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return "(" + Pointer + ", "  + Value + ")";
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int row = 0; row < _matrix.GetLength(0); row++)
        {
            sb.Append(string.Join<Node>(", ", MatrixUtils.GetRow(_matrix, row)));
            if (row!= _matrix.GetLength(1) - 1)
                sb.AppendLine();
        }

        return sb.ToString();
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