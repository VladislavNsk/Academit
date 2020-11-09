using System;

namespace HashTableMain
{
    class HashTableMain
    {
        static void Main()
        {
            int number1 = 9;
            int number2 = 4;
            int number3 = 52;
            int number4 = -45;
            int number5 = 566;
            int number6 = 99;

            HashTable<int> hashTable = new HashTable<int>()
                {
                     number1,
                     number2,
                     number3,
                     number4,
                     number5,
                     number6
                };

            if(hashTable.Remove(number4))
            {
                Console.WriteLine($"Элемент {number4} удален");
            }
            else
            {
                Console.WriteLine($"Элемент {number4} не найден");
            }

            if(hashTable .Contains(number6))
            {
                Console.WriteLine("Хэш-таблица содержит эелемент " + number6);
            }
            else
            {
                Console.WriteLine("Хэш-таблица не содержит эелемент " + number6);
            }

            foreach(int item in hashTable)
            {
                Console.WriteLine(item);
            }

            int[] array = new int[10];
            hashTable.CopyTo(array, 6);
        }
    }
}
