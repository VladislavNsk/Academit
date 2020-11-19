using System;
using System.Text;

namespace SinglyLinkedListMain
{
    class SinglyLinkedList<T>
    {
        private ListItem<T> head;

        public int Count { get; private set; }

        public SinglyLinkedList()
        {
        }

        public SinglyLinkedList(T value)
        {
            head = new ListItem<T>(value);
            Count++;
        }

        public T GetFirst()
        {
            return head.Data;
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException($"Элемента с индексом {index} нет, всего элементов в списке {Count}");
            }
        }

        private ListItem<T> GetItem(int index)
        {
            int i = 0;
            ListItem<T> item;

            for (item = head; item != null; item = item.Next)
            {
                if (i == index)
                {
                    return item;
                }

                i++;
            }

            return item;
        }

        public T Get(int index)
        {
            CheckIndex(index);

            if (index == 0)
            {
                return head.Data;
            }

            return GetItem(index).Data;
        }

        public T Set(int index, T data)
        {
            CheckIndex(index);
            T oldData = head.Data;

            if (index == 0)
            {
                head.Data = data;
                return oldData;
            }

            ListItem<T> item = GetItem(index);
            oldData = item.Data;
            item.Data = data;

            return oldData;
        }

        public void Add(int index, T data)
        {
            CheckIndex(index);

            if (index == 0)
            {
                AddFirst(data);
                return;
            }

            int i = 1;

            for (ListItem<T> current = head.Next, previous = head; current != null; previous = current, current = current.Next)
            {
                if (i == index)
                {
                    previous.Next = new ListItem<T>(data, current);
                    Count++;
                    return;
                }

                i++;
            }
        }

        public void Add(T data)
        {
            if (Count == 0)
            {
                AddFirst(data);
                return;
            }

            GetItem(Count - 1).Next = new ListItem<T>(data);
            Count++;
        }

        public T DeleteAtIndex(int index)
        {
            CheckIndex(index);

            if (index == 0)
            {
                return DeleteFirst();
            }

            int i = 1;
            T data = head.Data;

            for (ListItem<T> current = head.Next, previous = head; current != null; previous = current, current = current.Next)
            {
                if (i == index)
                {
                    data = current.Data;
                    previous.Next = current.Next;
                    Count--;
                    return data;
                }

                i++;
            }

            return data;
        }

        public bool Delete(T data)
        {
            if(data == head.Data)
            {

            }

            for (ListItem<T> current = head, previous = null; current != null; previous = current, current = current.Next)
            {
                if (current.Data == null)
                {
                    continue;
                }

                if (current.Data.Equals(data))
                {
                    if (previous == null)
                    {
                        head = head.Next;
                        Count--;
                        return true;
                    }

                    previous.Next = current.Next;
                    Count--;
                    return true;
                }
            }

            return false;
        }

        public T DeleteFirst()
        {
            T data = head.Data;
            head = head.Next;
            Count--;
            return data;
        }

        public void AddFirst(T data)
        {
            ListItem<T> item = new ListItem<T>(data) { Next = head };
            head = item;
            Count++;
        }

        public void Reverse()
        {
            if (head == null)
            {
                return;
            }

            ListItem<T> current = head;
            ListItem<T> next = head.Next;

            while (next != null)
            {
                current.Next = next.Next;
                next.Next = head;
                head = next;
                next = current.Next;
            }
        }

        public SinglyLinkedList<T> Copy()
        {
            if (head == null)
            {
                return new SinglyLinkedList<T>();
            }

            SinglyLinkedList<T> singly = new SinglyLinkedList<T>() { head = new ListItem<T>(head.Data), Count = Count };

            for (ListItem<T> item1 = singly.head, item2 = head; item2.Next != null; item2 = item2.Next, item1 = item1.Next)
            {
                item1.Next = new ListItem<T>(item2.Next.Data);
            }

            return singly;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("{");

            for (ListItem<T> item = head; item != null; item = item.Next)
            {
                if (item.Data == null)
                {
                    stringBuilder.Append("null");
                }
                else
                {
                    stringBuilder.Append(item.Data);
                }

                if (item.Next != null)
                {
                    stringBuilder.Append(", ");
                }
            }

            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
