using System;

namespace SinglyLinkedListMain
{
    class ListItem<T>
    {
        public T Data { get; set; }

        public ListItem<T> NextItem { get; set; }

        public ListItem(T data)
        {
            Data = data;
        }

        public ListItem(T data, ListItem<T> nextItem)
        {
            Data = data;
            NextItem =  new ListItem<T>(nextItem);
        }

        public ListItem(ListItem<T> item)
        {
            if(item == null)
            {
                throw new NullReferenceException($"Элемент списка item = null");
            }

            Data = item.Data;

            if(item.NextItem != null)
            {
                NextItem = new ListItem<T>(item.NextItem);
            }
        }
    }
}
