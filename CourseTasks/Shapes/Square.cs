namespace Shapes
{
    class Square : Shape
    {
        public double SideLength { get; set; }

        public Square(double sideLength)
        {
            SideLength = sideLength;
        }

        public override double GetWidth()
        {
            return SideLength;
        }

        public override double GetHeight()
        {
            return SideLength;
        }

        public override double GetArea()
        {
            return SideLength * SideLength;
        }

        public override double GetPerimeter()
        {
            return SideLength * 4;
        }

        public override string ToString()
        {
            return SideLength.ToString();
        }

        public override bool Equals(object obj)
        {
            Shape shape = (Shape)obj;
            return this.GetArea().Equals(shape.GetArea());
        }

        public override int GetHashCode()
        {
            int prime = 9;
            int hash = 1;

            hash = prime * hash + (int)GetArea();
            hash = prime * hash + GetPerimeter().GetHashCode();
            hash = prime * hash + GetHeight().GetHashCode();
            hash = prime * hash + GetWidth().GetHashCode();

            return hash;
        }
    }
}
