﻿using System;

namespace TreeMain
{
    class TreeMain
    {
        static void Main()
        {
            Tree<string> tree = new Tree<string>("Main") { "Color", "Game", null, "Food", null, "Mouse" };

            tree.Remove("Main");

            if (tree.Contains("Food"))
            {
                Console.WriteLine("Дерево содержит искомый элемент");
            }

            tree.RecursionDepthVisit((t) => Console.Write(t + " "));
            Console.WriteLine();

            foreach (var treeItem in tree.VisitInWidth())
            {
                if (treeItem != null)
                {
                    Console.Write(treeItem.ToUpper() + " ");
                }
            }

            Console.WriteLine();
            Console.WriteLine(tree);
        }
    }
}
