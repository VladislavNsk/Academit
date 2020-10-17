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

            Range crossingRange = range1.GetIntersection(range2);

            if (crossingRange != null)
            {
                Console.WriteLine($"Диапазон пересечения от {crossingRange.From} до {crossingRange.To}");
            }
            else
            {
                Console.WriteLine("Пересечения диапазонов нет");
            }

            Console.WriteLine("Диапазон объединения");

            Range[] unitedRanges = range1.GetUnion(range2);
            Console.WriteLine($"От {unitedRanges[0].From} до {unitedRanges[0].To}");

            if (unitedRanges.Length == 2)
            {
                Console.WriteLine($"От {unitedRanges[1].From} до {unitedRanges[1].To}");
            }

            Console.WriteLine("Разность диапазонов");
            Range[] rangesDifference = range1.GetDifference(range2);

            if (rangesDifference.Length > 0)
            {
                Console.WriteLine($"От {rangesDifference[0].From} до {rangesDifference[0].To}");

                if (rangesDifference.Length == 2)
                {
                    Console.WriteLine($"От {rangesDifference[1].From} до {rangesDifference[1].To}");
                }
            }
            else
            {
                Console.WriteLine("От 0 до 0");
            }
        }
    }
}
