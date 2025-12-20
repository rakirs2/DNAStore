using System.Text;
using Base.Interfaces;
using Base.Utils;

namespace Base.DataStructures;

public class AlignmentMatrix : IAlignmentMatrix
{
    private readonly string _a;
    private readonly string _b;
    private readonly Node[,] _matrix;

    public AlignmentMatrix(string a, string b)
    {
        _a = a;
        _b = b;
        _matrix = new Node[a.Length + 1, b.Length + 1];

        // Instantiate all matrix points with a node of value none.
        for (var i = 0; i < (a.Length + 1) * (b.Length + 1); i++)
            _matrix[i % (a.Length + 1), i / (a.Length + 1)] = new Node();

        for (var i = 1; i <= a.Length; i++)
        for (var j = 1; j <= b.Length; j++)
            // we have a match
            if (a[i - 1] == b[j - 1])
            {
                _matrix[i, j].Value = _matrix[i - 1, j - 1].Value + 1;
                _matrix[i, j].Direction = Direction.Match;
            }
            else if (_matrix[i, j - 1].Value == _matrix[i - 1, j].Value)
            {
                _matrix[i, j].Value = _matrix[i, j - 1].Value;
                _matrix[i, j].Direction = Direction.Both;
            }
            else if (_matrix[i, j - 1].Value > _matrix[i - 1, j].Value)
            {
                _matrix[i, j].Direction = Direction.Left;
                _matrix[i, j].Value = _matrix[i, j - 1].Value;
            }
            else
            {
                _matrix[i, j].Direction = Direction.Up;
                _matrix[i, j].Value = _matrix[i - 1, j].Value;
            }
    }

    public string LongestCommonSubSequence()
    {
        // The table is built; now to traceback
        // TODO: this only returns one. How do we return all?
        var i = _a.Length;
        var j = _b.Length;
        var node = _matrix[i, j];
        var val = node.Value;
        var sb = new StringBuilder();
        while (val > 0)
        {
            // Could instead go 'left' on 'both'
            if (node.Direction.Equals(Direction.Up) || node.Direction.Equals(Direction.Both))
            {
                i--;
            }
            else if (node.Direction.Equals(Direction.Left))
            {
                j--;
            }
            else
            {
                i--;
                j--;
                sb.Insert(0, _a[i]);
            }

            node = _matrix[i, j];
            val = node.Value;
        }

        return sb.ToString();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var row = 0; row < _matrix.GetLength(0); row++)
        {
            sb.Append(string.Join<Node>(", ", MatrixUtils.GetRow(_matrix, row)));
            if (row != _matrix.GetLength(1) - 1)
                sb.AppendLine();
        }

        return sb.ToString();
    }

    public static string LongestCommonSubSequence(string a, string b)
    {
        return new AlignmentMatrix(a, b).LongestCommonSubSequence();
    }

    private class Node
    {
        public Direction Direction { get; set; } = Direction.None;
        public int Value { get; set; }

        public override string ToString()
        {
            return "(" + Direction + ", " + Value + ")";
        }
    }

    private enum Direction
    {
        None = 0,
        Match = 1,
        Left = 2,
        Up = 4,
        Both = 8
    }
}