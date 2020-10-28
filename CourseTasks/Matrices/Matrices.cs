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
                    new Vector(new double[] {  2, 4, 0 }),
                    new Vector(new double[] { -2, 1, 3 }),
                    new Vector(new double[] { -1, 0, 1 })
                });

            Console.WriteLine(matrix1);

            Matrix matrix2 = new Matrix(matrix1);
            Console.WriteLine(matrix2);

            Vector vector = new Vector(new double[] { 1, 20, -1 });
            Vector newVector = matrix1.MultiplyByVector(vector);

            Console.WriteLine(newVector);
        }
    }
}
