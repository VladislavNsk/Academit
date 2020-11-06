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
            List<string> stringList = GetListFromFile(path);
            Console.WriteLine(string.Join("|", stringList));

            List<int> numbersList = new List<int> { 1, 2, 2, 2, 26, 5, 6, 7, 8, 9, 10 };
            RemoveEvenNumbers(numbersList);
            Console.WriteLine(string.Join(" ", numbersList));

            List<int> duplicateNumbersList = new List<int>
            {
                1, 2, 3, 4, 5,
                1, 2, 3, 4, 5,
                6, 7, 8, 9, 0,
                6, 7, 8, 9, 0
            };

            List<int> uniqueNumbersList = GetUniqueNumbersList(duplicateNumbersList);
            Console.WriteLine(string.Join(" ", uniqueNumbersList));
        }

        public static List<string> GetListFromFile(string path)
        {
            List<string> stringList = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        stringList.Add(reader.ReadLine());
                    }
                }

                return stringList;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Файл \"{e.FileName}\" не найден");
                return stringList;
            }
        }

        public static void RemoveEvenNumbers(List<int> numbersList)
        {
            for (int i = 0; i < numbersList.Count; i++)
            {
                if (numbersList[i] % 2 == 0)
                {
                    numbersList.RemoveAt(i);
                    i--;
                }
            }
        }

        public static List<int> GetUniqueNumbersList(List<int> duplicateNumbersList)
        {
            List<int> uniqueNumbersList = new List<int>(duplicateNumbersList.Count);

            foreach (int number in duplicateNumbersList)
            {
                if (!uniqueNumbersList.Contains(number))
                {
                    uniqueNumbersList.Add(number);
                }
            }

            return uniqueNumbersList;
        }
    }
}
