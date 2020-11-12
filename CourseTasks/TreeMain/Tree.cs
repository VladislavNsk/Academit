using System;
using System.Collections.Generic;
using System.Text;

namespace TreeMain
{
    class Tree<T>
    {
        private TreeNode<T> root;
        public int count;
        private readonly IComparer<T> comparer;

        public Tree()
        {
            Count = 0;
        }

        public Tree(TreeNode<T> root)
        {
            this.root = root ?? throw new Exception();
            Count = 1 + root.GetChildrenCount();
        }

        public Tree(TreeNode<T> root, IComparer<T> comparer)
        {
            this.root = root ?? throw new Exception();
            this.comparer = comparer;
            Count = 1 + root.GetChildrenCount();
        }

        public int Count
        {
            get
            {
                return count;
            }

            private set
            {
                count = value;
            }
        }

        public void Add(TreeNode<T> treeNode)
        {
            if (treeNode == null)
            {
                // ex
            }

            if (root == null)
            {
                root = treeNode;
                Count++;
                return;
            }

            TreeNode<T> currentNode = root;

            if (!(currentNode.Data is IComparable<T>))
            {
                AddWithComparer(treeNode);
                return;
            }

            while (currentNode != null)
            {
                IComparable<T> comparable = (IComparable<T>)currentNode.Data;

                if (comparable.CompareTo(treeNode.Data) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = treeNode;
                        treeNode.Parent = currentNode;
                        Count++;
                        return;
                    }

                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (currentNode.RightChild == null)
                {
                    currentNode.RightChild = treeNode;
                    treeNode.Parent = currentNode;
                    Count++;
                    return;
                }

                currentNode = currentNode.RightChild;
            }
        }

        private void AddWithComparer(TreeNode<T> treeNode)
        {
            if (comparer == null)
            {
                // ex
            }

            TreeNode<T> currentNode = root;

            while (currentNode != null)
            {
                if (comparer.Compare(currentNode.Data, treeNode.Data) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = treeNode;
                        treeNode.Parent = currentNode;
                        Count++;
                        return;
                    }

                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (currentNode.RightChild == null)
                {
                    currentNode.RightChild = treeNode;
                    treeNode.Parent = currentNode;
                    Count++;
                    return;
                }

                currentNode = currentNode.RightChild;
            }
        }

        public bool Contains(TreeNode<T> treeNode)
        {
            TreeNode<T> currentNode = root;

            if (!(currentNode.Data is IComparable<T>))
            {
                return ContainsWithComparer(treeNode);
            }

            while (currentNode != null)
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

                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (currentNode.RightChild == null)
                {
                    return false;
                }

                currentNode = currentNode.RightChild;
            }

            return false;
        }

        private bool ContainsWithComparer(TreeNode<T> treeNode)
        {
            if (comparer == null)
            {
                // ex
            }

            TreeNode<T> currentNode = root;

            while (currentNode != null)
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

                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (currentNode.RightChild == null)
                {
                    return false;
                }

                currentNode = currentNode.RightChild;
            }

