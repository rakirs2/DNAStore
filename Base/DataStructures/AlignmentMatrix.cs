using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Base.Interfaces;
using Base.Utils;

namespace Base.DataStructures;

public class AlignmentMatrix : IAlignmentMatrix
{
    private readonly Node[,] _matrix;
    private readonly string _a;
    private readonly string _b;
    public AlignmentMatrix(string a, string b)
    {
        _a = a;
        _b = b;
         _matrix = new Node[a.Length +1,b.Length+1];
         for (int i = 0; i < (a.Length + 1) * (b.Length + 1); i++)
         {
             _matrix[i%(a.Length+1),i/(a.Length+1)]=new Node();
         }

         for (int i = 1; i <= a.Length; i++)
         {
             for (int j = 1; j <= b.Length; j++)
             {
                 // we have a match
                 if (a[i - 1] == b[j - 1])
                 {
                     _matrix[i, j].Value = _matrix[i - 1, j - 1].Value + 1;
                     _matrix[i, j].Pointer = Direction.Match;
                 } else if (_matrix[i, j - 1].Value == _matrix[i - 1, j].Value)
                 {
                     _matrix[i, j].Value = _matrix[i, j-1].Value;
                     _matrix[i, j].Pointer = Direction.Both;
                 }else if (_matrix[i,j-1].Value > _matrix[i-1,j].Value) {
                     _matrix[i,j].Pointer = Direction.Left;
                     _matrix[i,j].Value = _matrix[i,j-1].Value;
                 } else {
                     _matrix[i,j].Pointer = Direction.Up;
                     _matrix[i,j].Value = _matrix[i-1,j].Value;
                 }
             }
         }
    }

    // TODO: I think this should be held as an external object as things get more abstracted.
    private class Node
    {
        public Node()
        {
            Pointer = Direction.None;
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
    
    // TODO: consider using flags
    private enum Direction
    {
         None = 0,
         Match = 1,
         Left = 2,
         Up = 4,
         Both = 8
    }

    public string LongestCommonSubSequence()
    {
        // The table is built; now to traceback
        // TODO: this only returns one. How do we return all?
        int i = _a.Length;
        int j = _b.Length;
        Node node = _matrix[i,j];
        int val = node.Value;
        StringBuilder sb = new StringBuilder();
        while (val > 0) {
            // Could instead go 'left' on 'both'
            if (node.Pointer.Equals(Direction.Up) || node.Pointer.Equals(Direction.Both)) {
                i--;
            } else if (node.Pointer.Equals(Direction.Left)) {
                j--;
            } else {
                i--;
                j--;
                sb.Insert(0, _a[i]);
            }
            node = _matrix[i,j];
            val = node.Value;
        }
        return sb.ToString();
    }

    public static string LongestCommonSubSequence(string a, string b)
    {
        return new AlignmentMatrix(a, b).LongestCommonSubSequence();
    }
}