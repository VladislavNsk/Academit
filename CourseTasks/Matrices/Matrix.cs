using System;
using System.Collections.Generic;
using System.Text;
using Vectors;


namespace Matrices
{
    class Matrix
    {
        public double[,] Array { get; set; }

        private double[] tempArray;
        private static double[,] tempMatrix;

        public Matrix(int n, int m)
        {
            if (n <= 0 || m <= 0)
            {
                throw new Exception("Не корректные входные данные");
            }

            Array = new double[n, m];
        }

        public Matrix(Matrix matrix)
        {
            Array = matrix.Array;
        }

        public Matrix(double[,] array)
        {
            Array = array;
        }

        public Matrix(Vector[] vectorsArray)
        {
            Array = new double[vectorsArray.Length, GetMaxIndex(vectorsArray)];

            for (int i = 0; i < vectorsArray.Length; i++)
            {
                for (int j = 0; j < vectorsArray[i].Value.Length; j++)
                {
                    Array[i, j] = vectorsArray[i].Value[j];
                }
            }
        }

        public double[] GetSize()
        {
            return new double[] { Array.GetLength(0), Array.GetLength(1) };
        }

        public void SetVectorRow(int rowIndex, Vector vector)
        {
            if (Array.GetLength(1) != vector.Value.Length && Array.GetLength(0) < rowIndex && rowIndex >= 0)
            {
                throw new Exception("Размерность массива не совпадает");
            }

            for (int y = 0; y < Array.GetLength(0); y++)
            {
                if (y != rowIndex)
                {
                    continue;
                }

                for (int x = 0; x < vector.Value.Length; x++)
                {
                    Array[y, x] = vector.Value[x];
                }

                break;
            }
        }

        public Vector GetVectorColumn(int columnIndex)
        {
            if (Array.GetLength(1) < columnIndex && columnIndex >= 0)
            {
                throw new Exception("Искомового индекса нет");
            }

            tempArray = new double[Array.GetLength(0)];

            for (int x = 0; x < Array.GetLength(1); x++)
            {
                if (x != columnIndex)
                {
                    continue;
                }

                for (int y = 0; y < Array.GetLength(0); y++)
                {
                    tempArray[y] = Array[y, x];
                }

                break;
            }

            return new Vector(tempArray);
        }

        public Vector GetVectorRow(int rowIndex)
        {
            if (Array.GetLength(0) < rowIndex && rowIndex >= 0)
            {
                throw new Exception("Искомового индекса нет");
            }

            tempArray = new double[Array.GetLength(1)];

            for (int y = 0; y < Array.GetLength(0); y++)
            {
                if (y != rowIndex)
                {
                    continue;
                }

                for (int x = 0; x < Array.GetLength(1); x++)
                {
                    tempArray[x] = Array[y, x];
                }

                break;
            }

            return new Vector(tempArray);
        }

        public void TransposeMatrix()
        {
            double[,] array = new double[Array.GetLength(1), Array.GetLength(0)];

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    array[j, i] = Array[i, j];
                }
            }

