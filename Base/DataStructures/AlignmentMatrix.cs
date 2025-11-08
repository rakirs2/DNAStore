using System.Diagnostics;
using System.Security.Cryptography;
using Base.Interfaces;

namespace Base.DataStructures;

public class AlignmentMatrix : IAlignmentMatrix
{
    private List<List<Node>> _matrix;
    public AlignmentMatrix(string a, string b)
    {
        var _matrix = new List<List<Node>>();
        for (int i = 0; i < a.Length +1; i++)
        {
            _matrix.Add(new List<Node>(b.Length +1));
        }

        // force edges to be None and 0.
        foreach (var item in _matrix)
        {
            item.Add(new Node());
        }
        
        for(int i = 0; i < b.Length + 1; i++)
        {
            if (i == 0)
            {
                continue;
            }
            _matrix[0].Add( new Node());
        }

        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < b.Length; j++)
            {
                // what value do we add?
                _matrix[i].Add(new Node( ));
                Console.WriteLine($"{_matrix[i]}, {_matrix[j]}");
            }
        }

        Debug.Assert(_matrix.Count == a.Length + 1);
        foreach (var item in _matrix)
        {
            Debug.Assert(item.Count == b.Length + 1);
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