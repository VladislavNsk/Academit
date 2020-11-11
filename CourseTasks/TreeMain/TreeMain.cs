using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (tree.Contains(treeNode6))
            {
                Console.WriteLine("good");
            }
            else
            {
                Console.WriteLine("not good");
            }

            tree.Remove(treeNode2);


        }
    }
}
