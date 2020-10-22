using System;
using VectorMain;

namespace Matrices
{
    class Matrices
    {
        static void Main()
        {
            Matrix matrix = new Matrix(
             new Vector[]
                {
                new Vector(new double[] {  2, 4, 0 }),
                new Vector(new double[] { -2, 1, 3 }),
                new Vector(new double[] { -1, 0, 1 })
                });


            Vector vector = new Vector(new double[] { 1, 2, -1 });
            matrix.MultiplyByVector(vector);

            Console.WriteLine(matrix.ToString());

            Matrix matrix1 = new Matrix(4, 5);
        }
    }
}
