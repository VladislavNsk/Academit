namespace ShapesMain.Shapes
{
    class Square : IShape
    {
        public double SideLength { get; set; }

        public Square(double sideLength)
        {
            SideLength = sideLength;
        }

        public double GetWidth()
        {
            return SideLength;
        }

        public double GetHeight()
        {
            return SideLength;
        }

        public double GetArea()
        {
            return SideLength * SideLength;
        }

        public double GetPerimeter()
        {
            return SideLength * 4;
        }

        public override string ToString()
        {
            return "Square " + SideLength.ToString();
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

            Square square = obj as Square;

            return square.SideLength == SideLength;
        }

        public override int GetHashCode()
        {
            int prime = 17;
            int hash = 1;

            hash = prime * hash + SideLength.GetHashCode();

            return hash;
        }
    }
}
