using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TreeMain
{
    public class Tree<T> : IEnumerable<T>
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
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer), "Сomparer имеет значение null.");
        }

        public Tree(T data, IComparer<T> comparer)
        {
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer), "Сomparer имеет значение null.");
            root = new TreeNode<T>(data);
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

            var currentNode = root;

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

            if (data1 == null && data2 == null)
            {
                return 0;
            }

            if (data1 == null)
            {
                return -1;
            }

            if (data2 == null)
            {
                return 1;
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
                var result = CompareData(parentNode.LeftChild.Data, data);

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
            var currentNode = root;
            TreeNode<T> parentNode = null;

            while (currentNode != null)
            {
                int result = CompareData(currentNode.Data, data);

                if (result > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        return null;
                    }

                    parentNode = currentNode;
                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (result < 0)
                {
                    if (currentNode.RightChild == null)
                    {
                        return null;
                    }

                    parentNode = currentNode;
                    currentNode = currentNode.RightChild;
                    continue;
                }

                return parentNode;
            }

            return null;
        }

        public bool Remove(T data)
        {
            var parentNode = GetParentNode(data);

            if (parentNode == null)
            {
                if (root != null)
                {
                    if (CompareData(root.Data, data) != 0)
                    {
                        return false;
                    }

                    RemoveRoot();
                    modCount++;
                    Count--;
                    return true;
                }

                return false;
            }

            var treeNode = parentNode.LeftChild;

            if (parentNode.RightChild != null && CompareData(parentNode.RightChild.Data, data) == 0)
            {
                treeNode = parentNode.RightChild;
            }

            var childrenCount = treeNode.GetChildrenCount();

            if (childrenCount == 0)
            {
                RemoveLeaf(treeNode, parentNode);
            }
            else if (childrenCount == 1)
            {
                RemoveNodeWithOneChild(treeNode, parentNode);
            }
            else
            {
                RemoveNodeWithTwoChildren(treeNode, parentNode);
            }

            modCount++;
            Count--;
            return true;
        }

        private void RemoveLeaf(TreeNode<T> treeNode, TreeNode<T> parentNode)
        {
            if (parentNode.LeftChild == treeNode)
            {
                parentNode.LeftChild = null;
            }
            else
            {
                parentNode.RightChild = null;
            }
        }

        private void RemoveNodeWithOneChild(TreeNode<T> treeNode, TreeNode<T> parentNode)
        {
            var children = treeNode.GetChildren();

            if (parentNode.LeftChild == treeNode)
            {
                parentNode.LeftChild = children[0] ?? children[1];
            }
            else
            {
                parentNode.RightChild = children[0] ?? children[1];
            }
        }

        private void RemoveNodeWithTwoChildren(TreeNode<T> treeNode, TreeNode<T> parentNode)
        {
            var leftParentNode = GetLeftParentNode(treeNode.RightChild);
            var leftNode = leftParentNode.LeftChild;

            if (leftNode == null)
            {
                if (parentNode.LeftChild == treeNode)
                {
                    parentNode.LeftChild = leftParentNode;
                }
                else
                {
                    parentNode.RightChild = leftParentNode;
                }

                leftParentNode.LeftChild = treeNode.LeftChild;
                return;
            }

            leftParentNode.LeftChild = leftNode.RightChild;

            if (parentNode.LeftChild == treeNode)
            {
                parentNode.LeftChild = leftNode;
            }
            else
            {
                parentNode.RightChild = leftNode;
            }

            leftNode.LeftChild = treeNode.LeftChild;
            leftNode.RightChild = treeNode.RightChild;
        }

        private void RemoveRoot()
        {
            var rootChildrenCount = root.GetChildrenCount();

            if (rootChildrenCount == 0)
            {
                root = null;
                return;
            }

            if (rootChildrenCount == 1)
            {
                var children = root.GetChildren();
                root = children[0] ?? children[1];
                return;
            }

            var leftParentNode = GetLeftParentNode(root.RightChild);
            var leftNode = leftParentNode.LeftChild;

            if (leftNode == null)
            {
                leftParentNode.LeftChild = root.LeftChild;
                root = leftParentNode;
                return;
            }

            leftParentNode.LeftChild = leftNode.RightChild;
            leftNode.LeftChild = root.LeftChild;
            leftNode.RightChild = root.RightChild;
            root = leftNode;
        }

        private static TreeNode<T> GetLeftParentNode(TreeNode<T> treeNode)
        {
            var parentNode = treeNode;

            if (parentNode.LeftChild == null)
            {
                return parentNode;
            }

            var leftNode = parentNode.LeftChild;

            while (leftNode.LeftChild != null)
            {
                parentNode = leftNode;
                leftNode = leftNode.LeftChild;
            }

            return parentNode;
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

            var stack = new Stack<TreeNode<T>>();
            var fixedModCount = modCount;
            stack.Push(root);

            while (stack.Count != 0)
            {
                if (fixedModCount != modCount)
                {
                    throw new InvalidOperationException("Коллекция была изменена");
                }

                var node = stack.Pop();

                if (node.RightChild != null)
                {
                    stack.Push(node.RightChild);
                }

                if (node.LeftChild != null)
                {
                    stack.Push(node.LeftChild);
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

            var queue = new Queue<TreeNode<T>>();
            var fixedModCount = modCount;
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                if (fixedModCount != modCount)
                {
                    throw new InvalidOperationException("Коллекция была изменена");
                }

                var node = queue.Dequeue();

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
            return (IEnumerator<T>)VisitInWidth();
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

            var stringBuilder = new StringBuilder("{");

            foreach (var item in VisitInWidth())
            {
                if (item == null)
                {
                    stringBuilder.Append("null");
                }
                else
                {
                    stringBuilder.Append(item);
                }

                stringBuilder.Append(", ");
            }

            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }
    }
}
