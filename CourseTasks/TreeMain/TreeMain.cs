using System;

namespace TreeMain
{
    class TreeMain
    {
        static void Main()
        {
            Tree<int> tree = new Tree<int>(123) { 20, 22, 8, 25, 2, 10, 1, 28, 22, 26, 30, 27, 40, 50, 200, 180, 170, 171, 220, 250, 222 };

            tree.Remove(123);

            if (tree.Contains(8))
            {
                Console.WriteLine("Дерево содержит искомый элемент");
            }

            Console.WriteLine("Рекурсивный обход в глубину");
            tree.RecursionDepthVisit();

            Console.WriteLine("Обход в глубину без рекурсии");
            tree.DepthVisit();

            Console.WriteLine("Обход в ширину");
            tree.VisitInWidth();

            Console.WriteLine(tree);
        }
    }
}