            return false;
        }

        public bool Remove(TreeNode<T> treeNode)
        {
            if (!Contains(treeNode))
            {
                Console.WriteLine("Узла нет в дереве");
                return false;
            }

            if (treeNode.GetChildrenCount() == 0)
            {
                RemoveSheet(treeNode);
                return true;
            }

            if (treeNode.GetChildrenCount() == 1)
            {
                RemoveNodeWithOneChild(treeNode);
                return true;
            }

            RemoveNodeWithTwoChildren(treeNode);
            return true;
        }

        private void RemoveSheet(TreeNode<T> treeNode)
        {
            if (treeNode.Parent.LeftChild.Equals(treeNode))
            {
                treeNode.Parent.LeftChild = null;
            }
            else
            {
                treeNode.Parent.RightChild = null;
            }


            Count--;
            return;
        }

        private void RemoveNodeWithOneChild(TreeNode<T> treeNode)
        {
            TreeNode<T>[] child = treeNode.GetChildren();

            if (treeNode.Parent.LeftChild.Equals(treeNode))
            {
                treeNode.Parent.LeftChild = child[0] ?? child[1];
                treeNode.Parent.LeftChild.Parent = treeNode.Parent;
            }
            else
            {
                treeNode.Parent.RightChild = child[0] ?? child[1];
                treeNode.Parent.RightChild.Parent = treeNode.Parent;
            }

            Count--;
            return;
        }

        private void RemoveNodeWithTwoChildren(TreeNode<T> treeNode)
        {
            if (treeNode.Equals(root))
            {
                RemoveRoot();
                return;
            }

            TreeNode<T> minLeftNode = GetMinLeftNode(treeNode.RightChild);

            if (minLeftNode.Parent.Equals(treeNode))
            {
                if (treeNode.Parent.LeftChild.Equals(treeNode))
                {
                    treeNode.Parent.LeftChild = minLeftNode;
                }
                else
                {
                    treeNode.Parent.RightChild = minLeftNode;
                }

                minLeftNode.LeftChild = treeNode.LeftChild;
                return;
            }

            if (minLeftNode.RightChild != null)
            {
                minLeftNode.Parent.LeftChild = minLeftNode.RightChild;
            }
            else
            {
                minLeftNode.Parent.LeftChild = null;
            }

            if (treeNode.Parent.LeftChild.Equals(treeNode))
            {
                treeNode.Parent.LeftChild = minLeftNode;
            }
            else
            {
                treeNode.Parent.RightChild = minLeftNode;
            }

            minLeftNode.LeftChild = treeNode.LeftChild;
            minLeftNode.RightChild = treeNode.RightChild;

            Count--;
        }

        private void RemoveRoot()
        {
            TreeNode<T> minLeftNode = GetMinLeftNode(root.RightChild);

            if (minLeftNode.Parent.Equals(root))
            {
                minLeftNode.LeftChild = root.LeftChild;
                minLeftNode.Parent = null;
                root = minLeftNode;
                return;
            }

            if (minLeftNode.RightChild != null)
            {
                minLeftNode.Parent.LeftChild = minLeftNode.RightChild;
                minLeftNode.RightChild.Parent = minLeftNode.Parent;
            }
            else
            {
                minLeftNode.Parent.LeftChild = null;
            }

            minLeftNode.LeftChild = root.LeftChild;
            minLeftNode.RightChild = root.RightChild;
            minLeftNode.Parent = null;
            root = minLeftNode;
            Count--;
        }

        private static TreeNode<T> GetMinLeftNode(TreeNode<T> minLeftNode)
        {
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(minLeftNode);

            while (stack.Peek().LeftChild != null)
            {
                minLeftNode = stack.Pop();
                stack.Push(minLeftNode.LeftChild);
            }

            return stack.Pop();
        }

        public void RecursionDepthVisit()
        {
            RecursionDepthVisit(root);
        }

        private void RecursionDepthVisit(TreeNode<T> node)
        {
            if (node != null)
            {
                Console.WriteLine(node.Data);

                foreach (TreeNode<T> child in node.GetChildren())
                {
                    RecursionDepthVisit(child);
                }
            }
        }

        public void DepthVisit()
        {
            if(root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }

            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(root);

            while (stack.Count != 0)
            {
                TreeNode<T> node = stack.Peek();
                Console.WriteLine(stack.Pop().Data);
                TreeNode<T>[] children = node.GetChildren();

                for (int i = children.Length - 1; i >= 0; i--)
                {
                    if(children[i] != null)
                    {
                        stack.Push(children[i]);
                    }
                }
            }
        }

        public void VisitInWidth()
        {
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                TreeNode<T> node = queue.Dequeue();

                foreach (TreeNode<T> child in node.GetChildren())
                {
                    if (child != null)
                    {
                        queue.Enqueue(child);
                    }
                }

                Console.WriteLine(node.Data);
            }
        }
    }
}
