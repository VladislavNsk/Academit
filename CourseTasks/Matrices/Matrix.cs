using System;
using System.Collections.Generic;
using System.Text;
using VectorMain;

namespace Matrices
{
    class Matrix
    {
        private Vector[] matrix;

        //private double[] tempArray;
        //private static double[,] tempMatrix;

        public Matrix(int rowsCount, int columnsCount)
        {
            if (rowsCount <= 0 || columnsCount <= 0)
            {
                throw new Exception("Не корректные входные данные");
            }

            matrix = new Vector[rowsCount];

            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new Vector(columnsCount);
            }
        }

        public Matrix(Matrix matrix)
        {
            this.matrix = new Vector[matrix.GetRowsCount()];
            matrix.matrix.CopyTo(this.matrix, 0);
        }

        public Matrix(double[,] array)
        {
            if (array.Length == 0)
            {
                // олшибка
            }

            this.matrix = new Vector[array.GetLength(0)];

            for (int i = 0; i < array.Length; i++)
            {
                this.matrix[i] = new Vector(array.GetLength(1));
            }
        }

        public Matrix(Vector[] vectorsArray)
        {
            if (vectorsArray.Length == 0)
            {
                // error
            }

            matrix = new Vector[vectorsArray.Length];
            vectorsArray.CopyTo(matrix, 0);
        }

        public int GetRowsCount()
        {
            return matrix.Length;
        }

        public int GetColumnsCount()
        {
            return matrix[0].GetSize();
        }

        public Vector GetColumn(int columnIndex)
        {
            //if (array.GetLength(1) < columnIndex && columnIndex >= 0)
            //{
            //    throw new Exception("Искомового индекса нет");
            //}

            tempArray = new double[matrix.GetLength(0)];

            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                if (x != columnIndex)
                {
                    continue;
                }

                for (int y = 0; y < matrix.GetLength(0); y++)
                {
                    tempArray[y] = matrix[y, x];
                }

                break;
            }

            return new Vector(tempArray);
        }

        public void SetRow(int rowIndex, Vector vector)
        {
            //if (array.GetLength(1) != vector.Value.Length && array.GetLength(0) < rowIndex && rowIndex >= 0)
            //{
            //    throw new Exception("Размерность массива не совпадает");
            //}

            Vector tempVector = new Vector(vector);
            matrix[rowIndex] = tempVector;
            //переименовать
        }

        public Vector GetRow(int rowIndex)
        {
            //if (array.GetLength(0) < rowIndex && rowIndex >= 0)
            //{
            //    throw new Exception("Искомового индекса нет");
            //}


            return matrix[rowIndex];
        }

        public void Transpose()
        {
            double[,] array = new double[this.matrix.GetLength(1), this.matrix.GetLength(0)];

            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrix.GetLength(1); j++)
                {
                    array[j, i] = this.matrix[i, j];
                }
            }

            this.matrix = array;
        }

        public void Multiply(double number)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i].Multiply(number);
            }
        }

        public double GetDeterminant()
        {
            if (matrix[0].GetSize() != matrix.Length)
            {
                throw new Exception("Матрица не квадратная, найти определитель не возможно");
            }

            return GetDeterminant(matrix);
        }

        private double GetDeterminant(Vector[] matrix)
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
            if (vector.Value.Length != this.matrix.GetLength(0))
            {
                throw new Exception("Размер вектора не соответствует");// error

            }

            double[] array = new double[matrix[0].GetSize()];

            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrix.GetLength(1); j++)
                {
                    array[i] += this.matrix[i, j] * vector.Value[j];
                }
            }

            this.matrix = new double[this.matrix.GetLength(0), 1];

            for (int i = 0; i < array.Length; i++)
            {
                this.matrix[i, 0] = array[i];
            }
        }

        public void AddMatrix(Matrix matrix)
        {
            if (this.matrix.GetLength(0) != matrix.matrix.GetLength(0) || this.matrix.GetLength(1) != matrix.matrix.GetLength(1))
            {
                throw new Exception("Матрицы равзного размера");
            }

            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrix.GetLength(1); j++)
                {
                    this.matrix[i, j] += matrix.matrix[i, j];
                }
            }
        }

        public void SubtractMatrix(Matrix matrix)
        {
            if (this.matrix.GetLength(0) != matrix.matrix.GetLength(0) || this.matrix.GetLength(1) != matrix.matrix.GetLength(1))
            {
                throw new Exception("Матрицы равзного размера");
            }

            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrix.GetLength(1); j++)
                {
                    this.matrix[i, j] -= matrix.matrix[i, j];
                }
            }
        }

        public static Matrix AddMatrices(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.matrix.GetLength(0) != matrix2.matrix.GetLength(0) || matrix1.matrix.GetLength(1) != matrix2.matrix.GetLength(1))
            {
                throw new Exception("Матрицы равзного размера");
            }

            tempMatrix = new double[matrix1.matrix.GetLength(0), matrix1.matrix.GetLength(1)];

            for (int i = 0; i < tempMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tempMatrix.GetLength(1); j++)
                {
                    tempMatrix[i, j] = matrix1.matrix[i, j] + matrix2.matrix[i, j];
                }
            }

            return new Matrix(tempMatrix);
        }

        public static Matrix SubtractMatrices(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.matrix.GetLength(0) != matrix2.matrix.GetLength(0) || matrix1.matrix.GetLength(1) != matrix2.matrix.GetLength(1))
            {
                throw new Exception("Матрицы равзного размера");
            }

            tempMatrix = new double[matrix1.matrix.GetLength(0), matrix1.matrix.GetLength(1)];

            for (int i = 0; i < tempMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tempMatrix.GetLength(1); j++)
                {
                    tempMatrix[i, j] = matrix1.matrix[i, j] - matrix2.matrix[i, j];
                }
            }

            return new Matrix(tempMatrix);
        }

        public static Matrix MultiplyMatrices(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.matrix.GetLength(0) != matrix2.matrix.GetLength(0) || matrix1.matrix.GetLength(1) != matrix2.matrix.GetLength(1))
            {
                throw new Exception("Матрицы равзного размера");
            }

            tempMatrix = new double[matrix1.matrix.GetLength(0), matrix1.matrix.GetLength(1)];

            for (int i = 0; i < tempMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tempMatrix.GetLength(1); j++)
                {
                    tempMatrix[i, j] = matrix1.matrix[i, j] * matrix2.matrix[i, j];
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

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                stringBuilder.Append("{ ");

                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    stringBuilder.Append(matrix[i, j].ToString() + ", ");
                }

                stringBuilder.Append(matrix[i, matrix.GetLength(1) - 1].ToString() + " ");

                if (i == matrix.GetLength(0) - 1)
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