            Array = array;
        }

        public void Multiply(int number)
        {
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Array[i, j] *= number;
                }
            }
        }

        public double GetDeterminant()
        {
            if (Array.GetLength(0) != Array.GetLength(1))
            {
                throw new Exception("Матрица не квадратная, найти определитель не возможно");
            }

            return GetDeterminant(Array);
        }

        private double GetDeterminant(double[,] matrix)
        {
            if (matrix.Length == 1)
            {
                return matrix[0, 0];
            }

            if (matrix.Length == 4)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            double determinant = 0;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                determinant += matrix[0, i] * Math.Pow(-1, 1 + i + 1) * GetAlgebraicComplement(matrix, i);
            }

            return determinant;
        }

        private double GetAlgebraicComplement(double[,] matrix, int columnIndex)
        {
            double[,] algebraicComplement = new double[matrix.GetLength(1) - 1, matrix.GetLength(1) - 1];
            List<double> row = new List<double>();

            for (int i = 1; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == columnIndex)
                    {
                        continue;
                    }

                    row.Add(matrix[i, j]);
                }

                SetAlgebraicComplement(algebraicComplement, row, i - 1);
                row.Clear();
            }

            return GetDeterminant(algebraicComplement);
        }

        private void SetAlgebraicComplement(double[,] algebraicComplement, List<double> row, int rowIndex)
        {
            for (int i = 0; i < algebraicComplement.GetLength(1); i++)
            {
                algebraicComplement[rowIndex, i] = row[i];
            }
        }

        public void MultiplyByVector(Vector vector)
        {
            if (vector.Value.Length != Array.GetLength(0))
            {
                throw new Exception("Размер вектора не соответствует");
            }

            double[] array = new double[Array.GetLength(0)];

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    array[i] += Array[i, j] * vector.Value[j];
                }
            }

            Array = new double[Array.GetLength(0), 1];

            for (int i = 0; i < array.Length; i++)
            {
                Array[i, 0] = array[i];
            }
        }

        public void AddMatrix(Matrix matrix)
        {
            if (Array.GetLength(0) != matrix.Array.GetLength(0) || Array.GetLength(1) != matrix.Array.GetLength(1))
            {
                throw new Exception("Матрицы равзного размера");
            }

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Array[i, j] += matrix.Array[i, j];
                }
            }
        }

        public void SubtractMatrix(Matrix matrix)
        {
            if (Array.GetLength(0) != matrix.Array.GetLength(0) || Array.GetLength(1) != matrix.Array.GetLength(1))
            {
                throw new Exception("Матрицы равзного размера");
            }

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Array[i, j] -= matrix.Array[i, j];
                }
            }
        }

        public static Matrix AddMatrices(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Array.GetLength(0) != matrix2.Array.GetLength(0) || matrix1.Array.GetLength(1) != matrix2.Array.GetLength(1))
            {
                throw new Exception("Матрицы равзного размера");
            }

            tempMatrix = new double[matrix1.Array.GetLength(0), matrix1.Array.GetLength(1)];

            for (int i = 0; i < tempMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tempMatrix.GetLength(1); j++)
                {
                    tempMatrix[i, j] = matrix1.Array[i, j] + matrix2.Array[i, j];
                }
            }

            return new Matrix(tempMatrix);
        }

        public static Matrix SubtractMatrices(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Array.GetLength(0) != matrix2.Array.GetLength(0) || matrix1.Array.GetLength(1) != matrix2.Array.GetLength(1))
            {
                throw new Exception("Матрицы равзного размера");
            }

            tempMatrix = new double[matrix1.Array.GetLength(0), matrix1.Array.GetLength(1)];

            for (int i = 0; i < tempMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tempMatrix.GetLength(1); j++)
                {
                    tempMatrix[i, j] = matrix1.Array[i, j] - matrix2.Array[i, j];
                }
            }

            return new Matrix(tempMatrix);
        }

        public static Matrix MultiplyMatrices(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Array.GetLength(0) != matrix2.Array.GetLength(0) || matrix1.Array.GetLength(1) != matrix2.Array.GetLength(1))
            {
                throw new Exception("Матрицы равзного размера");
            }

            tempMatrix = new double[matrix1.Array.GetLength(0), matrix1.Array.GetLength(1)];

            for (int i = 0; i < tempMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tempMatrix.GetLength(1); j++)
                {
                    tempMatrix[i, j] = matrix1.Array[i, j] * matrix2.Array[i, j];
                }
            }

            return new Matrix(tempMatrix);
        }

        private int GetMaxIndex(Vector[] vectors)
        {
            int maxIndex = 0;

            for (int i = 0; i < vectors.Length; i++)
            {
                if (maxIndex < vectors[i].Value.Length)
                {
                    maxIndex = vectors[i].Value.Length;
                }
            }

            return maxIndex;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("{ ");

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                stringBuilder.Append("{ ");

                for (int j = 0; j < Array.GetLength(1) - 1; j++)
                {
                    stringBuilder.Append(Array[i, j].ToString() + ", ");
                }

                stringBuilder.Append(Array[i, Array.GetLength(1) - 1].ToString() + " ");

                if (i == Array.GetLength(0) - 1)
                {
                    stringBuilder.Append("} ");
                }
                else
                {
                    stringBuilder.Append("}, ");
                }
            }

            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }
    }
}
