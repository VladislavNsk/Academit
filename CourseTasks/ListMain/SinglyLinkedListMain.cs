using System;

namespace SinglyLinkedListMain
{
    class SinglyLinkedListMain
    {
        static void Main()
        {
            SinglyLinkedList<string> singlyLinkedList1 = new SinglyLinkedList<string>();

            singlyLinkedList1.Add("a");
            singlyLinkedList1.Add("b");
            singlyLinkedList1.Add("c");
            singlyLinkedList1.Add("d");
            singlyLinkedList1.Add(null);
            singlyLinkedList1.Add("f");
            singlyLinkedList1.Add("g");
            singlyLinkedList1.Add("h");
            singlyLinkedList1.Add(6, "test");

            SinglyLinkedList<string> singlyLinkedList2 = singlyLinkedList1.Copy();

            Console.WriteLine("Первый список до реверса " + singlyLinkedList1);
            singlyLinkedList1.Reverse();
            Console.WriteLine("После " + singlyLinkedList1);

            Console.WriteLine("Второй список " + singlyLinkedList2);
            singlyLinkedList2.Delete("b");
            singlyLinkedList2.DeleteAtIndex(3);
            Console.WriteLine("Второй список после удаления элементов " + singlyLinkedList2);

            Console.WriteLine("Размер первого списка = " + singlyLinkedList1.Count);
            Console.WriteLine("Размер второго списка = " + singlyLinkedList2.Count);
        }
    }
}
