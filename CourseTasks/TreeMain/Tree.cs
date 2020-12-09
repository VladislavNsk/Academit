using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TreeMain
{
    class Tree<T> : IEnumerable<T>
    {
        private TreeNode<T> root;
        private readonly IComparer<T> comparer;
        private int modCount;

        public int Count { get; private set; }

        public Tree()
        {
        }

        public Tree(T data)
        {
            root = new TreeNode<T>(data);
            Count++;
        }

        public Tree(IComparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer), "Сomparer имеет значение null.");
            }

            this.comparer = comparer;
        }

        public Tree(T data, IComparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer), "Сomparer имеет значение null.");
            }

            root = new TreeNode<T>(data);
            this.comparer = comparer;
            Count++;
        }

        public void Add(T data)
        {
            if (root == null)
            {
                root = new TreeNode<T>(data);
                Count++;
                modCount++;
                return;
            }

            TreeNode<T> currentNode = root;

            while (currentNode != null)
            {
                if (CompareData(currentNode.Data, data) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = new TreeNode<T>(data);
                        Count++;
                        modCount++;
                        return;
                    }

                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (currentNode.RightChild == null)
                {
                    currentNode.RightChild = new TreeNode<T>(data);
                    Count++;
                    modCount++;
                    return;
                }

                currentNode = currentNode.RightChild;
            }
        }

        private int CompareData(T data1, T data2)
        {
            if (comparer != null)
            {
                return comparer.Compare(data1, data2);
            }

            if (data1 == null)
            {
                return -1;
            }

            if (data1 is IComparable<T> comparable)
            {
                return comparable.CompareTo(data2);
            }

            throw new InvalidCastException("Класс не реализует интерфейс IComparable");
        }

        public bool Contains(T data)
        {
            return GetTreeNode(data) != null;
        }

        private TreeNode<T> GetTreeNode(T data)
        {
            var parentNode = GetParentNode(data);

            if (parentNode == root)
            {
                return root;
            }

            if (parentNode != null)
            {
                int result = CompareData(parentNode.LeftChild.Data, data);

                if (result == 0)
                {
                    return parentNode.LeftChild;
                }

                return parentNode.RightChild;
            }

            return null;
        }

        private TreeNode<T> GetParentNode(T data)
        {
            TreeNode<T> currentNode = root;

            while (currentNode != null)
            {
                int result = CompareData(currentNode.Data, data);

                if (result > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        return null;
                    }

                    if (CompareData(currentNode.LeftChild.Data, data) == 0)
                    {
                        return currentNode;
                    }

                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (result < 0)
                {
                    if (currentNode.RightChild == null)
                    {
                        return null;
                    }

                    if (CompareData(currentNode.RightChild.Data, data) == 0)
                    {
                        return currentNode;
                    }

                    currentNode = currentNode.RightChild;
                    continue;
                }

                return currentNode;
            }

            return null;
        }

        public bool Remove(T data)
        {
            TreeNode<T> treeNode = GetTreeNode(data);

            if (treeNode == null)
            {
                return false;
            }

            if (treeNode == root)
            {
                RemoveRoot();
                modCount++;
                Count--;
                return true;
            }

            int childrenCount = treeNode.GetChildrenCount();

            if (childrenCount == 0)
            {
                RemoveLeaf(treeNode);
            }
            else if (childrenCount == 1)
            {
                RemoveNodeWithOneChild(treeNode);
            }
            else
            {
                RemoveNodeWithTwoChildren(treeNode);
            }

            modCount++;
            Count--;
            return true;
        }

        private void RemoveLeaf(TreeNode<T> treeNode)
        {
            TreeNode<T> parentNode = GetParentNode(treeNode.Data);

            if (parentNode.LeftChild == treeNode)
            {
                parentNode.LeftChild = null;
            }
            else
            {
                parentNode.RightChild = null;
            }
        }

        private void RemoveNodeWithOneChild(TreeNode<T> treeNode)
        {
            TreeNode<T>[] child = treeNode.GetChildren();
            TreeNode<T> parentNode = GetParentNode(treeNode.Data);

            if (parentNode.LeftChild == treeNode)
            {
                parentNode.LeftChild = child[0] ?? child[1];
            }
            else
            {
                parentNode.RightChild = child[0] ?? child[1];
            }
        }

        private void RemoveNodeWithTwoChildren(TreeNode<T> treeNode)
        {
            TreeNode<T> minLeftNode = GetMinLeftNode(treeNode.RightChild);
            TreeNode<T> minLeftParentNode = GetParentNode(minLeftNode.Data);
            TreeNode<T> parentNode = GetParentNode(treeNode.Data);

            if (minLeftParentNode == treeNode)
            {
                if (parentNode.LeftChild == treeNode)
                {
                    parentNode.LeftChild = minLeftNode;
                }
                else
                {
                    parentNode.RightChild = minLeftNode;
                }

                minLeftNode.LeftChild = treeNode.LeftChild;
                return;
            }

            minLeftParentNode.LeftChild = minLeftNode.RightChild;

            if (parentNode.LeftChild == treeNode)
            {
                parentNode.LeftChild = minLeftNode;
            }
            else
            {
                parentNode.RightChild = minLeftNode;
            }

            minLeftNode.LeftChild = treeNode.LeftChild;
            minLeftNode.RightChild = treeNode.RightChild;
        }

        private void RemoveRoot()
        {
            int rootChildrenCount = root.GetChildrenCount();

            if (rootChildrenCount == 0)
            {
                root = null;
                return;
            }

            if (rootChildrenCount == 1)
            {
                TreeNode<T>[] child = root.GetChildren();
                root = child[0] ?? child[1];
                return;
            }

            TreeNode<T> minLeftNode = GetMinLeftNode(root.RightChild);
            TreeNode<T> minLeftParentNode = GetParentNode(minLeftNode.Data);

            if (minLeftParentNode == root)
            {
                minLeftNode.LeftChild = root.LeftChild;
                root = minLeftNode;
                return;
            }

            minLeftParentNode.LeftChild = minLeftNode.RightChild;
            minLeftNode.LeftChild = root.LeftChild;
            minLeftNode.RightChild = root.RightChild;
            root = minLeftNode;
        }

        private static TreeNode<T> GetMinLeftNode(TreeNode<T> minLeftNode)
        {
            TreeNode<T> currentNode = minLeftNode;

            while (currentNode.LeftChild != null)
            {
                currentNode = currentNode.LeftChild;
            }

            return currentNode;
        }

        public void RecursionDepthVisit(Action<T> action)
        {
            if (root == null)
            {
                return;
            }

            RecursionDepthVisit(root, action);
        }

        private static void RecursionDepthVisit(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                action(node.Data);

                RecursionDepthVisit(node.LeftChild, action);
                RecursionDepthVisit(node.RightChild, action);
            }
        }

        public IEnumerable<T> DepthVisit()
        {
            if (root == null)
            {
                yield break;
            }

            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(root);

            while (stack.Count != 0)
            {
                TreeNode<T> node = stack.Pop();

                if (node.LeftChild != null)
                {
                    stack.Push(node.LeftChild);
                }

                if (node.RightChild != null)
                {
                    stack.Push(node.RightChild);
                }

                yield return node.Data;
            }
        }

        public IEnumerable<T> VisitInWidth()
        {
            if (root == null)
            {
                yield break;
            }

            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                TreeNode<T> node = queue.Dequeue();

                if (node.LeftChild != null)
                {
                    queue.Enqueue(node.LeftChild);
                }

                if (node.RightChild != null)
                {
                    queue.Enqueue(node.RightChild);
                }

                yield return node.Data;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (root == null)
            {
                yield break;
            }

            int fixedModCount = modCount;
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                TreeNode<T> node = queue.Dequeue();

                if (fixedModCount != modCount)
                {
                    throw new InvalidOperationException("Коллекция была изменена");
                }

                if (node.LeftChild != null)
                {
                    queue.Enqueue(node.LeftChild);
                }

                if (node.RightChild != null)
                {
                    queue.Enqueue(node.RightChild);
                }

                yield return node.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            if (Count == 0)
            {
                return "{}";
            }

            StringBuilder tree = new StringBuilder("{");

            foreach (var item in VisitInWidth())
            {
                if (item == null)
                {
                    tree.Append("null");
                }
                else
                {
                    tree.Append(item);
                }

                tree.Append(", ");
            }

            tree.Remove(tree.Length - 2, 2);
            tree.Append("}");

            return tree.ToString();
        }
    }
}
