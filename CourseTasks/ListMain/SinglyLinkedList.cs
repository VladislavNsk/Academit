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

        public SinglyLinkedList(T data)
        {
            head = new ListItem<T>(data);
            Count++;
        }

        public T GetFirst()
        {
            if (Count == 0)
            {
                throw new NullReferenceException("Список пуст");
            }

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

            return GetItem(index).Data;
        }

        public T Set(int index, T data)
        {
            CheckIndex(index);

            ListItem<T> item = GetItem(index);
            T oldData = item.Data;
            item.Data = data;

            return oldData;
        }

        public void Add(int index, T data)
        {
            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException($"Индекс {index} за пределами диапазона. Всего элементов в списке {Count}");
            }

            if (index == 0)
            {
                AddFirst(data);
                return;
            }

            ListItem<T> previous = GetItem(index - 1);
            previous.Next = new ListItem<T>(data, previous.Next);
            Count++;
        }

        public void Add(T data)
        {
            if (Count == 0)
            {
                AddFirst(data);
                return;
            }

            Add(Count, data);
        }

        public T DeleteAtIndex(int index)
        {
            CheckIndex(index);

            if (index == 0)
            {
                return DeleteFirst();
            }

            ListItem<T> previous = GetItem(index - 1);
            T data = previous.Next.Data;
            previous.Next = previous.Next.Next;
            Count--;

            return data;
        }

        public bool Delete(T data)
        {
            if (Count == 0)
            {
                return false;
            }

            if (Equals(head.Data, data))
            {
                DeleteFirst();
                return true;
            }

            for (ListItem<T> current = head, previous = null; current != null; previous = current, current = current.Next)
            {
                if (Equals(current.Data, data))
                {
                    previous.Next = current.Next;
                    Count--;
                    return true;
                }
            }

            return false;
        }

        public T DeleteFirst()
        {
            if (Count == 0)
            {
                throw new NullReferenceException("Список пуст");
            }

            T data = head.Data;
            head = head.Next;
            Count--;

            return data;
        }

        public void AddFirst(T data)
        {
            head = new ListItem<T>(data, head);
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

            SinglyLinkedList<T> listCopy = new SinglyLinkedList<T>(head.Data);
            listCopy.Count = Count;

            for (ListItem<T> itemCopy = listCopy.head, nextSourceItem = head.Next; nextSourceItem != null; nextSourceItem = nextSourceItem.Next, itemCopy = itemCopy.Next)
            {
                itemCopy.Next = new ListItem<T>(nextSourceItem.Data);
            }

            return listCopy;
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
