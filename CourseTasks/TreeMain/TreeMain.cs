using System;

namespace TreeMain
{
    class TreeMain
    {
        static void Main()
        {
            TreeNode<int> treeNode1 = new TreeNode<int>(123);
            TreeNode<int> treeNode2 = new TreeNode<int>(20);
            TreeNode<int> treeNode3 = new TreeNode<int>(222);
            TreeNode<int> treeNode4 = new TreeNode<int>(8);
            TreeNode<int> treeNode5 = new TreeNode<int>(25);
            TreeNode<int> treeNode6 = new TreeNode<int>(2);
            TreeNode<int> treeNode7 = new TreeNode<int>(10);
            TreeNode<int> treeNode8 = new TreeNode<int>(1);
            TreeNode<int> treeNode9 = new TreeNode<int>(28);
            TreeNode<int> treeNode10 = new TreeNode<int>(22);
            TreeNode<int> treeNode11 = new TreeNode<int>(26);
            TreeNode<int> treeNode12 = new TreeNode<int>(30);
            TreeNode<int> treeNode13 = new TreeNode<int>(27);
            TreeNode<int> treeNode14 = new TreeNode<int>(40);
            TreeNode<int> treeNode15 = new TreeNode<int>(50);
            TreeNode<int> treeNode16 = new TreeNode<int>(200);
            TreeNode<int> treeNode17 = new TreeNode<int>(180);
            TreeNode<int> treeNode18 = new TreeNode<int>(170);
            TreeNode<int> treeNode19 = new TreeNode<int>(171);
            TreeNode<int> treeNode20 = new TreeNode<int>(220);
            TreeNode<int> treeNode21 = new TreeNode<int>(250);
            TreeNode<int> treeNode22 = new TreeNode<int>(222);

            Tree<int> tree = new Tree<int>(treeNode1);

            tree.Add(treeNode2);
            tree.Add(treeNode3);
            tree.Add(treeNode4);
            tree.Add(treeNode5);
            tree.Add(treeNode6);
            tree.Add(treeNode7);
            tree.Add(treeNode8);
            tree.Add(treeNode9);
            tree.Add(treeNode10);
            tree.Add(treeNode11);
            tree.Add(treeNode12);
            tree.Add(treeNode13);
            tree.Add(treeNode14);
            tree.Add(treeNode15);
            tree.Add(treeNode16);
            tree.Add(treeNode17);
            tree.Add(treeNode18);
            tree.Add(treeNode19);
            tree.Add(treeNode20);
            tree.Add(treeNode21);
            tree.Add(treeNode22);

            tree.Remove(treeNode1);
            
            Console.WriteLine("Рекурсивный обход в глубину");
            tree.RecursionDepthVisit();

            Console.WriteLine("Обход в глубину без рекурсии");
            tree.DepthVisit();

            Console.WriteLine("Обход в ширину");
            tree.VisitInWidth();
        }
    }
}
