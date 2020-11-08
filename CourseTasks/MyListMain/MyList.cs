using System;
using System.Collections;
using System.Collections.Generic;

namespace MyListMain
{
    class MyList<T> : IList<T>
    {
        private T[] array;
        private int count;
        private int capacity;
        private byte modCount;

        public int Capacity
        {
            get
            {
                return capacity;
            }

            set
            {
                if (value < Count)
                {
                    throw new ArgumentOutOfRangeException("value", "Емкость меньше размера списка");
                }

                capacity = value;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public MyList() : this(5)
        {
        }

        public MyList(int capacity)
        {
            modCount = 0;
            count = 0;
            Capacity = capacity;
            array = new T[Capacity];
        }

        public T this[int index]
        {
            get
            {
                CheckOutBounds(index);
                return array[index];
            }

            set
            {
                CheckOutBounds(index);
                array[index] = value;
                modCount++;
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
            if (Count == array.Length)
            {
                IncreaseCapacity();
            }

            array[Count] = item;
            count++;
            modCount++;
        }

        private void IncreaseCapacity()
        {
            Capacity = capacity * 2;
            T[] oldArray = array;
            array = new T[Capacity];
            Array.Copy(oldArray, 0, array, 0, oldArray.Length);
        }

        private void TrimExcess()
        {
            Capacity = count;
            T[] oldArray = array;
            array = new T[Count];
            Array.Copy(oldArray, 0, array, 0, array.Length);
        }

        private bool IsNeedTrimExcess()
        {
            return (double)count / Capacity < 0.2;
        }

        private void CheckOutBounds(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException($"Индекс {index} за пределами диапазона. Всего элементов в списке {Count}");
            }
        }

        public void Clear()
        {
            count = 0;

            if (IsNeedTrimExcess())
            {
                TrimExcess();
            }
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (Count + arrayIndex > array.Length)
            {
                throw new ArgumentException($"Длинны  массива назначения не хватает для копирования, длинна массива = {array.Length}, нужно {Count + arrayIndex + 1}");
            }

            for (int i = 0; i < Count; i++)
            {
                array[arrayIndex++] = this.array[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            int fixedModCount = modCount;

            for (int i = 0; i < Count; i++)
            {
                if (modCount != fixedModCount)
                {
                    throw new InvalidOperationException("Коллекция была изменена");
                }

                yield return array[i];
            }
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            CheckOutBounds(index);

            if (Count == array.Length)
            {
                IncreaseCapacity();
            }

            Array.Copy(array, index, array, index + 1, Count - index);
            array[index] = item;
            count++;
            modCount++;
        }

        public bool Remove(T item)
        {
            if (IndexOf(item) == -1)
            {
                return false;
            }

            RemoveAt(IndexOf(item));
            return true;
        }

        public void RemoveAt(int index)
        {
            CheckOutBounds(index);

            if (index < Count - 1)
            {
                Array.Copy(array, index + 1, array, index, Count - index - 1);
            }

            count--;
            modCount++;

            if (IsNeedTrimExcess())
            {
                TrimExcess();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
