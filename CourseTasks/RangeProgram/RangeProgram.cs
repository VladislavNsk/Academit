using System;

namespace RangeProgram
{
    class RangeProgram
    {
        public static void Main()
        {
            Range range1 = new Range(1, 11);
            Range range2 = new Range(4, 6);

            Console.WriteLine("Введите число");
            double number = double.Parse(Console.ReadLine());

            if (range1.IsInside(number))
            {
                Console.WriteLine("Число находится в диапазоне");
            }
            else
            {
                Console.WriteLine("Число вне диапазона");
            }

            Range intersection = range1.GetIntersection(range2);

            if (intersection != null)
            {
                Console.WriteLine($"Диапазон пересечения от {intersection.From} до {intersection.To}");
            }
            else
            {
                Console.WriteLine("Пересечения диапазонов нет");
            }

            Console.WriteLine("Диапазон объединения");

            Range[] union = range1.GetUnion(range2);
            Console.WriteLine($"От {union[0].From} до {union[0].To}");

            if (union.Length == 2)
            {
                Console.WriteLine($"От {union[1].From} до {union[1].To}");
            }

            Console.WriteLine("Разность диапазонов");
            Range[] difference = range1.GetDifference(range2);

            if (difference.Length > 0)
            {
                Console.WriteLine($"От {difference[0].From} до {difference[0].To}");

                if (difference.Length == 2)
                {
                    Console.WriteLine($"От {difference[1].From} до {difference[1].To}");
                }
            }
            else
            {
                Console.WriteLine("Результат разности пустой");
            }
        }
    }
}
