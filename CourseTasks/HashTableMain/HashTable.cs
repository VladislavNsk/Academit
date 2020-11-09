using System;
using System.Collections;
using System.Collections.Generic;

namespace HashTableMain
{
    class HashTable<T> : ICollection<T>
    {
        private List<T>[] array;
        private int count;
        private int size;
        private int modCount;

        public HashTable() : this(10)
        {
        }

        public HashTable(int size)
        {
            this.size = size;
            array = new List<T>[size];
            count = 0;
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(T item)
        {
            int index = GetIndexItem(item);

            if (array[index] == null)
            {
                array[index] = new List<T>();
            }

            array[index].Add(item);
            count++;
            modCount++;
        }

        private int GetIndexItem(T item)
        {
            return Math.Abs(item.GetHashCode() % array.Length);
        }

        public void Clear()
        {
            array = new List<T>[size];
            count = 0;
            modCount++;
        }

        public bool Contains(T item)
        {
            int index = GetIndexItem(item);

            if (array[index] != null)
            {
                return array[index].Contains(item);
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex >= array.Length || arrayIndex < 0)
            {
                throw new IndexOutOfRangeException($"Индекс {arrayIndex} за пределами диапазона. Всего элементов в списке {array.Length}");
            }

            if (Count + arrayIndex > array.Length)
            {
                throw new ArgumentException($"Длинны  массива назначения не хватает для копирования, длинна массива = {array.Length}, нужно {Count + arrayIndex}");
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (this.array[i] != null)
                {
                    for (int j = 0; j < this.array[i].Count; j++)
                    {
                        array[arrayIndex++] = this.array[i][j];
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            int fixedModCount = modCount;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    foreach (var j in array[i])
                    {
                        if (j != null)
                        {
                            yield return j;
                        }

                        if (modCount != fixedModCount)
                        {
                            throw new InvalidOperationException("Коллекция была изменена");
                        }
                    }
                }
            }
        }

        public bool Remove(T item)
        {
            int index = GetIndexItem(item);

            if (array[index] != null)
            {
                if (array[index].Remove(item))
                {
                    if (array[index].Count == 0)
                    {
                        array[index] = null;
                    }

                    count--;
                    modCount++;
                    return true;
                }
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
