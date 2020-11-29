namespace TreeMain
{
    class TreeNode<T>
    {
        public TreeNode<T> LeftChild { get; set; }

        public TreeNode<T> RightChild { get; set; }

        public T Data { get; set; }

        public TreeNode(T data)
        {
            Data = data;
        }

        public TreeNode(TreeNode<T> treeNode)
        {
            Data = treeNode.Data;
            LeftChild = treeNode.LeftChild;
            RightChild = treeNode.RightChild;
        }

        public TreeNode<T>[] GetChildren()
        {
            return new TreeNode<T>[] { LeftChild, RightChild };
        }

        public int GetChildrenCount()
        {
            int childrenCount = 0;

            if (LeftChild != null)
            {
                childrenCount++;
            }

            if (RightChild != null)
            {
                childrenCount++;
            }

            return childrenCount;
        }
    }
}
