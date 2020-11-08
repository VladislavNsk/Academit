using System;

namespace MyListMain
{
    class MyListMain
    {
        static void Main()
        {
            MyList<int> myList = new MyList<int> { 10, 20, 30, 40, 50 };
            myList.Insert(1, 105);
            myList.RemoveAt(0);
            myList.Remove(50);

            Console.Write("Значения списка: ");
            Console.WriteLine(string.Join(", ", myList));

            myList.Clear();
            Console.WriteLine("Размер списка после удаления = " + myList.Count);
        }
    }
}
