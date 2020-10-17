using System;
using ShapesMain.Comparers;
using ShapesMain.Shapes;

namespace ShapesMain
{
    class ShapesMain
    {
        public static void Main()
        {
            IShape[] shapes =
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

            foreach (var f in shapes)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("--------------------------------");
                Console.WriteLine();
                Console.WriteLine(f.GetArea());
                Console.WriteLine(f.GetPerimeter());
            }

            PrintMaxAreaShape(shapes);
            PrintSecondByPerimeterShape(shapes);
        }

        public static void PrintMaxAreaShape(IShape[] shapes)
        {
            Array.Sort(shapes, new AreaComparer());

            Console.WriteLine
            (
                shapes[7] +
                " Площадь = " + shapes[7].GetArea() +
                " Периметр = " + shapes[7].GetPerimeter() +
                " Ширина = " + shapes[7].GetWidth() +
                " Высота = " + shapes[7].GetHeight()
            );
        }

        public static void PrintSecondByPerimeterShape(IShape[] shapes)
        {
            Array.Sort(shapes, new PerimeterComparer());

            Console.WriteLine
            (
                shapes[6] +
                " Площадь = " + shapes[6].GetArea() +
                " Периметр = " + shapes[6].GetPerimeter() +
                " Ширина = " + shapes[6].GetWidth() +
                " Высота = " + shapes[6].GetHeight()
            );
        }
    }
}
