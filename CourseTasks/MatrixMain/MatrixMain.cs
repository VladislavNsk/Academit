using System;
using VectorMain;

namespace MatrixMain
{
    class MatrixMain
    {
        static void Main()
        {
            Vector vector1 = new Vector(new double[] { 5, 5, 5 });

            Matrix matrix1 = new Matrix(
             new Vector[]
                {
                    new Vector(new double[] { 2, 1, 8 }),
                    new Vector(new double[] { -3, 0, 5 }),
                    new Vector(new double[] { 4, -1, 1 }),
                    vector1
                });

            Console.WriteLine("До изменения вектора: " + matrix1);
            vector1.SetComponent(2, 555);
            Console.WriteLine("После изменения вектора: " + matrix1);

            Matrix matrix2 = new Matrix(
             new Vector[]
                {
                    new Vector(new double[] { 10, 0, -20 }),
                    new Vector(new double[] { 30, 20, 10 }),
                    new Vector(new double[] { 10, 20, -20 }),
                    vector1
                });

            Matrix matrix3 = new Matrix(
             new Vector[]
                {
                    new Vector(new double[] { 10, 0, -20, }),
                    new Vector(new double[] { 30, 20, 10 }),
                    new Vector(new double[] { 10, 20, -20 })
                });

            Console.WriteLine("Матрица3: " + matrix3);
            matrix3.Transpose();
            Console.WriteLine("Матрица3 после транспонирования: " + matrix3);

            matrix1.Subtract(matrix2);
            Console.WriteLine("Матрица1 после вычитания матрицы2: " + matrix1);

            Vector vector2 = new Vector(new double[] { 1, 20, -1 });
            Vector vector3 = matrix1.MultiplyByVector(vector2);
            Console.WriteLine("Результат умножения матрицы на вектор: " + vector3);

            double determinant = matrix3.GetDeterminant();
            Console.WriteLine("Определитель матрицы3 = " + determinant);
        }
    }
}
