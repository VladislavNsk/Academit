using System;

namespace SinglyLinkedListMain
{
    class SinglyLinkedList<T>
    {
        private ListItem<T> head;
        private int count;

        public SinglyLinkedList()
        {
            count = 0;
        }

        public SinglyLinkedList(ListItem<T> head)
        {
            if (head == null)
            {
                throw new NullReferenceException($"Элемент списка head = null");
            }

            this.head = new ListItem<T>(head);

            for (ListItem<T> item = head; item != null; item = item.NextItem)
            {
                count++;
            }
        }

        public int GetSize()
        {
            return count;
        }

        public T GetFirstItemData()
        {
            if (head != null)
            {
                return head.Data;
            }

            throw new IndexOutOfRangeException("Первого элемента не существует, список  пустой");
        }

        public T GetData(int index)
        {
            if (index >= count || index < 0)
            {
                throw new IndexOutOfRangeException($"Индекса по номеру {index} не существует, всего элементов в списке {count}");
            }

            int itemIndex = 0;
            T data = head.Data;

            for (ListItem<T> item = head; item != null; item = item.NextItem)
            {
                if (itemIndex == index)
                {
                    data = item.Data;
                    return data;
                }

                itemIndex++;
            }

            return data;
        }

        public T SetData(T data, int index)
        {
            if (index >= count || index < 0)
            {
                throw new IndexOutOfRangeException($"Индекса по номеру {index} не существует, всего элементов в списке {count}");
            }

            int itemIndex = 0;
            T oldData = head.Data;

            for (ListItem<T> item = head; item != null; item = item.NextItem)
            {
                if (itemIndex == index)
                {
                    oldData = item.Data;
                    item.Data = data;
                    return oldData;
                }

                itemIndex++;
            }

            return oldData;
        }

        public void Set(ListItem<T> item, int index)
        {
            if (index >= count || index < 0)
            {
                throw new IndexOutOfRangeException($"Индекса по номеру {index} не существует, всего элементов в списке {count}");
            }

            if (index == 0)
            {
                item.NextItem = head;
                head = item;
                count++;
                return;
            }

            int itemIndex = 1;

            for (ListItem<T> i = head.NextItem, previousItem = head; i != null; previousItem = i, i = i.NextItem)
            {
                if (itemIndex == index)
                {
                    item.NextItem = i;
                    previousItem.NextItem = item;
                    count++;
                    return;
                }

                itemIndex++;
            }
        }

        public void Set(ListItem<T> item)
        {
            if (count == 0)
            {
                head = item;
                count++;
                return;
            }

            for (ListItem<T> i = head; i != null; i = i.NextItem)
            {
                if (i.NextItem == null)
                {
                    i.NextItem = item;
                    count++;
                    return;
                }
            }
        }

        public T DeleteAtIndex(int index)
        {
            if (index >= count || index < 0)
            {
                throw new IndexOutOfRangeException($"Индекса по номеру {index} не существует, всего элементов в списке {count}");
            }

            T data = head.Data;

            if (index == 0)
            {
                head = head.NextItem;
                count--;
                return data;
            }

            int itemIndex = 1;

            for (ListItem<T> item = head.NextItem, previousItem = head; item != null; previousItem = item, item = item.NextItem)
            {
                if (itemIndex == index)
                {
                    data = item.Data;
                    previousItem.NextItem = item.NextItem;
                    count--;
                    return data;
                }

                itemIndex++;
            }

            return data;
        }

        public bool Delete(T data)
        {
            if (head == null)
            {
                throw new NullReferenceException("Список  пустой");
            }

            for (ListItem<T> item = head, previousItem = null; item != null; previousItem = item, item = item.NextItem)
            {
                if (item.Data.Equals(data))
                {
                    if (previousItem == null)
                    {
                        head = null;
                        count--;
                        return true;
                    }

                    previousItem.NextItem = item.NextItem;
                    count--;
                    return true;
                }
            }

            return false;
        }

        public T DeleteFirstItem()
        {
            return DeleteAtIndex(0);
        }

        public void SetInFirstItem(ListItem<T> item)
        {
            Set(item, 0);
        }

        public void Reverse()
        {
            if (head == null)
            {
                throw new NullReferenceException("Список пустой");
            }

            ListItem<T> nextItem = head.NextItem;
            ListItem<T> item = head;

            while (nextItem != null)
            {
                item.NextItem = nextItem.NextItem;
                SetInFirstItem(nextItem);
                nextItem = item.NextItem;

                count--;
            }
        }

        public SinglyLinkedList<T> Copy()
        {
            if (head == null)
            {
                return new SinglyLinkedList<T>();
            }

            return new SinglyLinkedList<T>(head);
        }
    }
}
