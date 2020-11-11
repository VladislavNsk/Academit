using System;
using System.Collections.Generic;

namespace TreeMain
{
    class Tree<T>
    {
        private TreeNode<T> root;
        private int count;
        private TreeNode<T> currentNode;
        private TreeNode<T> currentNodeParent;
        IComparer<T> comparer;

        public Tree()
        {
            count = 0;
        }

        public Tree(TreeNode<T> root) : this(root, null)
        {
        }

        public Tree(TreeNode<T> root, IComparer<T> comparer)
        {
            count = 1 + root.GetChildrenCount();
            this.root = root ?? throw new Exception();
            this.comparer = comparer;
        }

        public void Add(TreeNode<T> treeNode)
        {
            if (root == null)
            {
                root = treeNode;
                count++;
                return;
            }

            currentNode = root;

            if (currentNode.Data is IComparable<T>)
            {
                while (true)
                {
                    IComparable<T> comparable = currentNode.Data as IComparable<T>;

                    if (comparable.CompareTo(treeNode.Data) > 0)
                    {
                        if (currentNode.LeftChild == null)
                        {
                            currentNode.LeftChild = treeNode;
                            count++;
                            return;
                        }

                        currentNodeParent = currentNode;
                        currentNode = currentNode.LeftChild;
                        continue;
                    }

                    if (currentNode.RightChild == null)
                    {
                        currentNode.RightChild = treeNode;
                        count++;
                        return;
                    }

                    currentNodeParent = currentNode;
                    currentNode = currentNode.RightChild;
                }
            }

            if (comparer == null)
            {
                // ex
            }

            while (true)
            {
                if (comparer.Compare(currentNode.Data, treeNode.Data) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = treeNode;
                        count++;
                        return;
                    }

                    currentNodeParent = currentNode;
                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (currentNode.RightChild == null)
                {
                    currentNode.RightChild = treeNode;
                    count++;
                    return;
                }

                currentNodeParent = currentNode;
                currentNode = currentNode.RightChild;
            }
        }

        public bool Contains(TreeNode<T> treeNode)
        {
            currentNode = root;

            if (currentNode.Data is IComparable<T>)
            {
                while (true)
                {
                    IComparable<T> comparable = currentNode.Data as IComparable<T>;

                    if (comparable.CompareTo(treeNode.Data) == 0)
                    {
                        return true;
                    }

                    if (comparable.CompareTo(treeNode.Data) > 0)
                    {
                        if (currentNode.LeftChild == null)
                        {
                            return false;
                        }

                        currentNodeParent = currentNode;
                        currentNode = currentNode.LeftChild;
                        continue;
                    }

                    if (currentNode.RightChild == null)
                    {
                        return false;
                    }

                    currentNodeParent = currentNode;
                    currentNode = currentNode.RightChild;
                }
            }

            if (comparer == null)
            {
                // ex
            }

            while (true)
            {
                if (comparer.Compare(currentNode.Data, treeNode.Data) == 0)
                {
                    return true;
                }

                if (comparer.Compare(currentNode.Data, treeNode.Data) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        return false;
                    }

                    currentNodeParent = currentNode;
                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (currentNode.RightChild == null)
                {
                    return false;
                }

                currentNodeParent = currentNode;
                currentNode = currentNode.RightChild;
            }
        }

        public void Remove(TreeNode<T> treeNode)
        {
            if (!Contains(treeNode))
            {
                Console.WriteLine("Элемента нет в дереве");
                return;
            }

            if (currentNode.GetChildrenCount() == 0)
            {
                if (currentNodeParent.LeftChild.Equals(currentNode))
                {
                    currentNodeParent.LeftChild = null;
                }
                else
                {
                    currentNodeParent.RightChild = null;
                }

                count--;
                return;
            }

            if (currentNode.GetChildrenCount() == 1)
            {
                TreeNode<T>[] child = currentNode.GetChildren();

                if (currentNodeParent.LeftChild.Equals(currentNode))
                {
                    currentNodeParent.LeftChild = child[0] ?? child[1];
                }
                else
                {
                    currentNodeParent.RightChild = child[0] ?? child[1];
                }

                count--;
                return;
            }

            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            TreeNode<T> minLeftNodeParent = currentNode;
            TreeNode<T> minLeftNode = currentNode.RightChild;
            stack.Push(minLeftNode);

            while (stack.Peek().LeftChild != null)
            {
                minLeftNode = stack.Pop();
                stack.Push(minLeftNode.LeftChild);
                minLeftNodeParent = minLeftNode;
            }

            if(minLeftNode.RightChild != null)
            {
                minLeftNodeParent.LeftChild = minLeftNode.RightChild;
            }
            else
            {
                minLeftNodeParent.LeftChild = null;
            }

            if (currentNodeParent.LeftChild.Equals(currentNode))
            {
                currentNodeParent.LeftChild = minLeftNode;
            }
            else
            {
                currentNodeParent.RightChild = minLeftNode;
            }

            count--;
        }
    }
}
