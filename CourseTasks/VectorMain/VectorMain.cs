﻿using System;

namespace VectorMain
{
    public class VectorMain
    {
        public static void Main()
        {
            double[] array1 = { 0, 5, 7, 10, 99 };
            double[] array2 = { 0, 22, 33, 44, 55, 66, 77 };

            Vector vector1 = new Vector(array1);
            Vector vector2 = new Vector(10);
            Vector vector3 = new Vector(vector1);
            Vector vector4 = new Vector(10, array2);

            vector2.Add(vector4);
            Console.WriteLine(vector2.ToString());

            vector2.SetComponent(0, 100500);
            Console.WriteLine(vector2.ToString());

            Console.WriteLine("Размер вектора = " + vector1.GetSize());

            if (vector1.Equals(vector3))
            {
                Console.WriteLine("Вектора равны");
            }
            else
            {
                Console.WriteLine("Вектора не равны");
            }

            vector1.Subtract(vector4);
            Console.WriteLine("Первый ветор после вычитания = " + vector1.ToString());

            if (vector1.Equals(vector3))
            {
                Console.WriteLine("Вектора равны");
            }
            else
            {
                Console.WriteLine("Вектора не равны");
            }

            vector4.Multiply(4.1);
            Console.WriteLine(vector4.ToString());

            Vector vector5 = Vector.GetSum(vector1, vector4);
            Console.WriteLine("Сумма двух векторов = " + vector5.ToString());

            double scalarMultiplication = Vector.GetScalarMultiplication(vector5, vector1);
            Console.WriteLine(scalarMultiplication.ToString());

            vector5.Reverse();
            Console.WriteLine(vector5.ToString());
        }
    }
}
