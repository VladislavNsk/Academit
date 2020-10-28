using System;

namespace VectorMain
{
    public class Vector
    {
        private double[] components;

        public Vector(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), $"Введен не корректный размер вектора size: \"{size}\", должен быть больше 0");
            }

            components = new double[size];
        }

        public Vector(Vector vector)
        {
            components = new double[vector.GetSize()];
            vector.components.CopyTo(components, 0);
        }

        public Vector(double[] array)
        {
            if (array.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(array), $"Пытаетесь присвоить пустой массив array.Length: \"{array.Length}\"");
            }

            components = new double[array.Length];
            array.CopyTo(components, 0);
        }

        public Vector(int size, double[] array)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(array), $"Введен не корректный размер вектора size: \"{size}\", должен быть больше 0");
            }

            components = new double[size];
            array.CopyTo(components, 0);
        }

        private void EqualizeNumberComponents(Vector vector)
        {
            if (components.Length == vector.components.Length)
            {
                return;
            }

            if (components.Length < vector.components.Length)
            {
                Array.Resize(ref components, vector.components.Length);
            }
        }

        public int GetSize()
        {
            return components.Length;
        }

        public override string ToString()
        {
            return "{ " + string.Join(", ", components) + " }";
        }

        public void Add(Vector vector)
        {
            EqualizeNumberComponents(vector);

            for (int i = 0; i < vector.components.Length; i++)
            {
                components[i] += vector.components[i];
            }
        }

        public void Subtract(Vector vector)
        {
            EqualizeNumberComponents(vector);

            for (int i = 0; i < vector.components.Length; i++)
            {
                components[i] -= vector.components[i];
            }
        }

        public void Multiply(double number)
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i] *= number;
            }
        }

        public void Reverse()
        {
            Multiply(-1);
        }

        public double GetLength()
        {
            double sum = 0;

            foreach (double component in components)
            {
                sum += Math.Pow(component, 2);
            }

            return Math.Sqrt(sum);
        }

        public double GetComponent(int index)
        {
            return components[index];
        }

        public void SetComponent(int index, double newValue)
        {
            components[index] = newValue;
        }

        public static Vector GetSum(Vector vector1, Vector vector2)
        {
            Vector tempVector1 = new Vector(vector1);
            Vector tempVector2 = new Vector(vector2);
            tempVector1.Add(tempVector2);

            return tempVector1;
        }

        public static Vector GetDifference(Vector vector1, Vector vector2)
        {
            Vector tempVector1 = new Vector(vector1);
            Vector tempVector2 = new Vector(vector2);
            tempVector1.Subtract(tempVector2);

            return tempVector1;
        }

        public static double GetScalarProduct(Vector vector1, Vector vector2)
        {
            Vector tempVector1 = new Vector(vector1);
            Vector tempVector2 = new Vector(vector2);

            tempVector1.EqualizeNumberComponents(tempVector2);
            double scalarProduct = 0;

            for (int i = 0; i < tempVector2.components.Length; i++)
            {
                scalarProduct += tempVector1.components[i] * tempVector2.components[i];
            }

            return scalarProduct;
        }

        public override int GetHashCode()
        {
            int prime = 7;
            int hash = 1;

            foreach (double component in components)
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

            if (ReferenceEquals(obj, null) || obj.GetType() != GetType())
            {
                return false;
            }

            Vector vector = (Vector)obj;

            if (vector.components.Length != components.Length)
            {
                return false;
            }

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] != vector.components[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
