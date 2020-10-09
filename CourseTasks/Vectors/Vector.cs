using System;

namespace Vectors
{
    class Vector
    {
        public double[] Value { get; set; }

        public Vector(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Введен не корректный размер вектора " + nameof(size));

            }

            Value = new double[size];
        }

        public Vector(Vector vector)
        {
            this.Value = vector.Value;
        }

        public Vector(double[] array)
        {
            Value = array;
        }

        public Vector(int size, double[] array)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Введен не корректный размер вектора " + nameof(size));

            }

            Value = new double[size];

            for (int i = 0; i < array.Length; i++)
            {
                Value[i] = array[i];
            }
        }

        public void CheckSize(Vector vector)
        {
            if(Value.Length == vector.Value.Length)
            {
                return;
            }

            double[] tempArray;

            if (Value.Length > vector.Value.Length)
            {
                tempArray = vector.Value;
                vector.Value = new double[Value.Length];

                for(int i = 0; i < tempArray.Length; i++)
                {
                    vector.Value[i] = tempArray[i];
                }
            }
            else
            {
                tempArray = Value;
                Value = new double[vector.Value.Length];

                for (int i = 0; i < tempArray.Length; i++)
                {
                    Value[i] = tempArray[i];
                }
            }
        }

        public int GetSize()
        {
            return Value.Length;
        }

        public override string ToString()
        {
            return "{ " + string.Join("; ", Value) + " }";
        }

        public void Add(Vector vector)
        {
            CheckSize(vector);

            for(int i = 0; i < Value.Length; i++)
            {
                Value[i] += vector.Value[i];
            }
        }

        public void Subtract(Vector vector)
        {
            CheckSize(vector);

            for (int i = 0; i < Value.Length; i++)
            {
                Value[i] -= vector.Value[i];
            }
        }

        public void Multiply(double number)
        {
            for (int i = 0; i < Value.Length; i++)
            {
                Value[i] *= number;
            }
        }

        public void Expand()
        {
            for (int i = 0; i < Value.Length; i++)
            {
                Value[i] *= -1;
            }
        }

        public double GetLength()
        {
            double tempResult = 0;

            for(int i = 0; i < Value.Length; i++)
            {
                tempResult += Math.Pow(Value[i], 2);
            }

            return Math.Sqrt(tempResult);
        }

        public double GetComponent(int index)
        {
            return Value[index];
        }

        public void SetComponent(int index, double newValue)
        {
            Value[index] = newValue;
        }

        public static Vector AddVectors(Vector vector1, Vector vector2)
        {
            vector1.CheckSize(vector2);
            double[] res = new double[vector1.Value.Length];

            for (int i = 0; i < vector1.Value.Length; i++)
            {
                res[i] = vector1.Value[i] + vector2.Value[i];
            }

            return new Vector(vector1.Value.Length, res);
        }

        public static Vector SubtractVectors(Vector vector1, Vector vector2)
        {
            vector1.CheckSize(vector2);
            double[] res = new double[vector1.Value.Length];

            for (int i = 0; i < vector1.Value.Length; i++)
            {
                res[i] = vector1.Value[i] - vector2.Value[i];
            }

            return new Vector(vector1.Value.Length, res);
        }

        public static Vector MultiplyVectors(Vector vector1, Vector vector2)
        {
            vector1.CheckSize(vector2);
            double[] res = new double[vector1.Value.Length];

            for (int i = 0; i < vector1.Value.Length; i++)
            {
                res[i] = vector1.Value[i] * vector2.Value[i];
            }

            return new Vector(vector1.Value.Length, res);
        }

        public override int GetHashCode()
        {
            int prime = 9;
            int hash = 1;

            hash = prime * hash + Value.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            Vector vector = (Vector)obj;

            if (Value.Length != vector.Value.Length)
            {
                return false;
            }

            for(int i = 0; i < Value.Length; i++)
            {
                if (Value[i] != vector.Value[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
