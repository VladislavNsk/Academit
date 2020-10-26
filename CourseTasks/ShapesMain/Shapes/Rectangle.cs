namespace ShapesMain.Shapes
{
    class Rectangle : IShape
    {
        public double Width { get; set; }

        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double GetWidth()
        {
            return Width;
        }

        public double GetHeight()
        {
            return Height;
        }

        public double GetArea()
        {
            return Width * Height;
        }

        public double GetPerimeter()
        {
            return (Width + Height) * 2;
        }

        public override string ToString()
        {
            return $"Rectangle height = {Height}, width = {Width}";
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

            Rectangle rectangle = (Rectangle)obj;

            return rectangle.Width == Width && rectangle.Height == Height;
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int hash = 1;

            hash = prime * hash + Height.GetHashCode();
            hash = prime * hash + Width.GetHashCode();

            return hash;
        }
    }
}
