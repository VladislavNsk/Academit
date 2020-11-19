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

        public Tree(T item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(root), $"Корень дерева не может быть null");
            }

            root = new TreeNode<T>(item);
            Count++;
        }

        public Tree(T item, IComparer<T> comparer)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), $"Корень дерева не может быть null");
            }

            root = new TreeNode<T>(item);
            this.comparer = comparer;
            Count++;
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException($"Элемент дерева не может быть null");
            }

            if (root == null)
            {
                root = new TreeNode<T>(item);
                Count++;
                modCount++;
                return;
            }

            TreeNode<T> currentNode = root;

            if (!(currentNode.Data is IComparable<T>))
            {
                AddWithComparer(item);
                return;
            }

            while (currentNode != null)
            {
                IComparable<T> comparable = (IComparable<T>)currentNode.Data;

                if (comparable.CompareTo(item) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = new TreeNode<T>(item) { Parent = currentNode };
                        Count++;
                        modCount++;
                        return;
                    }

                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (currentNode.RightChild == null)
                {
                    currentNode.RightChild = new TreeNode<T>(item) { Parent = currentNode };
                    Count++;
                    modCount++;
                    return;
                }

                currentNode = currentNode.RightChild;
            }
        }

        private void AddWithComparer(T item)
        {
            if (comparer == null)
            {
                throw new InvalidOperationException($"Компаратор = null, класс не реализует интерфейс IComparable");
            }

            TreeNode<T> currentNode = root;

            while (currentNode != null)
            {
                if (comparer.Compare(currentNode.Data, item) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = new TreeNode<T>(item) { Parent = currentNode };
                        Count++;
                        modCount++;
                        return;
                    }

                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (currentNode.RightChild == null)
                {
                    currentNode.RightChild = new TreeNode<T>(item) { Parent = currentNode };
                    Count++;
                    modCount++;
                    return;
                }

                currentNode = currentNode.RightChild;
            }
        }

        public bool Contains(T item)
        {
            return GetTreeNode(item) != null;
        }

        private TreeNode<T> GetTreeNode(T item)
        {
            TreeNode<T> currentNode = root;

            if (!(currentNode.Data is IComparable<T>))
            {
                return GetTreeNodeWithComparer(item);
            }

            while (currentNode != null)
            {
                IComparable<T> comparable = (IComparable<T>)currentNode.Data;

                if (comparable.CompareTo(item) == 0)
                {
                    return currentNode;
                }

                if (comparable.CompareTo(item) > 0)
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

        private TreeNode<T> GetTreeNodeWithComparer(T item)
        {
            if (comparer == null)
            {
                throw new NullReferenceException($"Компаратор = null, класс не реализует интерфейс IComparable");
            }

            TreeNode<T> currentNode = root;

            while (currentNode != null)
            {
                if (comparer.Compare(currentNode.Data, item) == 0)
                {
                    return currentNode;
                }

                if (comparer.Compare(currentNode.Data, item) > 0)
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

        public bool Remove(T item)
        {
            TreeNode<T> treeNode = GetTreeNode(item);

            if (treeNode == null)
            {
                Console.WriteLine("Узла нет в дереве");
                return false;
            }

            if (treeNode.GetChildrenCount() == 0)
            {
                RemoveSheet(treeNode);
                modCount++;
                return true;
            }

            if (treeNode.GetChildrenCount() == 1)
            {
                RemoveNodeWithOneChild(treeNode);
                modCount++;
                return true;
            }

            RemoveNodeWithTwoChildren(treeNode);
            modCount++;
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
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }

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
            if (root == null)
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
                    if (children[i] != null)
                    {
                        stack.Push(children[i]);
                    }
                }
            }
        }

        public void VisitInWidth()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }

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

        public IEnumerator<T> GetEnumerator()
        {
            int fixedModCount = modCount;
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

                if(fixedModCount != modCount)
                {
                    throw new InvalidOperationException("Коллекция была изменена");
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
            StringBuilder stringBuilder = new StringBuilder("{");
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

                stringBuilder.Append(node.Data);

                if(queue.Count != 0)
                {
                    stringBuilder.Append(", ");
                }
            }

            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
