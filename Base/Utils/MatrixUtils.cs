namespace Base.Utils;

public class MatrixUtils
{
    public static T[] GetRow<T>(T[,] matrix, int rowIndex)
    {
        int numCols = matrix.GetLength(1);
        T[] row = new T[numCols];
        for (int col = 0; col < numCols; col++)
        {
            row[col] = matrix[rowIndex, col];
        }
        return row;
    }
}