using System;

namespace RangeProgram
{
    class RangeProgram
    {
        static void Main()
        {
            Range range = new Range(0, 10);
            range.From = 1;

            Console.WriteLine("Введите число");
            double number = Convert.ToDouble(Console.ReadLine());

            if (range.IsInside(number))
            {
                Console.WriteLine("Число находится в диапазоне");
            }
            else
            {
                Console.WriteLine("Число вне диапазона");
            }
        }
    }
}
