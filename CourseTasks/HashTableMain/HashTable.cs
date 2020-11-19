using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HashTableMain
{
    class HashTable<T> : ICollection<T>
    {
        private List<T>[] lists;
        private int modCount;

        public HashTable() : this(10)
        {
        }

        public HashTable(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), $"Размер таблицы должен быть больше 0, сейчас размер = {size}");
            }

            lists = new List<T>[size];
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            int index = GetItemIndex(item);

            if (lists[index] == null)
            {
                lists[index] = new List<T>();
            }

            lists[index].Add(item);
            Count++;
            modCount++;
        }

        private int GetItemIndex(T item)
        {
            return item == null ? 0 : Math.Abs(item.GetHashCode() % lists.Length);
        }

        public void Clear()
        {
            Array.Clear(lists, 0, lists.Length);
            Count = 0;
            modCount++;
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                return false;
            }

            int index = GetItemIndex(item);
            return lists[index] != null && lists[index].Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Массив равен null");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), $"Индекс должен быть не меньше 0, сейчас = {arrayIndex}");
            }

            if (Count > array.Length - arrayIndex)
            {
                throw new ArgumentException($"Длинны  массива назначения не хватает для копирования, индекс начала копирования = {arrayIndex}, длинна массива = {array.Length}, нужно {Count + arrayIndex + 1}");
            }

            int i = arrayIndex;

            foreach (T item in this)
            {
                array[i] = item;
                i++;
            }
        }

        public bool Remove(T item)
        {
            if (item == null)
            {
                return false;
            }

            int index = GetItemIndex(item);

            if (lists[index] != null && lists[index].Remove(item))
            {
                if (lists[index].Count == 0)
                {
                    lists[index] = null;
                }

                Count--;
                modCount++;
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            int fixedModCount = modCount;

            foreach (List<T> list in lists)
            {
                if (list != null)
                {
                    foreach (T listItem in list)
                    {
                        if (modCount != fixedModCount)
                        {
                            throw new InvalidOperationException("Коллекция была изменена");
                        }

                        yield return listItem;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("{");

            foreach (T item in this)
            {
                if(item == null)
                {
                    stringBuilder.Append("null");
                }
                else
                {
                    stringBuilder.Append(item);
                }
                
                stringBuilder.Append(", ");
            }

            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }
    }
}
