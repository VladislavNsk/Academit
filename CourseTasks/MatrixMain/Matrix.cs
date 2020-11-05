using System;
using System.Collections.Generic;
using System.Text;
using VectorMain;

namespace MatrixMain
{
    class Matrix
    {
        private Vector[] rows;

        public Matrix(int rowsCount, int columnsCount)
        {
            if (rowsCount <= 0)
            {
                throw new ArgumentException($"Введено не корректное кол-во строк rowsCount = {rowsCount}, строк должно быть больше 0", nameof(rowsCount));
            }

            if (columnsCount <= 0)
            {
                throw new ArgumentException($"Введено не корректное кол-во столбцов columnsCount = {columnsCount}, столбцов должно быть больше 0", nameof(columnsCount));
            }

            rows = new Vector[rowsCount];

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = new Vector(columnsCount);
            }
        }

        public Matrix(Matrix matrix)
        {
            rows = new Vector[matrix.GetRowsCount()];

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = new Vector(matrix.GetRow(i));
            }
        }

        public Matrix(double[,] array)
        {
            if (array.Length == 0)
            {
                throw new ArgumentException($"Пытаетесь присвоить пустой массив array.Length: \"{array.Length}\"", nameof(array));
            }

            rows = new Vector[array.GetLength(0)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                rows[i] = new Vector(array.GetLength(1));

                for (int j = 0; j < array.GetLength(1); j++)
                {
                    rows[i].SetComponent(j, array[i, j]);
                }
            }
        }

        public Matrix(Vector[] rows)
        {
            if (rows.Length == 0)
            {
                throw new ArgumentException($"Пытаетесь присвоить пустой массив array.Length: \"{rows.Length}\"", nameof(rows));
            }

            this.rows = new Vector[rows.Length];
            int maxSize = GetMaxRowSize(rows);

            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i].GetSize() < maxSize)
                {
                    this.rows[i] = new Vector(maxSize);
                    this.rows[i].Add(rows[i]);
                }
                else
                {
                    this.rows[i] = new Vector(rows[i]);
                }
            }
        }

        public int GetRowsCount()
        {
            return rows.Length;
        }

        public int GetColumnsCount()
        {
            return rows[0].GetSize();
        }

        public Vector GetColumn(int columnIndex)
        {
            if (columnIndex < 0 || rows[0].GetSize() <= columnIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex), $"Столбца {columnIndex} нет");
            }

            double[] array = new double[rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                array[i] = rows[i].GetComponent(columnIndex);
            }

            return new Vector(array);
        }

        public Vector GetRow(int rowIndex)
        {
            if (rowIndex < 0 || rows.Length <= rowIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Строки {rowIndex} нет, всего строк в матрице {rows.Length}");
            }

            return new Vector(rows[rowIndex]);
        }

        public void SetRow(int rowIndex, Vector vector)
        {
            if (rowIndex < 0 || rows.Length <= rowIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Строки {rowIndex} нет, всего строк в матрице {rows.Length}");
            }

            if (rows[0].GetSize() != vector.GetSize())
            {
                throw new ArgumentException("Размеры строк должны совпадать", nameof(vector));
            }

            rows[rowIndex] = new Vector(vector);
        }

        public void Transpose()
        {
            Vector[] rows = new Vector[this.rows[0].GetSize()];

            for (int i = 0; i < this.rows[0].GetSize(); i++)
            {
                rows[i] = GetColumn(i);
            }

            this.rows = rows;
        }

        public void Multiply(double number)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i].Multiply(number);
            }
        }

        public double GetDeterminant()
        {
            if (rows[0].GetSize() != rows.Length)
            {
                throw new InvalidOperationException("Матрица не квадратная, найти определитель не возможно");
            }

            return GetDeterminant(rows);
        }

        private double GetDeterminant(Vector[] matrix)
        {
            if (matrix.Length == 1)
            {
                return matrix[0].GetComponent(0);
            }

            if (matrix.Length == 2)
            {
                return matrix[0].GetComponent(0) * matrix[1].GetComponent(1) - matrix[0].GetComponent(1) * matrix[1].GetComponent(0);
            }

            double determinant = 0;

            for (int i = 0; i < matrix[0].GetSize(); i++)
            {
                determinant += matrix[0].GetComponent(i) * Math.Pow(-1, 1 + i + 1) * GetAlgebraicComplement(matrix, i);
            }

            return determinant;
        }

        private double GetAlgebraicComplement(Vector[] matrix, int columnIndex)
        {
            Vector[] algebraicComplement = new Vector[matrix.Length - 1];
            List<double> row = new List<double>();

            for (int i = 1; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    if (j != columnIndex)
                    {
                        row.Add(matrix[i].GetComponent(j));
                    }
                }

                SetAlgebraicComplement(algebraicComplement, row, i - 1);
                row.Clear();
            }

            return GetDeterminant(algebraicComplement);
        }

        private void SetAlgebraicComplement(Vector[] algebraicComplement, List<double> row, int rowIndex)
        {
            algebraicComplement[rowIndex] = new Vector(algebraicComplement.Length);

            for (int i = 0; i < algebraicComplement.Length; i++)
            {
                algebraicComplement[rowIndex].SetComponent(i, row[i]);
            }
        }

        public Vector MultiplyByVector(Vector vector)
        {
            if (vector.GetSize() != GetColumnsCount())
            {
                throw new ArgumentException("Количество строк в векторе-столбце не совпадает с количеством столбцов в матрице", nameof(vector));
            }

            Vector result = new Vector(GetRowsCount());

            for (int i = 0; i < rows.Length; i++)
            {
                double component = Vector.GetScalarProduct(rows[i], vector);
                result.SetComponent(i, component);
            }

            return result;
        }

        public void Add(Matrix matrix)
        {
            if (IsMatricesEqual(this, matrix))
            {
                throw new ArgumentException($"Матрицы разного размера: matrix1.rows = {GetRowsCount()} matrix1.columns = {GetColumnsCount()}, " +
                                            $"matrix2.rows = {matrix.GetRowsCount()} matrix2.columns = {matrix.GetColumnsCount()}", nameof(matrix));
            }

            for (int i = 0; i < GetRowsCount(); i++)
            {
                rows[i].Add(matrix.rows[i]);
            }
        }

        public void Subtract(Matrix matrix)
        {
            if (IsMatricesEqual(this, matrix))
            {
                throw new ArgumentException($"Матрицы разного размера: matrix1.rows = {GetRowsCount()} matrix1.columns = {GetColumnsCount()}, " +
                                            $"matrix2.rows = {matrix.GetRowsCount()} matrix2.columns = {matrix.GetColumnsCount()}", nameof(matrix));
            }

            for (int i = 0; i < GetRowsCount(); i++)
            {
                rows[i].Subtract(matrix.rows[i]);
            }
        }

        public static Matrix GetSum(Matrix matrix1, Matrix matrix2)
        {
            if (IsMatricesEqual(matrix1, matrix2))
            {
                throw new ArgumentException("Матрицы разного размера", $"{nameof(matrix1)} и {nameof(matrix2)}");
            }

            Matrix resultMatrix = new Matrix(matrix1);
            resultMatrix.Add(matrix2);

            return resultMatrix;
        }

        public static Matrix GetDifference(Matrix matrix1, Matrix matrix2)
        {
            if (IsMatricesEqual(matrix1, matrix2))
            {
                throw new ArgumentException("Матрицы разного размера", $"{nameof(matrix1)} и {nameof(matrix2)}");
            }

            Matrix resultMatrix = new Matrix(matrix1);
            resultMatrix.Subtract(matrix2);

            return resultMatrix;
        }

        private static bool IsMatricesEqual(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.GetColumnsCount() != matrix2.GetColumnsCount() || matrix1.GetRowsCount() != matrix2.GetRowsCount();
        }

        public static Matrix GetProduct(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.GetColumnsCount() != matrix2.GetRowsCount())
            {
                throw new ArgumentException($"Количество столбцов первой матрицы matrix1.columns = {matrix1.GetColumnsCount()}," + 
                                            $"не совпадает с количеством строк второй матрицы matrix2.rows = {matrix2.GetRowsCount()}",
                                            $"{nameof(matrix1)} и {nameof(matrix2)}");
            }

            Matrix resultMatrix = new Matrix(matrix1.GetRowsCount(), matrix2.GetColumnsCount());

            for (int i = 0; i < matrix1.GetRowsCount(); i++)
            {
                for (int j = 0; j < matrix2.GetColumnsCount(); j++)
                {
                    resultMatrix.rows[i].SetComponent(j, Vector.GetScalarProduct(matrix1.rows[i], matrix2.GetColumn(j)));
                }
            }

            return resultMatrix;
        }

        private static int GetMaxRowSize(Vector[] rows)
        {
            int maxSize = rows[0].GetSize();

            for (int i = 1; i < rows.Length; i++)
            {
                if (maxSize < rows[i].GetSize())
                {
                    maxSize = rows[i].GetSize();
                }
            }

            return maxSize;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("{");

            for (int i = 0; i < rows.Length - 1; i++)
            {
                stringBuilder.Append(rows[i]);
                stringBuilder.Append(", ");
            }

            stringBuilder.Append(rows[rows.Length - 1]);
            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }
    }
}
