using System;

namespace Shapes
{
    class Triangle : Shape
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
            X2 = x2;
            X3 = x3;
            Y1 = y1;
            Y2 = y2;
            Y3 = y3;
        }

        public override double GetWidth()
        {
            double max = X1 > X2 ? X1 : X2;
            max = max > X3 ? max : X3;

            double min = X1 < X2 ? X1 : X2;
            min = min < X3 ? min : X3;

            return max - min;
        }

        public override double GetHeight()
        {
            double max = Y1 > Y2 ? Y1 : Y2;
            max = max > Y3 ? max : Y3;

            double min = Y1 < Y2 ? Y1 : Y2;
            min = min < Y3 ? min : Y3;

            return max - min;
        }

        public override double GetArea()
        {
            return 0.5 * GetWidth() * GetHeight();
        }

        public override double GetPerimeter()
        {
            double sideA = Math.Sqrt(Math.Pow((X2 - X1), 2) + Math.Pow((Y2 - Y1), 2));
            double sideB = Math.Sqrt(Math.Pow((X3 - X1), 2) + Math.Pow((Y3 - Y1), 2));
            double sideC = Math.Sqrt(Math.Pow((X3 - X2), 2) + Math.Pow((Y3 - Y2), 2));

            return sideA + sideB + sideC;
        }

        public override string ToString()
        {
            return $"X1 - {X1}, Y1 - {Y1}, X2 - {X2}, Y2 - {Y2}, X3 - {X3}, Y3 - {Y3}";
        }

        public override bool Equals(object obj)
        {
            Shape shape = (Shape)obj;
            return this.GetArea().Equals(shape.GetArea());
        }

        public override int GetHashCode()
        {
            int prime = 7;
            int hash = 1;

            hash = prime * hash + (int)GetArea();
            hash = prime * hash + GetPerimeter().GetHashCode();
            hash = prime * hash + GetHeight().GetHashCode();
            hash = prime * hash + GetWidth().GetHashCode();

            return hash;
        }
    }
}
