using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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
            this.comparer = comparer;
        }

        public Tree(T data, IComparer<T> comparer)
        {
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

        private int CompareData(T currentData, T requiredData)
        {
            if (currentData is IComparable<T> comparable)
            {
                return comparable.CompareTo(requiredData);
            }

            if (comparer == null)
            {
                throw new InvalidOperationException("Компаратор = null, класс не реализует интерфейс IComparable");
            }

            return comparer.Compare(currentData, requiredData);
        }

        public bool Contains(T data)
        {
            return GetTreeNode(data) != null;
        }

        private TreeNode<T> GetTreeNode(T data)
        {
            TreeNode<T> currentNode = root;

            while (currentNode != null)
            {
                int result = CompareData(currentNode.Data, data);

                if (result == 0)
                {
                    return currentNode;
                }

                if (result > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        return null;
                    }

                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (currentNode.RightChild == null)
                {
                    return null;
                }

                currentNode = currentNode.RightChild;
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

                if (currentNode.RightChild == null)
                {
                    return null;
                }

                if (CompareData(currentNode.RightChild.Data, data) == 0)
                {
                    return currentNode;
                }

                currentNode = currentNode.RightChild;
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

            if(treeNode.Equals(root))
            {
                RemoveRoot();
                modCount++;
                Count--;
                return true;
            }

            if (treeNode.GetChildrenCount() == 0)
            {
                RemoveLeaf(treeNode);
                modCount++;
                Count--;
                return true;
            }

            if (treeNode.GetChildrenCount() == 1)
            {
                RemoveNodeWithOneChild(treeNode);
                modCount++;
                Count--;
                return true;
            }

            RemoveNodeWithTwoChildren(treeNode);
            modCount++;
            Count--;
            return true;
        }

        private void RemoveLeaf(TreeNode<T> treeNode)
        {
            TreeNode<T> parrentNode = GetParentNode(treeNode.Data);

            if (parrentNode.LeftChild.Equals(treeNode))
            {
                parrentNode.LeftChild = null;
            }
            else
            {
                parrentNode.RightChild = null;
            }
        }

        private void RemoveNodeWithOneChild(TreeNode<T> treeNode)
        {
            TreeNode<T>[] child = treeNode.GetChildren();
            TreeNode<T> parrentNode = GetParentNode(treeNode.Data);

            if (parrentNode.LeftChild.Equals(treeNode))
            {
                parrentNode.LeftChild = child[0] ?? child[1];
            }
            else
            {
                parrentNode.RightChild = child[0] ?? child[1];
            }
        }

        private void RemoveNodeWithTwoChildren(TreeNode<T> treeNode)
        {
            TreeNode<T> minLeftNode = GetMinLeftNode(treeNode.RightChild);
            TreeNode<T> minLeftParrentNode = GetParentNode(minLeftNode.Data);
            TreeNode<T> parrentNode = GetParentNode(treeNode.Data);

            if (minLeftParrentNode.Equals(treeNode))
            {
                if (parrentNode.LeftChild.Equals(treeNode))
                {
                    parrentNode.LeftChild = minLeftNode;
                }
                else
                {
                    parrentNode.RightChild = minLeftNode;
                }

                minLeftNode.LeftChild = treeNode.LeftChild;
                return;
            }

            minLeftParrentNode.LeftChild = minLeftNode.RightChild;

            if (parrentNode.LeftChild.Equals(treeNode))
            {
                parrentNode.LeftChild = minLeftNode;
            }
            else
            {
                parrentNode.RightChild = minLeftNode;
            }

            minLeftNode.LeftChild = treeNode.LeftChild;
            minLeftNode.RightChild = treeNode.RightChild;
        }

        private void RemoveRoot()
        {
            if(root.GetChildrenCount() == 0)
            {
                root = null;
                return;
            }

            if(root.GetChildrenCount() == 1)
            {
                TreeNode<T>[] child = root.GetChildren();
                root = child[0] ?? child[1];
                return;
            }

            TreeNode<T> minLeftNode = GetMinLeftNode(root.RightChild);
            TreeNode<T> minLeftParrentNode = GetParentNode(minLeftNode.Data);

            if (minLeftParrentNode.Equals(root))
            {
                minLeftNode.LeftChild = root.LeftChild;
                root = minLeftNode;
                return;
            }

            minLeftParrentNode.LeftChild = minLeftNode.RightChild;
            minLeftNode.LeftChild = root.LeftChild;
            minLeftNode.RightChild = root.RightChild;
            root = minLeftNode;
        }

        private static TreeNode<T> GetMinLeftNode(TreeNode<T> minLeftNode)
        {
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();

            if(minLeftNode == null)
            {
                return minLeftNode;
            }

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
            if (root == null)
            {
                return;
            }

            RecursionDepthVisit(root);
        }

        private IEnumerable<T> RecursionDepthVisit(TreeNode<T> node)
        {
            if (node != null)
            {
                yield return node.Data;

                RecursionDepthVisit(node.LeftChild);
                RecursionDepthVisit(node.RightChild);
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

                if(node != null)
                {
                    stack.Push(node.RightChild);
                    stack.Push(node.LeftChild);

                    yield return node.Data;
                }
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

                if (node != null)
                {
                    queue.Enqueue(node.LeftChild);
                    queue.Enqueue(node.RightChild);

                    yield return node.Data;
                }
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

                if (node != null)
                {
                    queue.Enqueue(node.LeftChild);
                    queue.Enqueue(node.RightChild);

                    yield return node.Data;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("{");

            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                TreeNode<T> node = queue.Dequeue();

                if (node != null)
                {
                    queue.Enqueue(node.LeftChild);
                    queue.Enqueue(node.RightChild);
                    stringBuilder.Append(node.Data);

                    if (queue.Count != 0)
                    {
                        stringBuilder.Append(", ");
                    }
                }
            }

            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
