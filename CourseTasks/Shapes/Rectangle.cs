namespace Shapes
{
    class Rectangle : Shape
    {
        public double SideALength { get; set; }

        public double SideBLength { get; set; }

        public Rectangle(double aSideLength, double bSideLength)
        {
            SideALength = aSideLength;
            SideBLength = bSideLength;
        }

        public override double GetWidth()
        {
            return SideALength;
        }

        public override double GetHeight()
        {
            return SideBLength;
        }

        public override double GetArea()
        {
            return SideBLength * SideALength;
        }

        public override double GetPerimeter()
        {
            return SideBLength * 2 + SideALength * 2;
        }

        public override string ToString()
        {
            return $"А - {SideALength}, B - {SideBLength}";
        }

        public override bool Equals(object obj)
        {
            Shape shape = (Shape)obj;
            return this.GetArea().Equals(shape.GetArea());
        }

        public override int GetHashCode()
        {
            int prime = 5;
            int hash = 1;

            hash = prime * hash + (int)GetArea();
            hash = prime * hash + GetPerimeter().GetHashCode();
            hash = prime * hash + GetHeight().GetHashCode();
            hash = prime * hash + GetWidth().GetHashCode();

            return hash;
        }
    }
}
