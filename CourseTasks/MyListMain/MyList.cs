using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyListMain
{
    class MyList<T> : IList<T>
    {
        private T[] items;
        private int modCount;

        public int Count { get; private set; }

        public int Capacity
        {
            get
            {
                return items.Length;
            }

            set
            {
                if (value < Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Вместимость должна быть больше количества элементов в списке, сейчас = {value}");
                }

                Array.Resize(ref items, value);
            }
        }

        public MyList() : this(5)
        {
        }

        public MyList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), $"Вместимость должна быть больше 0, сейчас = {capacity}");
            }

            Capacity = capacity;
        }

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return items[index];
            }

            set
            {
                CheckIndex(index);
                items[index] = value;
                modCount++;
            }
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (Count == Capacity)
            {
                IncreaseCapacity();
            }

            items[Count] = item;
            Count++;
            modCount++;
        }

        private void IncreaseCapacity()
        {
            Capacity = (Capacity + 1) * 2;
        }

        public void TrimExcess()
        {
            if ((double)Count / Capacity < 0.9)
            {
                Capacity = Count;
            }
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException($"Индекс {index} за пределами диапазона. Индекс должен быть от 0 до {Count - 1} включительно");
            }
        }

        public void Clear()
        {
            Array.Clear(items, 0, Count);
            modCount++;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
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

            Array.Copy(items, 0, array, arrayIndex, Count);
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Equals(items[i], item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException($"Индекс {index} за пределами диапазона. Индекс должен быть от 0 до {Count} включительно");
            }

            if (Count == Capacity)
            {
                IncreaseCapacity();
            }

            if (index == Count)
            {
                items[Count] = item;
            }
            else
            {
                Array.Copy(items, index, items, index + 1, Count - index);
                items[index] = item;
            }

            Count++;
            modCount++;
        }

        public bool Remove(T item)
        {
            int itemIndex = IndexOf(item);

            if (itemIndex == -1)
            {
                return false;
            }

            RemoveAt(itemIndex);
            return true;
        }

        public void RemoveAt(int index)
        {
            CheckIndex(index);

            if (index < Count - 1)
            {
                Array.Copy(items, index + 1, items, index, Count - index - 1);
            }

            items[Count - 1] = default;
            Count--;
            modCount++;
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

                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            if (Count == 0)
            {
                return "{}";
            }

            StringBuilder stringBuilder = new StringBuilder("{");

            for (int i = 0; i < Count; i++)
            {
                if (items[i] == null)
                {
                    stringBuilder.Append("null");
                }
                else
                {
                    stringBuilder.Append(items[i]);
                }

                stringBuilder.Append(", ");
            }

            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }
    }
}
