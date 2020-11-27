using System;

namespace SinglyLinkedListMain
{
    class SinglyLinkedListMain
    {
        static void Main()
        {
            SinglyLinkedList<string> list1 = new SinglyLinkedList<string>();

            list1.Add("a");
            list1.Add("b");
            list1.Add("c");
            list1.Add("d");
            list1.Add(null);
            list1.Add("f");
            list1.Add("g");
            list1.Add("h");
            list1.Add(6, "test");
            list1.AddFirst("First");

            SinglyLinkedList<string> list2 = list1.Copy();

            Console.WriteLine("Первый список до реверса " + list1);
            list1.Reverse();
            Console.WriteLine("После " + list1);

            Console.WriteLine("Второй список " + list2);
            list2.Delete("f");
            list2.DeleteAtIndex(3);
            Console.WriteLine("Второй список после удаления элементов " + list2);

            Console.WriteLine("Размер первого списка = " + list1.Count);
            Console.WriteLine("Размер второго списка = " + list2.Count);
        }
    }
}
