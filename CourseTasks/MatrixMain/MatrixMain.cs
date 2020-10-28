using System;
using VectorMain;

namespace Matrices
{
    class Matrices
    {
        static void Main()
        {
            Matrix matrix1 = new Matrix(
             new Vector[]
                {
                    new Vector(new double[] { 1, 0, -2 }),
                    new Vector(new double[] { 3, 2, 1 }),
                    new Vector(new double[] { 1, 2, -2 })
                });

            Matrix matrix2 = new Matrix(
             new Vector[]
                {
                    new Vector(new double[] { 10, 0, -20 }),
                    new Vector(new double[] { 30, 20, 10 }),
                    new Vector(new double[] { 10, 20, -20 })
                });

            Matrix matrix3 = new Matrix(
             new Vector[]
                {
                    new Vector(new double[] { 10, 0, -20, 50, 100 }),
                    new Vector(new double[] { 30, 20, 10 }),
                    new Vector(new double[] { 10, 20, -20 })
                });

            Console.WriteLine(matrix3);

            matrix1.Subtract(matrix2);
            Console.WriteLine(matrix1);

            Vector vector = new Vector(new double[] { 1, 20, -1 });
            Vector newVector = matrix1.MultiplyByVector(vector);

            Console.WriteLine(newVector);

            double determinant = matrix1.GetDeterminant();
            Console.WriteLine(determinant);

            matrix3.Transpose();
            Console.WriteLine(matrix3);
        }
    }
}
