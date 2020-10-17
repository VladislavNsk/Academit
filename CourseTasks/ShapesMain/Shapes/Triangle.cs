﻿using System;

namespace ShapesMain.Shapes
{
    class Triangle : IShape
    {
        public double X1 { get; set; }

        public double Y1 { get; set; }

        public double X2 { get; set; }

        public double Y2 { get; set; }

        public double X3 { get; set; }

        public double Y3 { get; set; }

        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            X3 = x3;
            Y3 = y3;
        }

        private double GetSideLength(double x2, double x1, double y2, double y1)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public double GetWidth()
        {
            return Math.Max(Math.Max(X1, X2), X3) - Math.Min(Math.Min(X1, X2), X3);
        }

        public double GetHeight()
        {
            return Math.Max(Math.Max(Y1, Y2), Y3) - Math.Min(Math.Min(Y1, Y2), Y3);
        }

        public double GetArea()
        {
            return GetWidth() * GetHeight() / 2;
        }

        public double GetPerimeter()
        {
            double sideA = Math.Sqrt(Math.Pow((X2 - X1), 2) + Math.Pow((Y2 - Y1), 2));
            double sideB = Math.Sqrt(Math.Pow((X3 - X1), 2) + Math.Pow((Y3 - Y1), 2));
            double sideC = Math.Sqrt(Math.Pow((X3 - X2), 2) + Math.Pow((Y3 - Y2), 2));

            return sideA + sideB + sideC;
        }

        public override string ToString()
        {
            return $"Triangle ({X1}; {Y1}), ({X2}; {Y2}), ({X3}; {Y3})";
        }

        public override bool Equals(object shape)
        {
            if (ReferenceEquals(shape, this))
            {
                return true;
            }

            if (shape.GetType() != this.GetType() || ReferenceEquals(shape, null))
            {
                return false;
            }

            Triangle triangle = shape as Triangle;

            return triangle.X1 == X1 && triangle.Y1 == Y1 &&
                   triangle.X2 == X2 && triangle.Y2 == Y2 &&
                   triangle.X3 == X3 && triangle.Y3 == Y3;
        }

        public override int GetHashCode()
        {
            int prime = 7;
            int hash = 1;

            hash = prime * hash + X1.GetHashCode();
            hash = prime * hash + Y1.GetHashCode();
            hash = prime * hash + X2.GetHashCode();
            hash = prime * hash + Y2.GetHashCode();
            hash = prime * hash + X3.GetHashCode();
            hash = prime * hash + Y3.GetHashCode();

            return hash;
        }
    }
}