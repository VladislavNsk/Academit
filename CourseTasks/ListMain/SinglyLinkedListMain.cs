using System;

namespace SinglyLinkedListMain
{
    class SinglyLinkedListMain
    {
        static void Main()
        {
            ListItem<int> item1 = new ListItem<int>(5);
            SinglyLinkedList<int> singlyLinkedList1 = new SinglyLinkedList<int>(item1);

            ListItem<int> item2 = new ListItem<int>(3);
            ListItem<int> item3 = new ListItem<int>(84);
            ListItem<int> item4 = new ListItem<int>(11);
            ListItem<int> item5 = new ListItem<int>(-8);

            singlyLinkedList1.Set(item2);
            singlyLinkedList1.Set(item3);
            singlyLinkedList1.Set(item4);
            singlyLinkedList1.Set(item5);

            SinglyLinkedList<int> singlyLinkedList2 = singlyLinkedList1.Copy();

            ListItem<int> item6 = new ListItem<int>(777);
            ListItem<int> item7 = new ListItem<int>(555);

            singlyLinkedList2.SetInFirstItem(item6);
            singlyLinkedList2.Set(item7, 3);

            singlyLinkedList1.Reverse();

            Console.WriteLine("Размер первого списка = " + singlyLinkedList1.GetSize());
            Console.WriteLine("Размер торого списка = " + singlyLinkedList2.GetSize());
        }
    }
}
