﻿using System;

namespace MyListMain
{
    class MyListMain
    {
        static void Main()
        {
            string word = null;
            MyList<string> myList = new MyList<string>(10) { "word", "world", "price", word, "home" };

            myList.Insert(5, "roof");
            myList.RemoveAt(5);
            myList.Remove(word);

            string[] words = new string[10];
            myList.CopyTo(words, 6);

            Console.Write("Значения массива: ");
            Console.WriteLine(string.Join(", ", words));

            myList.Clear();
            Console.WriteLine("Размер списка после удаления = " + myList.Count);
        }
    }
}
