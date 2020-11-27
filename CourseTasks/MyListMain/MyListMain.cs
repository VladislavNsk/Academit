using System;

namespace MyListMain
{
    class MyListMain
    {
        static void Main()
        {
            MyList<string> myList = new MyList<string>(10) { "word", "world", "price", null, "home" };

            myList.Insert(4, "roof");
            myList.RemoveAt(4);
            myList.Remove("home");

            string[] words = new string[10];
            myList.CopyTo(words, 6);

            Console.Write("Значения массива: ");
            Console.WriteLine(string.Join(", ", words));

            myList.TrimExcess();

            myList.Clear();
            Console.WriteLine("Размер списка после удаления = " + myList.Count);
        }
    }
}
