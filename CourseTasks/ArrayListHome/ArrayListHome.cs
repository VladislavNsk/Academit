using System;
using System.Collections.Generic;
using System.IO;

namespace ArrayListHome
{
    class ArrayListHome
    {
        public static void Main()
        {
            string path = "..\\..\\Text.txt";
            List<string> list = new List<string>();

            ПрочитатьСтрокиВСписок(path, list);
            Console.WriteLine(list[0]);

            List<int> numbersList = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

            RemoveEvenNumbers(numbersList);
            Console.WriteLine(numbersList.Count);

            List<int> duplicateNumbersList = new List<int>()
            {
                1, 2, 3, 4, 5,
                1, 2, 3, 4, 5,
                6, 7, 8, 9, 0,
                6, 7, 8, 9, 0
            };

            List<int> uniqueNumbersList = RemoveDuplicateNumbers(duplicateNumbersList);
            Console.WriteLine(string.Join(" ", uniqueNumbersList));
        }

        public static void ПрочитатьСтрокиВСписок(string path, List<string> list)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string currentLine;

                while (!reader.EndOfStream)
                {
                    if ((currentLine = reader.ReadLine()).Length != 0)
                    {
                        list.Add(currentLine);
                    }
                }
            }
        }

        public static void RemoveEvenNumbers(List<int> numbersList)
        {
            for (int i = 0; i < numbersList.Count; i++)
            {
                if (numbersList[i] % 2 == 0)
                {
                    numbersList.RemoveAt(i);
                }
            }
        }

        public static List<int> RemoveDuplicateNumbers(List<int> duplicateNumbersList)
        {
            List<int> uniqueNumbersList = new List<int>();

            for (int i = 0; i < duplicateNumbersList.Count; i++)
            {
                if (uniqueNumbersList.Contains(duplicateNumbersList[i]))
                {
                    continue;
                }

                uniqueNumbersList.Add(duplicateNumbersList[i]);
            }

            return uniqueNumbersList;
        }
    }
}
