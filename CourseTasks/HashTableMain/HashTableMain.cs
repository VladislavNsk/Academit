using System;

namespace HashTableMain
{
    class HashTableMain
    {
        static void Main()
        {
            string word1 = "car";
            string word2 = "moto";
            string word3 = "home";
            string word4 = "phone";
            string word5 = "mouse";
            string word6 = "dog";

            HashTable<string> hashTable = new HashTable<string>
            {
                null,
                word1,
                word2,
                word3,
                word4,
                null,
                word5,
                word6
            };

            if (hashTable.Remove(word4))
            {
                Console.WriteLine($"Элемент {word4} удален");
            }
            else
            {
                Console.WriteLine($"Элемент {word4} не найден");
            }

            if (hashTable.Contains(word6))
            {
                Console.WriteLine("Хэш-таблица содержит эелемент " + word6);
            }
            else
            {
                Console.WriteLine("Хэш-таблица не содержит эелемент " + word6);
            }

            string[] array = new string[10];
            hashTable.CopyTo(array, 0);

            Console.WriteLine("Элементы хэш таблицы " + hashTable);
        }
    }
}
