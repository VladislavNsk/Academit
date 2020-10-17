namespace ShapesMain.Shapes
{
    class Rectangle : IShape
    {
        public double Height { get; set; }

        public double Width { get; set; }

        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }

        public double GetWidth()
        {
            return Height;
        }

        public double GetHeight()
        {
            return Width;
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

            if (obj.GetType() != this.GetType() || ReferenceEquals(obj, null))
            {
                return false;
            }

            Rectangle rectangle = obj as Rectangle;

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
