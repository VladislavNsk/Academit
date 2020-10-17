using System;

namespace VectorMain
{
    class Vector
    {
        private double[] vectorComponents;

        public Vector(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException($"Введен не корректный размер вектора size: \"{size}\"", nameof(size));
            }

            vectorComponents = new double[size];
        }

        public Vector(Vector vector)
        {
            vectorComponents = new double[vector.GetSize()];
            vector.vectorComponents.CopyTo(vectorComponents, 0);
        }

        public Vector(double[] array)
        {
            if (array.Length == 0)
            {
                throw new ArgumentException($"Пытаетесь присвоить пустой массив array.Length: \"{array.Length}\"", nameof(array));
            }

            vectorComponents = new double[array.Length];
            array.CopyTo(vectorComponents, 0);
        }

        public Vector(int size, double[] array)
        {
            if (size <= 0 || array.Length == 0)
            {
                throw new ArgumentException($"Не корректные аргументы: size и\\или array: \"{size}\", \"{array.Length}\"", $"{size}, {array}");
            }

            if (array.Length > size)
            {
                throw new ArgumentException($"Длинна массива больше размерность в аргументе: size = {size}, array.Length = {array.Length}", $"{size}, {array}");
            }

            vectorComponents = new double[size];
            array.CopyTo(vectorComponents, 0);
        }

        private void EqualizeNumberComponents(Vector vector)
        {
            if (vectorComponents.Length == vector.vectorComponents.Length)
            {
                return;
            }

            if (vectorComponents.Length > vector.vectorComponents.Length)
            {
                double[] tempArray = vector.vectorComponents;
                vector.vectorComponents = new double[vectorComponents.Length];

                tempArray.CopyTo(vector.vectorComponents, 0);
            }
            else
            {
                double[] tempArray = vectorComponents;
                vectorComponents = new double[vector.vectorComponents.Length];

                tempArray.CopyTo(vectorComponents, 0);
            }
        }

        public int GetSize()
        {
            return vectorComponents.Length;
        }

        public override string ToString()
        {
            return "{ " + string.Join("; ", vectorComponents) + " }";
        }

        public void Add(Vector vector)
        {
            EqualizeNumberComponents(vector);

            for (int i = 0; i < vectorComponents.Length; i++)
            {
                vectorComponents[i] += vector.vectorComponents[i];
            }
        }

        public void Subtract(Vector vector)
        {
            EqualizeNumberComponents(vector);

            for (int i = 0; i < vectorComponents.Length; i++)
            {
                vectorComponents[i] -= vector.vectorComponents[i];
            }
        }

        public void Multiply(double number)
        {
            for (int i = 0; i < vectorComponents.Length; i++)
            {
                vectorComponents[i] *= number;
            }
        }

        public void Reverse()
        {
            Multiply(-1);
        }

        public double GetLength()
        {
            double sum = 0;

            foreach (double component in vectorComponents)
            {
                sum += Math.Pow(component, 2);
            }

            return Math.Sqrt(sum);
        }

        public double GetComponent(int index)
        {
            return vectorComponents[index];
        }

        public void SetComponent(int index, double newValue)
        {
            vectorComponents[index] = newValue;
        }

        public static Vector GetSum(Vector vector1, Vector vector2)
        {
            vector1.Add(vector2);
            Vector vector = new Vector(vector1);
            vector1.Subtract(vector2);

            return vector;
        }

        public static Vector GetDifference(Vector vector1, Vector vector2)
        {
            vector1.Subtract(vector2);
            Vector vector = new Vector(vector1);
            vector1.Add(vector2);

            return vector;
        }

        public static double GetScalarMultiplication(Vector vector1, Vector vector2)
        {
            vector1.EqualizeNumberComponents(vector2);
            double scalarMultiplication = 0;

            for (int i = 0; i < vector1.vectorComponents.Length; i++)
            {
                scalarMultiplication += vector1.vectorComponents[i] * vector2.vectorComponents[i];
            }

            return scalarMultiplication;
        }

        public override int GetHashCode()
        {
            int prime = 7;
            int hash = 1;

            foreach (double component in vectorComponents)
            {
                hash = prime * hash + component.GetHashCode();
            }

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (obj.GetType() != this.GetType() || ReferenceEquals(obj, null))
            {
                return false;
            }

            Vector vector = obj as Vector;

            if (vector.vectorComponents.Length != vectorComponents.Length)
            {
                return false;
            }

            for (int i = 0; i < vectorComponents.Length; i++)
            {
                if (vectorComponents[i] != vector.vectorComponents[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
