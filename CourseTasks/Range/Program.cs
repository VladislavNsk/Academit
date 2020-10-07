using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Range
{
    class Program
    {
        static void Main(string[] args)
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
