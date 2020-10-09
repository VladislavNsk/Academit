using System;

namespace Shapes
{
    class Shapes
    {
        public static void Main()
        {
            Shape[] shapes = new Shape[]
            {
                new Triangle(1, 1, 2, 2, 3, 3),
                new Triangle(5.5, 3.3, 7.7, 6.3, 5.9, 12.5),
                new Circle(12),
                new Circle(12),
                new Rectangle(222222.41, 6),
                new Rectangle(3.4, 2.1),
                new Square(6),
                new Square(9.9)
            };

            Console.WriteLine(shapes[4].ToString());

            Console.WriteLine("Фигуры равны - " + shapes[2].Equals(shapes[3]));
            Console.WriteLine("Фигуры не равны - " + shapes[2].Equals(shapes[4]));

            Console.WriteLine("Hash фигуры - " + shapes[0].GetHashCode());

            ShowMaxAreaShape(shapes);
            ShowSecondBySizePerimetrShape(shapes);
        }

        public static void ShowMaxAreaShape(Shape[] shapes)
        {
            Array.Sort(shapes, new AreaComparer());

            Console.WriteLine
            (
                "Площадь = "   + shapes[0].GetArea() +
                " Периметр = " + shapes[0].GetPerimeter() +
                " Ширина = "   + shapes[0].GetWidth() +
                " Высота = "   + shapes[0].GetHeight()
            );
        }

        public static void ShowSecondBySizePerimetrShape(Shape[] shapes)
        {
            Array.Sort(shapes, new PerimeterComparer());

            Console.WriteLine
            (
                "Площадь = "   + shapes[1].GetArea() +
                " Периметр = " + shapes[1].GetPerimeter() +
                " Ширина = "   + shapes[1].GetWidth() +
                " Высота = "   + shapes[1].GetHeight()
            );
        }
    }
}
