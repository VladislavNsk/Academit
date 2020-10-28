using System;
using System.Collections.Generic;
using System.Text;
using VectorMain;

namespace Matrices
{
    class Matrix
    {
        private Vector[] matrix;

        public Matrix(int rowsCount, int columnsCount)
        {
            if (rowsCount <= 0)
            {
                throw new ArgumentException("Введено не корректное кол-во строк \"rowsCount\", строк должно быть больше 0", nameof(rowsCount));
            }

            if (columnsCount <= 0)
            {
                throw new ArgumentException("Введено не корректное кол-во столбцов \"columnsCount\", столбцов должно быть больше 0", nameof(columnsCount));
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
                throw new ArgumentException($"Пытаетесь присвоить пустой массив array.Length: \"{array.Length}\"", nameof(array));
            }

            matrix = new Vector[array.GetLength(0)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                matrix[i] = new Vector(array.GetLength(1));

                for (int j = 0; j < array.GetLength(1); j++)
                {
                    matrix[i].SetComponent(j, array[i, j]);
                }
            }
        }

        public Matrix(Vector[] vectorsArray)
        {
            if (vectorsArray.Length == 0)
            {
                throw new ArgumentException($"Пытаетесь присвоить пустой массив array.Length: \"{vectorsArray.Length}\"", nameof(vectorsArray));
            }

            int maxSize = GetMaxSizeVector(vectorsArray);

            for (int i = 0; i < vectorsArray.Length; i++)
            {
                if (vectorsArray[i].GetSize() < maxSize)
                {
                    Vector tempVector = vectorsArray[i];
                    vectorsArray[i] = new Vector(maxSize);
                    vectorsArray[i].Add(tempVector);
                }
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
            if (columnIndex <= 0 || matrix[0].GetSize() < columnIndex)
            {
                throw new ArgumentException("Искомового столбца нет", nameof(columnIndex));
            }

            double[] tempArray = new double[matrix.Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                tempArray[i] = matrix[i].GetComponent(columnIndex);
            }

            return new Vector(tempArray);
        }

        public Vector GetRow(int rowIndex)
        {
            if (rowIndex <= 0 || matrix.Length < rowIndex)
            {
                throw new ArgumentException("Искомой строки нет", nameof(rowIndex));
            }

            return matrix[rowIndex];
        }

        public void SetRow(int rowIndex, Vector vector)
        {
            if (rowIndex <= 0 || matrix.Length < rowIndex)
            {
                throw new ArgumentException("Искомой строки нет", nameof(rowIndex));
            }

            if (matrix[0].GetSize() != vector.GetSize())
            {
                throw new ArgumentException("Размеры строк должны совпадать", nameof(vector));
            }

            Vector tempVector = new Vector(vector);
            matrix[rowIndex] = tempVector;
        }

        public void Transpose()
        {
            Vector[] tempMatrix = new Vector[matrix[0].GetSize()];

            for (int i = 0; i < matrix[0].GetSize(); i++)
            {
                tempMatrix[i] = new Vector(matrix.Length);

                for (int j = 0; j < matrix.Length; j++)
                {
                    tempMatrix[i].SetComponent(j, matrix[j].GetComponent(i));
                }
            }

            matrix = tempMatrix;
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
                    if (j == columnIndex)
                    {
                        continue;
                    }

                    row.Add(matrix[i].GetComponent(j));
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
            if (vector.GetSize() != matrix[0].GetSize())
            {
                throw new ArgumentException("Количество строк в векторе-столбце не совпадает с количеством стоблцов в матрице", nameof(vector));
            }

            Vector newVector = new Vector(vector.GetSize());

            for (int i = 0; i < matrix.Length; i++)
            {
                double temp = 0;

                for (int j = 0; j < matrix[0].GetSize(); j++)
                {
                    temp += matrix[i].GetComponent(j) * vector.GetComponent(j);

                }

                newVector.SetComponent(i, temp);
            }

            return newVector;
        }

        public void Add(Matrix matrix)
        {
            if (this.matrix.Length != matrix.matrix.Length || this.matrix[0].GetSize() != matrix.matrix[0].GetSize())
            {
                throw new ArgumentException("Матрицы разного размера", nameof(matrix));
            }

            for (int i = 0; i < this.matrix.Length; i++)
            {
                for (int j = 0; j < this.matrix[0].GetSize(); j++)
                {
                    this.matrix[i].SetComponent(j, this.matrix[i].GetComponent(j) + matrix.matrix[i].GetComponent(j));
                }
            }
        }

        public void Subtract(Matrix matrix)
        {
            if (this.matrix.Length != matrix.matrix.Length || this.matrix[0].GetSize() != matrix.matrix[0].GetSize())
            {
                throw new ArgumentException("Матрицы разного размера", nameof(matrix));
            }

            for (int i = 0; i < this.matrix.Length; i++)
            {
                for (int j = 0; j < this.matrix[0].GetSize(); j++)
                {
                    this.matrix[i].SetComponent(j, this.matrix[i].GetComponent(j) - matrix.matrix[i].GetComponent(j));
                }
            }
        }

        public static Matrix GetAmount(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.GetColumnsCount() != matrix2.GetColumnsCount() || matrix1.GetRowsCount() != matrix2.GetRowsCount())
            {
                throw new ArgumentException("Матрицы разного размера", $"{nameof(matrix1)} и {nameof(matrix2)}");
            }

            Matrix newMatrix = new Matrix(matrix1);
            newMatrix.Add(matrix2);

            return newMatrix;
        }

        public static Matrix GetDifference(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.GetColumnsCount() != matrix2.GetColumnsCount() || matrix1.GetRowsCount() != matrix2.GetRowsCount())
            {
                throw new ArgumentException("Матрицы разного размера", $"{nameof(matrix1)} и {nameof(matrix2)}");
            }

            Matrix newMatrix = new Matrix(matrix1);
            newMatrix.Subtract(matrix2);

            return newMatrix;
        }

        public static Matrix GetProduct(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.GetColumnsCount() != matrix2.GetRowsCount())
            {
                throw new ArgumentException("Количество столбцов первой матрицы, не совпадает с количеством строк второй матрицы", $"{nameof(matrix1)} и {nameof(matrix2)}");
            }

            Matrix newMatrix = new Matrix(matrix1.GetRowsCount(), matrix2.GetColumnsCount());

            for (int i = 0; i < matrix1.GetRowsCount(); i++)
            {
                for (int j = 0; j < matrix2.GetColumnsCount(); j++)
                {
                    newMatrix.matrix[i].SetComponent(j, matrix1.matrix[i].GetComponent(j) * matrix2.matrix[j].GetComponent(i));
                }
            }

            return newMatrix;
        }

        private int GetMaxSizeVector(Vector[] matrix)
        {
            int maxSize = matrix[0].GetSize();

            for (int i = 0; i < matrix.Length; i++)
            {
                if (maxSize < matrix[i].GetSize())
                {
                    maxSize = matrix[i].GetSize();
                }
            }

            return maxSize;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("{ ");

            for (int i = 0; i < matrix.Length; i++)
            {
                stringBuilder.Append("{ ");

                for (int j = 0; j < matrix[0].GetSize() - 1; j++)
                {
                    stringBuilder.Append(matrix[i].GetComponent(j).ToString());
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(matrix[i].GetComponent(matrix[0].GetSize() - 1).ToString());
                stringBuilder.Append(" ");

                if (i == matrix.Length - 1)
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
