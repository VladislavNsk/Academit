using System;

namespace ShapesMain.Shapes
{
    class Circle : IShape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public double GetWidth()
        {
            return Radius * 2;
        }

        public double GetHeight()
        {
            return Radius * 2;
        }

        public double GetArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }

        public double GetPerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public override string ToString()
        {
            return "Circle " + Radius.ToString();
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

            Circle circle = obj as Circle;

            return circle.Radius == Radius;
        }

        public override int GetHashCode()
        {
            int prime = 19;
            int hash = 1;

            hash = prime * hash + Radius.GetHashCode();

            return hash;
        }
    }
}
