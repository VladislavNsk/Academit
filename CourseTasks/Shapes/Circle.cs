using System;

namespace Shapes
{
    class Circle : Shape
    {
        public double Radius { get; set; }

        private readonly double diametr;

        public Circle(double radius)
        {
            Radius = radius;
            diametr = 2 * radius;
        }

        public override double GetWidth()
        {
            return diametr;
        }

        public override double GetHeight()
        {
            return diametr;
        }

        public override double GetArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }

        public override double GetPerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public override string ToString()
        {
            return Radius.ToString();
        }

        public override bool Equals(object obj)
        {
            Shape shape = (Shape)obj;
            return this.GetHashCode().Equals(shape.GetHashCode());
        }

        public override int GetHashCode()
        {
            int prime = 3;
            int hash = 1;

            hash = prime * hash + (int)GetArea();
            hash = prime * hash + GetPerimeter().GetHashCode();
            hash = prime * hash + GetHeight().GetHashCode();
            hash = prime * hash + GetWidth().GetHashCode();

            return hash;
        }
    }
}
